using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DatingApp.API.Data
{

    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context = null;

        public AuthRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public AuthRepository()
        {
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.InsertOneAsync(user);
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            };


        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.Find(x => x.Username == username).FirstOrDefaultAsync();
            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            //auth sucessful
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {

                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            };
            return true;
        }

        public async Task<bool> UserExcits(string username)
        {
            if (await _context.Users.Find(x=> x.Username == username).AnyAsync() )
            {
                return true;
            }
            return false;
        }
    }
}
