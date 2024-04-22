using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CloseNovelAndGoBackMessageBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageBoxHeadline;
    [SerializeField] private TextMeshProUGUI messageBoxBody;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button confirmButton;

    void Start()
    {
        closeButton.onClick.AddListener(delegate { OnCloseButton(); });
        cancelButton.onClick.AddListener(delegate { OnCancleButton(); });
        confirmButton.onClick.AddListener(delegate { OnConfirmButton(); });
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

    public void OnCancleButton()
    {
        this.CloseMessageBox();
    }

    public void OnConfirmButton()
    {
        SceneLoader.LoadMainMenuScene();
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
