using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowPasswordButton : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button button;

    void Start()
    {
        button.onClick.AddListener(delegate { OnClick(); });
    }

    public void OnClick()
    {
        if (inputField.contentType == TMP_InputField.ContentType.Password)
        {
            inputField.contentType = TMP_InputField.ContentType.Standard;
        } 
        else if (inputField.contentType == TMP_InputField.ContentType.Standard)
        {
            inputField.contentType = TMP_InputField.ContentType.Password;
        }
        inputField.ForceLabelUpdate();
    }
}
