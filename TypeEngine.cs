using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

class TypeEngine
{
    internal enum CharState
    {
        NotTyped,
        Correct,
        Incorrect,
        Current
    }

    internal CharState GetCharState(int index, StringBuilder typed, string paragraph)
    {
        if (index == typed.Length)
            return CharState.Current;

        if (index >= typed.Length)
            return CharState.NotTyped;

        if (index >= paragraph.Length)
            return CharState.Incorrect;

        return typed[index] == paragraph[index]
            ? CharState.Correct
            : CharState.Incorrect;
    }
    internal void SetColor(CharState state)
    {
        switch (state)
        {
            case CharState.Correct:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case CharState.Incorrect:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case CharState.Current:
                Console.ForegroundColor = ConsoleColor.DarkGray;
                break;
            default:
                Console.ResetColor();
                break;
        }
    }

    internal void RenderParagraph(string paragraph, StringBuilder typed)
    {
        for (int i = 0; i < paragraph.Length; i++)
        {
            CharState state = GetCharState(i, typed, paragraph);
            SetColor(state);
            Console.Write(paragraph[i]);
        }
        Console.ResetColor();
    }

    static void RedrawTypedText(StringBuilder typed)
    {
        Console.SetCursorPosition(0, 3);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, 3);
        Console.Write(typed);
    }


    internal void freeStyleParagraph()
    {
        Console.Clear();
        Console.CursorVisible = false;
        Console.WriteLine("FREE STYLE PARAGRAPH\n");

       string paragraph = ParagraphGenerator.Generate(30);

        StringBuilder typed = new StringBuilder();
        bool timerStarted = false;
        ConsoleKeyInfo key = new ConsoleKeyInfo();
        while (key.Key != ConsoleKey.Enter)
        {
            if (typed.Length > paragraph.Length - 10)
{
    paragraph += " " + ParagraphGenerator.Generate(20);
}

            Console.SetCursorPosition(0, 2);
            RenderParagraph(paragraph, typed);

            key = Console.ReadKey(true);

            if (!timerStarted)
            {
                timerStarted = true;
                Timer.startWatch();
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (typed.Length > 0)
                    typed.Length--;

                continue;
            }

            if (char.IsControl(key.KeyChar))
                continue;

            if (typed.Length < paragraph.Length)
                typed.Append(key.KeyChar);
        }

        Console.CursorVisible = true;
        Console.ResetColor();
        Console.SetCursorPosition(0, 4);
        Console.WriteLine("\nCompleted!");

        Stopwatch sw = Timer.stopWatch();
        Result.wpmCalculator(sw, typed);
        Result.accuracyCalculator(typed, paragraph);
    }

    internal void freeStyleNoParagraph()
    {
        Console.Clear();
        Console.CursorVisible = true;
        Console.WriteLine("FREE STYLE NO PARAGRAPH\n");
        Console.WriteLine("Press ENTER to stop typing!");

        StringBuilder typed = new StringBuilder();
        bool timerStarted = false;

        Console.SetCursorPosition(0, 3);

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (!timerStarted && key.Key != ConsoleKey.Enter)
            {
                timerStarted = true;
                Timer.startWatch();
            }

            if (key.Key == ConsoleKey.Enter)
                break;

            if (key.Key == ConsoleKey.Backspace)
            {
                if (typed.Length > 0)
                    typed.Length--;

                RedrawTypedText(typed);
                continue;
            }

            if (char.IsControl(key.KeyChar))
                continue;

            typed.Append(key.KeyChar);
            RedrawTypedText(typed);
        }

        Console.ResetColor();
        Console.WriteLine("\n\nCompleted!");

        Stopwatch sw = Timer.stopWatch();
        Result.wpmCalculator(sw, typed);
    }

    internal void timeBasedParagraph(int timeSeconds)
{
    Console.Clear();
    Console.CursorVisible = false;
    Console.WriteLine("TIME BASED PARAGRAPH\n");

    string paragraph = ParagraphGenerator.Generate(30);

    StringBuilder typed = new StringBuilder();
    Stopwatch sw = Stopwatch.StartNew();

    while (sw.Elapsed < TimeSpan.FromSeconds(timeSeconds))
    {
        if (typed.Length > paragraph.Length - 10)
        {
            paragraph += " " + ParagraphGenerator.Generate(20);
        }

        Console.SetCursorPosition(0, 3);
        RenderParagraph(paragraph, typed);

        int remaining = timeSeconds - (int)sw.Elapsed.TotalSeconds;
        if (remaining < 0) remaining = 0;

        Console.SetCursorPosition(0, 1);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"Remaining Time: {remaining}s   ");
        Console.ResetColor();

        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace)
            {
                if (typed.Length > 0)
                    typed.Length--;
            }
            else if (!char.IsControl(key.KeyChar))
            {
                typed.Append(key.KeyChar);
            }
        }
        else
        {
            Thread.Sleep(10);
        }
    }

    sw.Stop();
    Console.CursorVisible = true;

    Console.ResetColor();
    Console.SetCursorPosition(0, 5);
    Console.WriteLine("\nTime UP!");

    Result.wpmCalculator(sw, typed);
    Result.accuracyCalculator(typed, paragraph);
}

}
