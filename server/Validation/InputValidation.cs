using System.Text.RegularExpressions;

namespace WAMServer.Validation
{
    public static class InputValidation
    {
        public static bool IsValidEmail(string input)
        {
            if(input.Length > 2 && input.Length < 320)
            {
                if (Regex.IsMatch(input, """^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"""))
                {
                    return true;
                }
            }
            return false;
        }
    }
}