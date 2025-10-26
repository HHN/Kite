// Assets/_Scripts/ServerCommunication/HttpAuth.cs
using System;
using System.Text;

namespace Assets._Scripts.ServerCommunication
{
    public static class HttpAuth
    {
        public static string BuildBasicAuthHeader(string username, string password)
        {
            var raw = $"{username}:{password}";
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(raw));
            return $"Basic {base64}";
        }
    }
}