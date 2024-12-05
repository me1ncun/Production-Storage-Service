namespace Storage.Application.Helpers;

public static class ApiKeyHelper
{
    private static int length = 16;

    public static string GetUniqueKey()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        
        Random random = new Random();
        
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}