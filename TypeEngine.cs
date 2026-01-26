using System.Diagnostics;
using System.Text;

class TypeEngine
{
    internal enum CharState
    {
        NotTyped,
        Correct,
        Incorrect,
        Current
    }

    internal CharState getCharState(int i, StringBuilder typed, string paragraph)
    {
        if(i == typed.Length)
            return CharState.Current;

        if (i >= typed.Length)
            return CharState.NotTyped;

        if(typed[i] == paragraph[i])
        {
            return CharState.Correct;
        }
        
        return CharState.Incorrect;
    }

    internal void setColor(CharState state)
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

    internal void renderPara(string paragraph, StringBuilder typed)
    {
        for (int i = 0; i < paragraph.Length; i++)
        {
            CharState state = getCharState(i, typed, paragraph);
            setColor(state);
            Console.Write(paragraph[i]);
        }
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
        Console.WriteLine("FREE STYLE PARAGRAPH\n");

        string paragraph =
            "Hello! Welcome to Heat Stroker, the ultimate Typing application, you are currently trying the Free Style Paragraph Mode.";

        StringBuilder typed = new StringBuilder();

        bool flag = false;

        while (typed.Length < paragraph.Length)
        {
            Console.SetCursorPosition(0, 2);
            renderPara(paragraph, typed);

            ConsoleKeyInfo key = Console.ReadKey(true);
            if (!flag)
            {
                flag=true;
                Timer.startWatch();
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (typed.Length > 0)
                    typed.Remove(typed.Length - 1, 1);

                continue;
            }

            if (char.IsControl(key.KeyChar))
                continue;

            typed.Append(key.KeyChar);
        }

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

    internal void timeBasedParagraph(int time)
    {
        Console.Clear();
        Console.CursorVisible = false;
        Console.WriteLine("TIME BASED PARAGRAPH\n");

        string paragraph =
            "Hello! Welcome to Heat Stroker, the ultimate Typing application, you are currently trying the Free Style Paragraph Mode.";

        StringBuilder typed = new StringBuilder();

        Stopwatch sw = Stopwatch.StartNew();
        while (sw.Elapsed<TimeSpan.FromSeconds(time) && typed.Length < paragraph.Length)
        {
            Console.SetCursorPosition(0, 2);
            renderPara(paragraph, typed);
            Console.SetCursorPosition(typed.Length, 2);

            if (!Console.KeyAvailable)
                continue;
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace)
            {
                if (typed.Length > 0)
                    typed.Remove(typed.Length - 1, 1);

                continue;
            }

            if (char.IsControl(key.KeyChar))
                continue;

            typed.Append(key.KeyChar);
        }

        sw.Stop();
        Console.CursorVisible = true;
        
        Console.ResetColor();
        Console.SetCursorPosition(0, 4);
        Console.WriteLine("\nTime UP!");

        Result.wpmCalculator(sw, typed);
        Result.accuracyCalculator(typed, paragraph);
    }
}

    