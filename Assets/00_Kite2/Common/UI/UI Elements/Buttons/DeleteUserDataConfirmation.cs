using _00_Kite2.SaveNovelData;
using UnityEngine;
using UnityEngine.UI;
using LeastSquares.Overtone;
using TMPro;

public class DeleteUserDataConfirmation : MonoBehaviour
{

    [SerializeField] private TTSEngine engine;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject backgroundLeave;
    [SerializeField] private GameObject textCancle;
    [SerializeField] private GameObject person;
    [SerializeField] private GameObject uiContainer;
    [SerializeField] private TextMeshProUGUI messageText;

    private string origin;

    public void Initialize(string origin)
    {
        this.origin = origin;
        FontSizeManager.Instance().UpdateAllTextComponents();
    }

    void Start()
    {
        cancelButton.onClick.AddListener(delegate { OnCancleButton(); });
        confirmButton.onClick.AddListener(delegate { OnConfirmButton(); });
        if (origin == "delete")
        {
            messageText.text = InfoMessages.DELETE_DATA_CONFIRMATION;
        }
        if (origin == "reset")
        {
            messageText.text = InfoMessages.RESET_APP_CONFIRMATION;
        }
        AdjustImageSize();
        FontSizeManager.Instance().UpdateAllTextComponents();
    }

    private void AdjustImageSize()
    {
        float backgroundWidth = background.GetComponent<RectTransform>().rect.width;

        if (person.GetComponent<RectTransform>().rect.width > backgroundWidth)
        {
            person.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, backgroundWidth);
            person.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, backgroundWidth);
        }
    }

    public void OnConfirmButton()
    {
        if(origin == "delete")
        {
            TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.DELETED_DATA, engine);
            PlayerDataManager.Instance().ClearRelevantUserdata();
            if(AnalyticsServiceHandler.Instance().IsAnalyticsInitialized())
            {
                AnalyticsServiceHandler.Instance().DeleteCollectedData(); 
            }
            CloseMessageBox();
        }
        if (origin == "reset")
        {
            SaveLoadManager.ClearAllSaveData();

            TextToSpeechService.Instance().TextToSpeechReadLive(InfoMessages.RESET_APP, engine);
            PlayerDataManager.Instance().ClearEverything();
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            FavoritesManager.Instance().ClearFavorites();
            PlayRecordManager.Instance().ClearData();
            ShowPlayInstructionManager.Instance().SetShowInstruction(true);
            CloseMessageBox();
        }
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
