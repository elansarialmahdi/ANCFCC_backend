using EmailCodeVerificationAPI.Models;
using EmailCodeVerificationAPI.Services;

using Microsoft.AspNetCore.Mvc;

namespace EmailCodeVerificationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VerificationController : ControllerBase
    {
        private readonly CodeGeneratorService _codeService;
        private readonly EmailService _emailService;

        public VerificationController(CodeGeneratorService codeService, EmailService emailService)
        {
            _codeService = codeService;
            _emailService = emailService;
        }

        [HttpPost("generate")]
        public IActionResult Generate([FromBody] EmailRequest request)
        {
            try
            {
                var code = _codeService.GenerateCode();
                _emailService.SendEmail(request.ToEmail, code);
                return Ok(new { message = "Verification code sent to email." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("validate")]
        public IActionResult Validate([FromBody] CodeValidationRequest request)
        {
            if (_codeService.ValidateCode(request.Code))
            {
                _codeService.InvalidateCode();
                return Ok(new { success = true, message = "✅ Code is valid!" });
            }

            return BadRequest(new { success = false, message = "❌ Invalid or expired code." });
        }
    }
}
