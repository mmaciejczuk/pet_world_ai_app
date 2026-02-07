namespace PetWorld.Infrastructure.AI;

public sealed class AiOptions
{
    public string Provider { get; init; } = "OpenAI"; // na razie OpenAI (Agent Framework ChatCompletion)
    public string ApiKey { get; init; } = string.Empty;
    public string ChatModelId { get; init; } = "gpt-4o-mini";
}
