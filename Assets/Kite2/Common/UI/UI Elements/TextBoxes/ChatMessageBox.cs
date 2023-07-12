using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatMessageBox : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public Image backgroundImage;
    public Sprite[] sprites;
    public float height;

    public void SetMessage(string message)
    {
        bool active = SetInactiveIfMessageIsNull(message);

        if (!active) { return; }
 
        textBox.text = message;
        SetFittingBackGroundImage(message);
    }

    public bool SetInactiveIfMessageIsNull(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            this.gameObject.SetActive(false);
            return false;
        }
        else
        {
            this.gameObject.SetActive(true);
            return true;
        }
    }

    public void SetFittingBackGroundImage(string message)
    {
        switch (CalculateNumberOfLines(message))
        {
            case 1:
                {
                    height = 101;
                    GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 101);
                    backgroundImage.sprite = sprites[0];
                    break;
                }
            case 2:
                {
                    height = 131;
                    GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 131);
                    backgroundImage.sprite = sprites[1];
                    break;
                }
            case 3:
                {
                    height = 161;
                    GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 161);
                    backgroundImage.sprite = sprites[2];
                    break;
                }
            case 4:
                {
                    height = 191;
                    GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 191);
                    backgroundImage.sprite = sprites[3];
                    break;
                }
            case 5:
                {
                    height = 221;
                    GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 221);
                    backgroundImage.sprite = sprites[4];
                    break;
                }
            case 6:
                {
                    height = 251;
                    GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 251);
                    backgroundImage.sprite = sprites[5];
                    break;
                }
            case 7:
                {
                    height = 281;
                    GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 281);
                    backgroundImage.sprite = sprites[6];
                    break;
                }
            default:
                {
                    height = 311;
                    GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 311);
                    backgroundImage.sprite = sprites[7];
                    break;
                }
        }
    }

    public static int CalculateNumberOfLines(string str)
    {
        const int charsPerLine = 41;
        int stringLength = str.Length;

        return (stringLength + charsPerLine - 1) / charsPerLine;
    }
}
