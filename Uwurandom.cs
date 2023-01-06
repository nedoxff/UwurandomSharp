using Xoshiro.PRNG64;

namespace UwurandomSharp;

/// <summary>
/// A class that generates random catboy nonsense.
/// </summary>
public class Uwurandom
{
    /// <summary>
    /// Actions for one of the states.
    /// </summary>
    private static readonly List<string> Actions = new()
    {
        "*tilts head*",
        "*twitches ears slightly*",
        "*purrs*",
        "*falls asleep*",
        "*sits on ur keyboard*",
        "*nuzzles*",
        "*stares at u*",
        "*points towards case of monster zero ultra*",
        "*sneezes*",
        "*plays with yarn*",
        "*eats all ur doritos*",
        "*lies down on a random surface*"
    };

    /// <summary>
    /// _previous & _current keep track of what was printed (to avoid repetition).
    /// </summary>
    private int _previous, _current;

    /// <summary>
    /// Prints nonsense to the console without stopping.
    /// </summary>
    public void PrintInfinitely()
    {
        while (true)
            Console.Write(Generate() + " ");
        // ReSharper disable once FunctionNeverReturns
    }

    /// <summary>
    /// Generates a random uwurandom string.
    /// </summary>
    /// <returns>The generated string.</returns>
    /// <exception cref="ArgumentOutOfRangeException">This shouldn't happen, really.</exception>
    public string Generate()
    {
        _previous = _current;
        _current = Next(0, 9);
        while (_current == _previous)
            _current = Next(0, 9);

        return _current switch
        {
            0 => "uwu",
            1 => ":3",
            2 => "owo",
            3 => "ny" + new string('a', Next(1, 6)),
            4 => ">" + new string('/', Next(3, 6)) + "<",
            5 => Actions[Next(0, Actions.Count - 1)],
            6 => new string('A', Next(5, 16)),
            7 => GenerateFromChain(CatgirlNonsense.Table, 149, 7, "mr"),
            8 => GenerateFromChain(Keysmash.Table, 149, Next(0, Keysmash.Table.Count - 1)),
            9 => GenerateFromChain(TheScrunkly.Table, 99, 37, "aw"),
            _ => throw new ArgumentOutOfRangeException(nameof(_current), "Switch argument out of range")
        };
    }

    /// <summary>
    /// Generates a random value in range [min, max].
    /// </summary>
    /// <param name="min">Minimum value.</param>
    /// <param name="max">Maximum value.</param>
    /// <returns>The generated random number.</returns>
    private int Next(int min, int max)
    {
        return new XoShiRo256plus().Next(min, max + 1);
    }

    /// <summary>
    /// Generates a random string via a Markov chain.
    /// </summary>
    /// <param name="table">The markov chain to use.</param>
    /// <param name="limit">Loop count limit.</param>
    /// <param name="initialState">Initial state value.</param>
    /// <param name="initialResult">Start string (optional).</param>
    /// <returns>The generated string.</returns>
    private string GenerateFromChain(IReadOnlyList<MarkovEntry> table, int limit, int initialState,
        string initialResult = "")
    {
        var result = initialResult;
        var state = initialState;
        for (var i = 0; i < limit; i++)
        {
            var rand = Next(0, table[state].Total - 1);
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            // Using LINQ might be a bit slower here
            foreach (var choice in table[state].Choices)
            {
                if (choice.Probability < rand) continue;
                result += choice.NextCharacter;
                state = choice.Next;
                break;
            }
        }

        return result;
    }
}