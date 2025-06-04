using System.ComponentModel.DataAnnotations;
namespace AFAP.EndPointMVCPattern.CustomValidators
{
    public class ChackingString: ValidationAttribute
    {
        public string  Input { get; set; }
        public string DefaultErrorMessage { get; set; } = "لطفا حروف وارد کنید";
        public ChackingString(string input)
        {

        }
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
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                bool temp = Verifystring(Input);
                if (temp==false)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

            return null;
        }
    }
}
