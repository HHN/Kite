using UnityEngine;
using UnityEngine.UI;
using LeastSquares.Overtone;

public class DeleteUserDataConfirmation : MonoBehaviour
{

    [SerializeField] private TTSEngine engine;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button confirmButton;

    void Start()
    {
        closeButton.onClick.AddListener(delegate { OnCancleButton(); });
        cancelButton.onClick.AddListener(delegate { OnCancleButton(); });
        confirmButton.onClick.AddListener(delegate { OnConfirmButton(); });
    }
    public void OnConfirmButton()
    {
        TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.DELETED_DATA, engine);
        PlayerDataManager.Instance().ClearRelevantUserdata();
        if(AnalyticsServiceHandler.Instance().IsAnalyticsInitialized())
        {
           AnalyticsServiceHandler.Instance().DeleteCollectedData(); 
        }
        CloseMessageBox();
    }

    public void OnCancleButton()
    {
        CloseMessageBox();
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
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
