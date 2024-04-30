namespace FastFood.Helper
{
    using System.Globalization;
    public static class StringExtensions
    {
        public static string CapitalizeEachWord(this string s)
        {
            s = s.ToLower();
            TextInfo txt = new CultureInfo("en-US", false).TextInfo;
            return txt.ToTitleCase(s);
        }
    }
}
