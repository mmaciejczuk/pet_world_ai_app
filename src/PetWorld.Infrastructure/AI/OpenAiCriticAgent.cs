using Microsoft.Agents.AI;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using PetWorld.Application.Abstractions.AI;
using PetWorld.Domain.Entities;
using System.Text;
using System.Text.Json;

namespace PetWorld.Infrastructure.AI;

public sealed class OpenAiCriticAgent : ICriticAgent
{
    private readonly AiOptions _opt;

    public OpenAiCriticAgent(IOptions<AiOptions> opt) => _opt = opt.Value;

    public async Task<CriticVerdict> CriticizeAsync(
        string question,
        string answer,
        IReadOnlyList<Product> products,
        CancellationToken ct = default)
    {
        var apiKey = ResolveApiKey();
        var client = new OpenAIClient(apiKey);

        ChatClient chatClient = client.GetChatClient(_opt.ChatModelId);

        AIAgent agent = chatClient.AsAIAgent(
            instructions:
@"You are a strict reviewer for PetWorld answers.
Check:
- Uses ONLY items from the catalog
- Recommends at most 3 products
- Includes PLN prices
- No hallucinations, no extra products, no made-up claims
Return ONLY valid JSON:
{""approved"": true/false, ""feedback"": ""short actionable feedback (empty if approved)""}",
            name: "Critic");

        var prompt = BuildPrompt(question, answer, products);

        var response = await agent.RunAsync(prompt, cancellationToken: ct);
        return ParseVerdict(response.Text ?? response.ToString());
    }

    private string ResolveApiKey()
    {
        if (!string.IsNullOrWhiteSpace(_opt.ApiKey))
            return _opt.ApiKey;

        var env = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        if (!string.IsNullOrWhiteSpace(env))
            return env;

        throw new InvalidOperationException("Missing AI ApiKey (AI__ApiKey or OPENAI_API_KEY).");
    }

    private static string BuildPrompt(string question, string answer, IReadOnlyList<Product> products)
    {
        var sb = new StringBuilder();
        sb.AppendLine("QUESTION:");
        sb.AppendLine(question.Trim());
        sb.AppendLine();
        sb.AppendLine("ANSWER TO REVIEW:");
        sb.AppendLine(answer.Trim());
        sb.AppendLine();
        sb.AppendLine("CATALOG (allowed items):");
        foreach (var p in products)
            sb.AppendLine($"- {p.Name} | {p.Category} | {p.PricePln:0.##} PLN | {p.Description}");
        return sb.ToString();
    }

    private static CriticVerdict ParseVerdict(string? raw)
    {
        var text = (raw ?? string.Empty).Trim();

        var start = text.IndexOf('{');
        var end = text.LastIndexOf('}');
        if (start >= 0 && end > start)
            text = text[start..(end + 1)];

        try
        {
            using var doc = JsonDocument.Parse(text);
            var root = doc.RootElement;

            var approved = root.TryGetProperty("approved", out var a) && a.GetBoolean();
            var feedback = root.TryGetProperty("feedback", out var f) ? (f.GetString() ?? "") : "";

            return new CriticVerdict { Approved = approved, Feedback = feedback.Trim() };
        }
        catch
        {
            return new CriticVerdict
            {
                Approved = false,
                Feedback = "Critic did not return valid JSON. Return ONLY JSON with keys: approved, feedback."
            };
        }
    }
}
