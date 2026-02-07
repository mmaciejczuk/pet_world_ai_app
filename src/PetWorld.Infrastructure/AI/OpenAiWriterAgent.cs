using Microsoft.Agents.AI;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using PetWorld.Application.Abstractions.AI;
using PetWorld.Domain.Entities;
using System.Text;

namespace PetWorld.Infrastructure.AI;

public sealed class OpenAiWriterAgent : IWriterAgent
{
    private readonly AiOptions _opt;

    public OpenAiWriterAgent(IOptions<AiOptions> opt) => _opt = opt.Value;

    public async Task<WriterResponse> WriteAsync(
        string question,
        IReadOnlyList<Product> products,
        string? criticFeedback,
        CancellationToken ct = default)
    {
        var apiKey = ResolveApiKey();
        var client = new OpenAIClient(apiKey);

        ChatClient chatClient = client.GetChatClient(_opt.ChatModelId);

        // Używamy overload z instructions + name (działa w tutorialach / tej linii API)
        AIAgent agent = chatClient.AsAIAgent(
            instructions:
@"You are PetWorld store assistant.
Rules:
- Use ONLY products from the provided catalog.
- Recommend up to 3 products max.
- Show prices in PLN.
- Be concise and practical.
Output format:
1) Short answer (1-3 sentences)
2) Recommendations (bullets: Name — Price — Why)
3) If something is unclear, ask ONE short clarifying question at the end.",
            name: "Writer");

        var prompt = BuildPrompt(question, criticFeedback, products);

        var response = await agent.RunAsync(prompt, cancellationToken: ct);
        var text = (response.Text ?? response.ToString() ?? string.Empty).Trim();

        return new WriterResponse { Answer = text };
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

    private static string BuildPrompt(string question, string? criticFeedback, IReadOnlyList<Product> products)
    {
        var sb = new StringBuilder();
        sb.AppendLine("CUSTOMER QUESTION:");
        sb.AppendLine(question.Trim());
        sb.AppendLine();

        if (!string.IsNullOrWhiteSpace(criticFeedback))
        {
            sb.AppendLine("CRITIC FEEDBACK (fix the issues):");
            sb.AppendLine(criticFeedback.Trim());
            sb.AppendLine();
        }

        sb.AppendLine("CATALOG (use only these items):");
        foreach (var p in products)
            sb.AppendLine($"- {p.Name} | {p.Category} | {p.PricePln:0.##} PLN | {p.Description}");

        return sb.ToString();
    }
}
