using System;

public static class ExtentionMethods
{
    public static bool Contains(this string source, string value, StringComparison comparisonType)
    {
        return source?.IndexOf(value, comparisonType) >= 0;
    }
}
