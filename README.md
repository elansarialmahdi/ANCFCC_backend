# ANCFCC_backend
# ASP.NET Core Microservices Backend

This repository contains a set of **independent microservices**, each implemented in ASP.NET Core.  
⚠️ **Important:** The main branch only provides documentation. The actual services are located in their dedicated branches.

---

## 📂 Repository Structure

The project is organized into three branches, each corresponding to a specific microservice:

1. **`title-service` branch**  
   - Provides an API for managing land titles
   - Implements CRUD operations and follows REST best practices.

2. **`email-verification-service` branch**  
   - Handles sending and validating **email verification codes**.  
   - Useful for user sign-up, login security, and account recovery.  
   - Includes basic rate-limiting to prevent abuse.

3. **`captcha-service` branch**  
   - Provides an API for **CAPTCHA generation and validation**.  
   - Helps secure forms against bots and automated requests.  

---

## 🚀 How to Use

1. Clone the repository:
   ```bash
   git clone https://github.com/<your-username>/<repo-name>.git
   ------------------------------------------------------------------------------------------------------------------
   📌 Notes

Each service is independent and can be run/deployed separately.

The repository uses branches instead of folders to isolate each service.

Contributions should be made to the relevant branch, not the main one.
   
