using System;
using UnityEngine;

[Serializable]
public class ReviewObserver
{
    [SerializeField] private long id;
    [SerializeField] private string email;

    public void SetId(long id)
    {
        this.id = id;
    }

    public long GetId()
    {
        return id;
    }

    public void SetEmail(string email)
    {
        this.email = email;
    }

    public string GetEmail()
    {
        return email;
    }
}
