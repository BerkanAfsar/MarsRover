using System.Text.RegularExpressions;

namespace RoverNavigator
{
    public class InputValidator
    {
        public InputValidator() { }

        public bool IsValidFirstRoverInfo(string input)
        {
            string roverPattern = @"^\d+\s\d+\s[swenSWEN]$";

            var match = Regex.Match(input, roverPattern);

            if (match.Success)
                return true;
            else
                return false;
        }

        public bool IsValidInput(string input)
        {
            return IsValidRoverInfo(input) && NoInvalidChar(input);
        }

        private bool NoInvalidChar(string input)
        {
            string checkForPattern = @"[^swenmlrSWENMLR\d\n\s]";

            var match = Regex.Match(input, checkForPattern);

            if (match.Success)
                return false;
            else
                return true;
        }

        private bool IsValidRoverInfo(string input)
        {
            string checkForNumberPattern = @"\d";

            var match = Regex.Match(input, checkForNumberPattern);

            if (match.Success)
            {
                string roverPattern = @"(\s|^)\d+\s\d+\s[swenSWEN](\s|$)";

                match = Regex.Match(input, roverPattern);

                if (match.Success)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        public bool IsEnter(string input)
        {
            string pattern = "^\n$";
            var match = Regex.Match(input, pattern);

            if (match.Success)
                return true;
            else
                return false;
        }

        public bool IsValidUpperRightInput(string input)
        {
            string pattern = @"^\d+\s\d+$";

            var match = Regex.Match(input, pattern);

            if (match.Success)
                return true;
            else
                return false;
        }
    }
}
