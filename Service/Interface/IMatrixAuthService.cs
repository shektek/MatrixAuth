using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Matrix.Client;
using Matrix.Structures;

namespace MatrixAuth.Service.Interface
{
    public interface IMatrixAuthService
    {
        /// <summary>
        /// Checks whether the user exists in the login array
        /// </summary>
        /// <param name="username">The user to check for</param>
        /// <returns>True if the user is logged in, false otherwise</returns>
        bool UserExists(string username);

        /// <summary>
        /// Check if the user has logged in through the service yet
        /// </summary>
        /// <param name="homeserver">homeserver the user should be logged in to</param>
        /// <param name="username">username to check for</param>
        /// <param name="token">token should match this</param>
        /// <returns>True if the user is logged in, false if not</returns>
        bool ValidateUser(string homeserver, string username, string token);
        
        /// <summary>
        /// Tries to login with this username and token
        /// </summary>
        /// <param name="username">username to log in with</param>
        /// <param name="token">the token the client wants to check</param>
        /// <returns>MatrixLoginResponse indicating success. Throws on bad credentials!</returns>
        MatrixLoginResponse LoginWithToken(string homeserver, string username, string token);

        /// <summary>
        /// Tries to login to this homeserver with this username and token
        /// </summary>
        /// <param name="homeserver">homeserver to login to, e.g. matrix.org</param>
        /// <param name="username">username to login with</param>
        /// <param name="password">this users password</param>
        /// <returns>MatrixLoginResponse indicating success. Throws on bad credentials!</returns>
        MatrixLoginResponse LoginWithPassword(string homeserver, string username, string password);

        string[] ListUsers();

        /// <summary>
        /// Get the list of rooms this user has access to
        /// </summary>
        /// <param name="username">The user to check rooms for</param>
        /// <returns>List of room names</returns>
        IList<MatrixRoom> GetRooms(string username);

        /// <summary>
        /// Get the access token for this user
        /// </summary>
        /// <param name="username">The user to get the access token for</param>
        /// <returns>The Matrix access token</returns>
        string GetToken(string username);
    }
}
