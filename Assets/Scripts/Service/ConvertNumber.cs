using System;

public static class ConvertNumber
{
    public static string Convert(float digit, string start = "", string end = "")
    {
        string[] typeValue = new[] { "", "k", "m", "b", "t" };

        int indexer = 0;
        while (indexer + 1 < typeValue.Length && digit >= 1000d)
        {
            digit /= 1000f;
            indexer++;
        }

        digit = MathF.Round(digit, 2);
        return $"{start}{digit}{typeValue[indexer]}{end}";
    }
}