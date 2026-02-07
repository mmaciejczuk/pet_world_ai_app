using PetWorld.Application.Abstractions.AI;
using PetWorld.Application.Chat.Models;
using PetWorld.Domain.Entities;
using PetWorld.Domain.Repositories;

namespace PetWorld.Application.Chat;

public sealed class AskQuestionService
{
    private const int MaxIterations = 3;

    private readonly IProductRepository _products;
    private readonly IChatHistoryRepository _history;
    private readonly IWriterAgent _writer;
    private readonly ICriticAgent _critic;

    public AskQuestionService(
        IProductRepository products,
        IChatHistoryRepository history,
        IWriterAgent writer,
        ICriticAgent critic)
    {
        _products = products;
        _history = history;
        _writer = writer;
        _critic = critic;
    }

    public async Task<AskResult> AskAsync(string question, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(question))
            throw new ArgumentException("Question cannot be empty.", nameof(question));

        var catalog = await _products.GetAllAsync(ct);

        string? feedback = null;
        string answer = string.Empty;
        int iterations = 0;

        for (var i = 1; i <= MaxIterations; i++)
        {
            iterations = i;

            var writerResp = await _writer.WriteAsync(question, catalog, feedback, ct);
            answer = (writerResp.Answer ?? string.Empty).Trim();

            var verdict = await _critic.CriticizeAsync(question, answer, catalog, ct);

            if (verdict.Approved)
                break;

            feedback = verdict.Feedback;
        }

        await _history.AddAsync(new ChatHistoryItem
        {
            Question = question.Trim(),
            Answer = answer,
            Iterations = iterations
        }, ct);

        return new AskResult
        {
            Answer = answer,
            Iterations = iterations
        };
    }
}
