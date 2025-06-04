namespace AFAP.EndPointMVCPattern.CustomValidators
{
    public class StringValidation
    {
        public bool Verifystring(string input)
        {
            string isitdigit = input;
            long j = 0;
            bool result = long.TryParse(isitdigit, out j);
            if (result == true)
            {
                return true;
            }
            return false;
        }
    }
}
