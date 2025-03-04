using System;

namespace Assets._Scripts.Server_Communication.Request_Objects
{
    [Serializable]
    public class GetUserRoleByCodeRequest
    {
        public string code;
    }
}