using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DatingApp.API.Databases;
using DatingApp.API.Databases.Entities;

namespace DatingApp.API.Extensions
{
    public class DataSeeder
    {
        public static void SeedData(DataContext context)
        {
            if (context.Users.Any()) return;
            var usersJson = File.ReadAllText("Databases/users.json");
            var users = JsonSerializer.Deserialize<List<User>>(usersJson);
            if (users is null || !users.Any()) return;
            var passwordBytes = Encoding.UTF8.GetBytes("Password$");
            foreach (var user in users)
            {
                using var hashFunc = new HMACSHA256();
                user.PasswordHash = hashFunc.ComputeHash(passwordBytes);
                user.PasswordSalt = hashFunc.Key;
            }
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}