using System;

namespace Assets._Scripts.ServerCommunication.RequestObjects
{
    [Serializable]
    public class GetUserRoleByCodeRequest
    {
        public string code;
    }
}