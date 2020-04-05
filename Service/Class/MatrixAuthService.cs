using System;
using System.Collections.Generic;
using System.Linq;
using Matrix.Client;
using Matrix.Structures;
using MatrixAuth.Service.Interface;

namespace MatrixAuth.Service.Class
{
    public class MatrixAuthService : IMatrixAuthService
    {
        private IDictionary<string, MatrixClient> _matrixClients = new Dictionary<string, MatrixClient>();

        public bool UserExists(string username)
        {
            if(_matrixClients != null)
            {
                return _matrixClients.ContainsKey(username);
            }

            return false;
        }

        public bool ValidateUser(string homeserver, string username, string token)
        {
            if (homeserver != null && username != null && token != null)
            {
                if (_matrixClients.ContainsKey(username))
                {
                    var resp = _matrixClients[username].GetCurrentLogin();
                    if (resp.access_token == token && homeserver == resp.home_server)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public IList<MatrixRoom> GetRooms(string username)
        {
            if (_matrixClients != null && username.Length > 0 && _matrixClients.ContainsKey(username))
            {
                return new List<MatrixRoom>(_matrixClients[username].GetAllRooms());
            }

            return null;
        }

        public string GetToken(string username)
        {
            if(_matrixClients != null && username.Length > 0 && _matrixClients.ContainsKey(username))
            {
                return _matrixClients[username].GetAccessToken();
            }

            return null;
        }

        public MatrixLoginResponse LoginWithPassword(string homeserver, string username, string password)
        {
            MatrixLoginResponse response = null;
            if (homeserver.Length > 0 && username.Length > 0)
            {
                if (_matrixClients == null)
                {
                    _matrixClients = new Dictionary<string, MatrixClient>();
                }

                if (!_matrixClients.Keys.Contains(username))
                {
                    try
                    {
                        _matrixClients[username] = new MatrixClient(homeserver);

                        var passwordHash = password;//Utils.HashUtil.GetSha256FromString(password); //TODO: turn this back on
                        response = _matrixClients[username].LoginWithPassword(username, passwordHash);
                    }
                    catch (Exception e)
                    {
                        response = null;
                        Console.WriteLine($"Error encountered while password-logging in user {username}: {e.Message}");
                    }
                }
                else
                {
                    response = _matrixClients[username].GetCurrentLogin();
                }

            }

            return response;
        }

        public MatrixLoginResponse LoginWithToken(string homeserver, string username, string token)
        {
            MatrixLoginResponse response = null;
            if (homeserver.Length > 0 && username.Length > 0)
            {
                if (_matrixClients == null)
                {
                    _matrixClients = new Dictionary<string, MatrixClient>();
                }

                if (!_matrixClients.Keys.Contains(username))
                {
                    try
                    {
                        _matrixClients[username] = new MatrixClient(homeserver);

                        _matrixClients[username].UseExistingToken(username, token);
                        //_matrixClients[username].LoginWithToken(username, token);
                        response = _matrixClients[username].GetCurrentLogin();
                    }
                    catch (Exception e)
                    {
                        response = null;
                        Console.WriteLine($"Error encountered while token-logging in user {username}: {e.Message}");
                    }
                }
                else
                {
                    _matrixClients[username].UseExistingToken(username, token);
                    response = _matrixClients[username].GetCurrentLogin();
                }
                
            }

            return response;
        }

        public string[] ListUsers()
        {
            if(_matrixClients.Any())
            {
                return new List<string>(_matrixClients.Keys).ToArray();
            }

            return null;
        }
    }
}
