Botho University Clinic Management System - Full .NET 6 WinForms scaffold

Contents:
- BothoUniversityClinic.csproj
- Program.cs
- App.config (update connection string if needed)
- /Classes (DbConnection, TableInitializer, AuthManager, NotificationManager, FeedbackManager)
- /Forms (LoginForm, Dashboards, Messaging, Reports)
- /Database/BothoUniversityClinic.sql (create DB and tables)
- /Database/install_db.php (run under XAMPP to install DB automatically)

Setup steps:
1. Install XAMPP and start Apache & MySQL.
2. Copy the Database folder to C:\xampp\htdocs\mcpms_installer (or similar) and open http://localhost/mcpms_installer/install_db.php
   - Alternatively import BothoUniversityClinic.sql via phpMyAdmin.
3. Open the project in Visual Studio 2022+ (supports .NET 6). Restore NuGet packages.
4. Build and run. Sample users: admin/admin123, nurse1/nurse123, student1/student123
5. Use the "Install DB (XAMPP)" button on the login form to run the SQL script programmatically (requires MySql.Data script execution privileges).

Notes:
- For production, replace SHA2 password hashing with a salted algorithm (PBKDF2/BCrypt/Argon2).
- The UI is fully functional scaffold; you can extend visuals and polish as needed.
