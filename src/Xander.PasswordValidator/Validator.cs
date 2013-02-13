namespace Xander.PasswordValidator
{
    public class Validator
    {
        private readonly int _minPasswordLength;
        public Validator(int minPasswordLength)
        {
            _minPasswordLength = minPasswordLength;
        }

        public bool Validate(string password)
        {
            if (password.Length >= _minPasswordLength)
                return true;
            return false;
        }
    }
}