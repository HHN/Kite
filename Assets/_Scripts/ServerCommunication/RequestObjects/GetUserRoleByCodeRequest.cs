using System;

namespace Assets._Scripts.ServerCommunication.RequestObjects
{
    /// <summary>
    /// Represents a request object used to retrieve a user's role by providing a specific code.
    /// This is typically used for authentication or authorization where a code grants access to a role.
    /// The class is <see cref="Serializable"/> to facilitate conversion to formats like JSON for server communication.
    /// </summary>
    [Serializable]
    public class GetUserRoleByCodeRequest
    {
        public string code;
    }
}