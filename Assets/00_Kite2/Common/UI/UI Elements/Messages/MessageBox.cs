using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageBox : MonoBehaviour
{
    public TextMeshProUGUI messageBoxHeadline;
    public TextMeshProUGUI messageBoxBody;
    public Button closeButton;
    public Button okButton;

    void Start()
    {
        closeButton.onClick.AddListener(delegate { OnCloseButton(); });
        okButton.onClick.AddListener(delegate { OnOkButton(); });
    }

    public void SetHeadline(string headline)
    {
        messageBoxHeadline.text = headline;
    }

    public void SetBody(string headline)
    {
        messageBoxBody.text = headline;
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void OnCloseButton()
    {
        this.CloseMessageBox();
    }

    public void OnOkButton()
    {
        this.CloseMessageBox();
    }

    public void CloseMessageBox()
    {
        if (DestroyValidator.IsNullOrDestroyed(this) || DestroyValidator.IsNullOrDestroyed(this.gameObject))
        {
            return;
        }
        Destroy(this.gameObject);
    }
}
