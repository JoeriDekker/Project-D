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

        public static bool IsValidPassword(string password)
        {
            if(password.Length >= 8 && password.Length <= 64)
            {
                if (Regex.IsMatch(password, """^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,64}$"""))
                {
                    return true;
                }
            }
            return false;
        }
    }
}