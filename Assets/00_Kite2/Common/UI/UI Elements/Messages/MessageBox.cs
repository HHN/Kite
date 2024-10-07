using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageBox : MonoBehaviour
{
    public TextMeshProUGUI messageBoxHeadline;
    public TextMeshProUGUI messageBoxBody;
    public Button okButton;
    [SerializeField] private AudioSource errorSound;
    [SerializeField] private AudioSource infoSound;
    [SerializeField] private bool isErrorMessage;

    void Start()
    {
        okButton.onClick.AddListener(delegate { OnOkButton(); });
        FontSizeManager.Instance().UpdateAllTextComponents();
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
        if (this.isErrorMessage)
        {
            this.errorSound.Play();
        } else
        {
            this.infoSound.Play();
        }
        this.gameObject.SetActive(true);
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

    public void SetIsErrorMessage(bool isErrorMessage)
    {
        this.isErrorMessage = isErrorMessage;
    }
}
