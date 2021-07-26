using MusicStore_Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore_Interfaces
{
    public interface IUserRepository
    {

        /// <summary>
        /// verify if the credentials are valid
        /// </summary>
        /// <param name="username"></param>
        /// <param name="hashedPassword"></param>
        /// <returns></returns>
        Task<User> AuthenticateAsync(string username, string hashedPassword);

        /// <summary>
        /// Returns the user havin the coresponding username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<User> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Add the new user to the collection.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> AddAsync(User user);

        /// <summary>
        /// Returns the password salt for the user having the coresponding username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<string> GetUsersSaltByUsernameAsync(string username);

        Task<List<User>> GetAsync();
    }
}
