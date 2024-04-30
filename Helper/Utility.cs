using System.Text.RegularExpressions;

namespace FastFood.Helper
{
    public static class Utility
    {
        public static bool IsValidEmail(string email)
        {    
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Use a regular expression pattern to match email addresses
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }
        private static string GetLogFileName()
        {
            string logsDirectory = "logs";
            if (!Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }

            string fileName = $"app_{DateTime.Now:yyyyMMdd}.log";
            return Path.Combine(logsDirectory, fileName);
        }

    }
}
