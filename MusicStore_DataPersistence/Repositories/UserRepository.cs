using Microsoft.EntityFrameworkCore;
using MusicStore_Common.Entities;
using MusicStore_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore_DataPersistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MusicStoreDbContext _context;

        public UserRepository(MusicStoreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add the new user to the collection.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> AddAsync(User user)
        {

            if (await GetUserByUsernameAsync(user.Username) != null)
                return null;

            user.Id = Guid.NewGuid();

            await _context.AddAsync(user);

            await _context.SaveChangesAsync();

            return user;

        }

        /// <summary>
        /// verify if the credentials are valid
        /// </summary>
        /// <param name="username"></param>
        /// <param name="hashedPassword"></param>
        /// <returns></returns>
        public async Task<User> AuthenticateAsync(string username, string hashedPassword)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.Username == username && x.PasswordHash == hashedPassword);
        }

        /// <summary>
        /// Returns the user havin the coresponding username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        /// <summary>
        /// Returns the password salt for the user having the coresponding username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<string> GetUsersSaltByUsernameAsync(string username)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            return user.Salt;

        }

    }
}
