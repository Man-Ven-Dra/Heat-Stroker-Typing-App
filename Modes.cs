class Modes
{
    internal static string? modeChoice;

    internal void freeStyleModes()
    {
        Console.WriteLine("Select Free Style Mode:");
        Console.WriteLine(" 1. Paragraph\n 2. No Paragraph");

        char choice = Console.ReadKey(true).KeyChar;
        modeChoice = "1" + choice;

        Console.WriteLine("\nMode selected: " + modeChoice);
    }

    internal void timeBasedModes()
    {
        Console.WriteLine("Select Time Based Mode:");
        Console.WriteLine(" 1. 30 Sec\n 2. 60 Sec\n 3. 2 Min\n 4. 5 Min\n 5. 10 Min");

        char choice = Console.ReadKey(true).KeyChar;
        modeChoice = "2" + choice;

        Console.WriteLine("\nMode selected: " + modeChoice);
    }

    internal void modeChooser()
    {
        Console.WriteLine("SELECT MODE");
        Console.WriteLine(" 1. Free Style\n 2. Time Based");

        char choice = Console.ReadKey(true).KeyChar;

        switch (choice)
        {
            case '1':
                freeStyleModes();
                break;

            case '2':
                timeBasedModes();
                break;

            default:
                Console.WriteLine("\nInvalid Input!");
                break;
        }
    }
}