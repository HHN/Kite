using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTextManager
{
    private string textHead;
    private string textBody;

    private static InfoTextManager instance;

    private InfoTextManager()
    {
    }

    public static InfoTextManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new InfoTextManager();
            }
            return instance;
        }
    }

    public void SetTextHead(string textHead)
    {
        this.textHead = textHead;
    }

    public string GetTextHead()
    {
        return textHead;
    }

    public void SetTextBody(string textBody)
    {
        this.textBody = textBody;
    }

    public string GetTextBody()
    {
        return textBody;
    }
}
