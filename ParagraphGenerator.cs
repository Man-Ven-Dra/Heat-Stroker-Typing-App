using System;
using System.Text;

static class ParagraphGenerator
{
    private static readonly Random _rand = new Random();

    internal static string Generate(int wordCount)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < wordCount; i++)
        {
            string word = WordRepository.Words[
                _rand.Next(WordRepository.Words.Length)
            ];

            sb.Append(word);

            if (i < wordCount - 1)
                sb.Append(' ');
        }

        return sb.ToString();
    }
}
