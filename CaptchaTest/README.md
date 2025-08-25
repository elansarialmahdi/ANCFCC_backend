# ASP.NET Core CAPTCHA Web API

This is a simple ASP.NET Core Web API that generates CAPTCHA images and allows users to validate them. The CAPTCHA system includes:

- Image generation with text
- Base64 image delivery
- CAPTCHA ID management
- Validation endpoint
- (Optional) Rate limiting per IP
-  noise/lines in CAPTCHA images for bot resistance
- distributed caching (Redis) for scale
- color randomizer 
---

## 📦 Requirements

- .NET 7+
- Visual Studio or VS Code
- NuGet packages:
  - `Microsoft.AspNetCore.RateLimiting` (for rate limiting)
  - `SixLabors.ImageSharp` (for image generation)

---

## 🚀 API Endpoints

🧪 How to Test in Postman
Send a POST request to /captcha/generate

Send a POST request to /captcha/validate

Use the correct captchaId and the code you read from the image

