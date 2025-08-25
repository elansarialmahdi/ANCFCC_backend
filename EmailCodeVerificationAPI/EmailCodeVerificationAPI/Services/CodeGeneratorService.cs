namespace EmailCodeVerificationAPI.Services
{
    public class CodeGeneratorService
    {
        private string? _lastGeneratedCode;
        private DateTime? _expiryTime;

        public string GenerateCode()
        {
           
            var code = new Random().Next(10000, 99999).ToString();
            _lastGeneratedCode = code;
            _expiryTime = DateTime.UtcNow.AddMinutes(5); 
            return code;
        }

        public bool ValidateCode(string code)
        {
            if (_lastGeneratedCode == null || _expiryTime == null)
                return false;

            if (DateTime.UtcNow > _expiryTime.Value)
                return false; 

            return _lastGeneratedCode == code;
        }

        public void InvalidateCode()
        {
            _lastGeneratedCode = null;
            _expiryTime = null;
        }
    }
}
