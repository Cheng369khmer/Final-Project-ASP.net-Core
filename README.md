# MyPortfolio — ASP.NET Core Razor Pages

## 🚀 Quick Start

### Step 1: Setup XAMPP Database
1. Start **XAMPP** → Start **Apache** and **MySQL**
2. Open **phpMyAdmin**: http://localhost/phpmyadmin
3. Click **SQL** tab → paste contents of `setup_database.sql` → click **Go**

### Step 2: Restore NuGet Packages
```powershell
# Clear cache first (fixes "file already exists" error)
dotnet nuget locals all --clear
Remove-Item -Recurse -Force bin, obj -ErrorAction SilentlyContinue
dotnet restore
```

### Step 3: Run the App
```powershell
dotnet watch run
```

Open browser: **https://localhost:5001** or **http://localhost:5000**

---

## 👤 Demo Accounts

| Role  | Email                    | Password  |
|-------|--------------------------|-----------|
| Admin | admin@portfolio.com      | admin123  |
| User  | sophea@gmail.com         | user123   |
| User  | dara@gmail.com           | user123   |
| User  | malis@gmail.com          | user123   |
| User  | ratha@gmail.com          | user123   |
| User  | sreyla@gmail.com         | user123   |

---

## 📁 Project Structure

```
MyPortfolio/
├── Data/
│   ├── AppDbContext.cs       # EF Core DbContext
│   └── DbSeeder.cs           # Auto-seeds DB on first run
├── Models/
│   └── UserModel.cs
├── Pages/
│   ├── Admin/
│   │   ├── Dashboard.cshtml  # Admin stats + user table
│   │   └── Users.cshtml      # Manage users (delete)
│   ├── Shared/
│   │   └── _Layout.cshtml    # Main layout + navbar
│   ├── Index.cshtml          # Home (public)
│   ├── About.cshtml
│   ├── Portfolio.cshtml
│   ├── Resume.cshtml
│   ├── Contact.cshtml
│   ├── Privacy.cshtml
│   ├── Login.cshtml
│   ├── Register.cshtml
│   └── Logout.cshtml
├── wwwroot/
│   ├── css/site.css
│   └── js/site.js
├── appsettings.json
├── Program.cs
├── setup_database.sql        # Run this in phpMyAdmin
└── MyPortfolio.csproj
```

---

## 🔐 Role System

| Feature          | Guest | User | Admin |
|-----------------|-------|------|-------|
| Home page       | ✅    | ✅   | ✅    |
| About/Portfolio | ✅    | ✅   | ✅    |
| Login/Register  | ✅    | ✅   | ✅    |
| Hello! [Name]   | ❌    | ✅   | ✅    |
| Admin Dashboard | ❌    | ❌   | ✅    |
| Manage Users    | ❌    | ❌   | ✅    |
