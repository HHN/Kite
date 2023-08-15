using System;

[Serializable]
public class ChangePasswordRequest
{
    public string oldPassword;
    public string newPassword;
}
