class Program
{
    public static void modeSelector(string? choice)
    {
        Console.Clear();
        TypeEngine typer = new TypeEngine();
        
        switch (choice)
        {
            case "11":
                typer.freeStyleParagraph();
                break;
            case "12":
                typer.freeStyleNoParagraph();
                break;
            case "21":
                typer.timeBasedParagraph(30);
                break;
            case "22":
                typer.timeBasedParagraph(60);
                break;
            case "23":
                typer.timeBasedParagraph(120);
                break;
            case "24":
                typer.timeBasedParagraph(300);
                break;
            case "25":
                typer.timeBasedParagraph(600);
                break;
            default:
                break;
        }
    }
    public static void Main()
    {
        Modes mode = new Modes();
        mode.modeChooser();
        string? choice = Modes.modeChoice;

        modeSelector(choice);

    }
}
