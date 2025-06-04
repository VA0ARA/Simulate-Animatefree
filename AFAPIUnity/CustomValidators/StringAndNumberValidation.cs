namespace AFAPIUnity.CustomValidators
{
    public class StringAndNumberValidation
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
        public bool VerifyPhoneNumber(string number)
        {
            string isitdigit = "";
            for (int i = 1; i < number.Length; i++)
            {
                isitdigit += number[i];
            }
            long j = 0;
            bool result = long.TryParse(isitdigit, out j);
            string Phone = number;
            string temp = "";
            for (int i = 0; i < 2; i++)
            {
                temp += Phone[i];
            }
            if (temp == "09" && Phone.Length == 11 && result == true)
            {
                return true;
            }
            return false;
        }
    }
}
