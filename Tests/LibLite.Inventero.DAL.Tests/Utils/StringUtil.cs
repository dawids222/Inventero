namespace LibLite.Inventero.DAL.Tests.Utils
{
    internal static class StringUtil
    {
        const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string RandomString(int length)
        {
            return new string(Enumerable.Repeat(CHARS, length)
                .Select(s => s[Random.Shared.Next(s.Length)])
                .ToArray());
        }

        public static string GetRandomString()
        {
            var length = Random.Shared.Next(20);
            return RandomString(length);
        }
    }
}
