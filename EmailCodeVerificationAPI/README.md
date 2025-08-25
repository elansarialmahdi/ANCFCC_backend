# EmailCodeVerificationAPI
****Project Structure
EmailVerificationAPI/
 ├── Controllers/
 │    └── VerificationController.cs
 ├── Models/
 │    ├── EmailRequest.cs
 │    └── CodeValidationRequest.cs
 ├── Services/
 │    ├── CodeGeneratorService.cs
 │    └── EmailService.cs
 ├── Settings/
 │    └── EmailSettings.cs
 ├── Program.cs
 └── appsettings.json
--------------------------------------------------------------------------------------------------------
****Description
Program.cs (loads EmailSettings, registers services)

EmailService.cs (sends email using settings)

CodeGeneratorService.cs (creates + validates 5-digit code with 5 min expiry)

VerificationController.cs (two endpoints: generate + validate)

Models (EmailRequest.cs, CodeValidationRequest.cs)
---------------------------------------------------------------------------------------------------------
****Generate code:
POST https://localhost:5001/api/verification/generate
Content-Type: application/json

{
  "toEmail": "receiver@example.com"
}
-----------------------------------------------------------------------------------
*****Validate code:
POST https://localhost:5001/api/verification/validate
Content-Type: application/json

{
  "code": "48291"
}
--------------------------------------------------------------------------------------------------------
**** Configure Gmail****

Google blocks “less secure apps”, so you must use an App Password.
1-Go to Google Account Security
2-Enable 2-Step Verification if not already.
3-Under Security → App passwords, generate a new one:
   +Select app: Mail
   +Copy the 16-character app password (looks like abcd efgh ijkl mnop)
Keep this safe — it replaces your Gmail password in code.
