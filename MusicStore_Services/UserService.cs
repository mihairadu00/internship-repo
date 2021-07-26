using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicStore_Common;
using MusicStore_Common.Entities;
using MusicStore_Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore_Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly AuthorizationSettings _authorizationSettings;
        //private readonly byte[] _salt;

        public UserService(IUserRepository userRepository, IOptions<AuthorizationSettings> appSettings)
        {

            _userRepository = userRepository;

            _authorizationSettings = appSettings.Value;

            //_salt = new byte[128 / 8];
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(_salt);
            //}

            //_userRepository.Add(new User { FirstName = "User1", LastName = "User1", Username = "usertest1", PasswordHash = HashPassword("user1"), UserRole = "User" });
            //_userRepository.Add(new User { FirstName = "Admin1", LastName = "Admin1", Username = "admintest1", PasswordHash = HashPassword("admin1"), UserRole = "Admin" });

        }

        /// <summary>
        /// Authenticate a user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> Authenticate(string username, string password)
        {

            var salt = await _userRepository.GetUsersSaltByUsernameAsync(username);

            var user = await _userRepository.AuthenticateAsync(username, HashPassword(password, salt));
            return user == null ? null : GenerateJwtToken(user);
        
        }

        /// <summary>
        /// Register a new user in the system
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> Register(string firstName, string lastName, string username, string password)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            if (existingUser != null)
                return null;

            var salt = Guid.NewGuid().ToString();

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                PasswordHash = HashPassword(password, salt),
                UserRole = "User",
                Salt = salt
            };

            return await _userRepository.AddAsync(newUser);

        }

        /// <summary>
        /// Returns the user having the coresponding username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        /// <summary>
        /// Generates a new access token for an already logged in user to extend his session
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> RenewAccessToken(User user)
        {

            var verifiedUser = await _userRepository.GetUserByUsernameAsync(user.Username);
            return verifiedUser == null ? null : GenerateJwtToken(verifiedUser);
        
        }

        /// <summary>
        /// Generate a token that is valid for a pre-defined number of minutes.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateJwtToken(User user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authorizationSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
               
                new Claim(JwtRegisteredClaimNames.Sub, user.Username.ToString()),
                new Claim("id", user.Id.ToString()),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim("role",user.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
         
            };

            var token = new JwtSecurityToken(
               
                issuer: _authorizationSettings.Issuer,
                audience: _authorizationSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials

            );

            return new JwtSecurityTokenHandler().WriteToken(token);
       
        }

        private string HashPassword(string password, string salt)
        {

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(

                password: password,
                salt: Encoding.ASCII.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8)
                
            );

        }

    }
}
