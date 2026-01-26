using System.Diagnostics;
using System.Text;

class Result
{
    static internal void wpmCalculator(Stopwatch sw, StringBuilder typed)
    {
        if (sw.Elapsed.TotalMinutes == 0)
        {
            Console.WriteLine("WPM: 0");
            return;
        }

        int totalCharacters = typed.Length;

        double wordsTyped = totalCharacters / 5.0;
        double timeInMinutes = sw.Elapsed.TotalMinutes;

        double wpm = wordsTyped / timeInMinutes;

        Console.WriteLine($"Characters Typed : {totalCharacters}");
        Console.WriteLine($"Time Taken : {sw.Elapsed.TotalSeconds:F2} seconds");
        Console.WriteLine($"WPM : {Math.Round(wpm)}");
    }

    static internal void accuracyCalculator(StringBuilder typed, string paragraph)
    {
        int mistakes=0;
        int minLength = Math.Min(typed.Length, paragraph.Length);
        for(int i=0; i<minLength; i++)
        {
            if(typed[i] != paragraph[i])
            {
                mistakes++;
            }
        }
        //mistakes += Math.Abs(typed.Length - paragraph.Length);
        int totalCharacters = paragraph.Length;

        if (totalCharacters == 0){
            Console.WriteLine("Accuracy : 0%");
        return;
        }

        double accuracy = ((totalCharacters - mistakes) / (double)totalCharacters) * 100;
        Console.WriteLine($"Accuracy : {accuracy:F2}%");
    }
}