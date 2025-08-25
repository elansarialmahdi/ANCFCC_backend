
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

[ApiController]
[Route("api/[controller]")]
public class CaptchaController : ControllerBase
{
    private readonly CaptchaService _captchaService;
    private readonly IMemoryCache _cache;

    public CaptchaController(CaptchaService captchaService, IMemoryCache cache)
    {
        _captchaService = captchaService;
        _cache = cache;
    }

    [HttpGet("generate")]
    public IActionResult Generate()
    {
        var captcha = _captchaService.GenerateCaptcha();

        
        _cache.Set(captcha.CaptchaId, captcha.Answer, TimeSpan.FromMinutes(2));
        var base64Image = Convert.ToBase64String(captcha.ImageBytes);

        return Ok(new
        {
            captchaId = captcha.CaptchaId,
            imageBase64 = base64Image
        });
        //var captcha = _captchaService.GenerateCaptcha();

        //// Store the answer in memory for 2 minutes
        //_cache.Set(captcha.CaptchaId, captcha.Answer, TimeSpan.FromMinutes(2));

        //return File(captcha.ImageBytes, "image/png", enableRangeProcessing: false);
    }


    public class CaptchaValidationRequest
    {
        public string CaptchaId { get; set; }
        public string UserInput { get; set; }
    }

    [HttpPost("validate")]
    public IActionResult Validate([FromBody] CaptchaValidationRequest request)
    {
        if (_cache.TryGetValue(request.CaptchaId, out string correctAnswer))
        {
            if (request.UserInput.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
            {
                _cache.Remove(request.CaptchaId);
                return Ok(new { success = true, message = "CAPTCHA valid" });
            }
            return BadRequest(new { success = false, message = "Incorrect CAPTCHA" });
        }

        return BadRequest(new { success = false, message = "CAPTCHA expired or invalid" });
    }


}
