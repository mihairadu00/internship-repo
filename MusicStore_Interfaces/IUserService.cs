using MusicStore_Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore_Interfaces
{
    public interface IUserService
    {

        /// <summary>
        /// Authenticate a user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> Authenticate(string username, string password);

        /// <summary>
        /// Register a new user in the system
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> Register(string firstName, string lastName, string username, string password);

        /// <summary>
        /// Generates a new access token for an already logged in user to extend his session
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> RenewAccessToken(User user);

        /// <summary>
        /// Returns the user having the coresponding username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<User> GetUserByUsername(string username);

    }
}
