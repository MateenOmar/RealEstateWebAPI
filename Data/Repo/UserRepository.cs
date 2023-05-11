using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;

        public UserRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public async Task<User> Authenticate(string userName, string passwordText)
        {
            var user = await dc.Users.FirstOrDefaultAsync(x => x.username == userName 
            //&& x.password == password
            );

            if(user == null || user.passwordKey == null) 
                return null;

            if(!MatchPasswordHash(passwordText, user.password, user.passwordKey))
                return null;

            return user;
        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
             using (var hmac = new HMACSHA512(passwordKey)) {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

                for(int i=0; i < passwordHash.Length; i++){
                    if(password[i] != passwordHash[i])
                        return false;
                }

                return true;
            }
        }

        public void Register(string userName, string password)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512()) {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            User user = new User();
            user.username = userName;
            user.password = passwordHash;
            user.passwordKey = passwordKey;

            dc.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await dc.Users.AnyAsync(x => x.username == userName);
        }
    }
}