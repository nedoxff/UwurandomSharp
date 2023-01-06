namespace UwurandomSharp;

/// <summary>
/// Markov chain model.
/// </summary>
internal class MarkovEntry
{
    public readonly List<MarkovChoice> Choices;
    public readonly int Total;

    public MarkovEntry(int total, List<MarkovChoice> choices)
    {
        Total = total;
        Choices = choices;
    }
}

/// <summary>
/// Markov choice model.
/// </summary>
internal class MarkovChoice
{
    public readonly int Next;
    public readonly char NextCharacter;
    public readonly int Probability;

    public MarkovChoice(int next, char nextCharacter, int probability)
    {
        Next = next;
        NextCharacter = nextCharacter;
        Probability = probability;
    }
}