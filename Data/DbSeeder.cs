using MyPortfolio.Models;

namespace MyPortfolio.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Users.Any()) return; // Already seeded

            var users = new List<UserModel>
            {
                // Admin
                new UserModel { Name = "Admin", Email = "admin@portfolio.com", Gender = "Male",   Password = "admin123",  Role = "Admin", Remark = "System Administrator" },
                // 5 Normal Users
                new UserModel { Name = "Sophea Meas",   Email = "sophea@gmail.com",  Gender = "Female", Password = "user123", Role = "User", Remark = "Web Developer" },
                new UserModel { Name = "Dara Keo",      Email = "dara@gmail.com",    Gender = "Male",   Password = "user123", Role = "User", Remark = "UI/UX Designer" },
                new UserModel { Name = "Malis Chan",    Email = "malis@gmail.com",   Gender = "Female", Password = "user123", Role = "User", Remark = "Graphic Designer" },
                new UserModel { Name = "Ratha Sok",     Email = "ratha@gmail.com",   Gender = "Male",   Password = "user123", Role = "User", Remark = "Backend Developer" },
                new UserModel { Name = "Sreyla Pich",   Email = "sreyla@gmail.com",  Gender = "Female", Password = "user123", Role = "User", Remark = "Mobile Developer" },
            };

            db.Users.AddRange(users);
            db.SaveChanges();
        }
    }
}
