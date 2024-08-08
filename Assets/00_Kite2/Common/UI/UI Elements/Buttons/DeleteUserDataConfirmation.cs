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

    void Start()
    {
        cancelButton.onClick.AddListener(delegate { OnCancleButton(); });
        confirmButton.onClick.AddListener(delegate { OnConfirmButton(); });
        InitUI();
    }

    private void InitUI()
    {
        Color color = new Color(0.07450981f, 0.1254902f, 0.2039216f, 1.000f);
        background.GetComponent<Image>().color = color;
        backgroundLeave.GetComponent<Image>().color = color;
        textCancle.GetComponent<TextMeshProUGUI>().color = color;
        RectTransform backgroundTransform = background.GetComponent<RectTransform>();
        backgroundTransform.anchoredPosition = new Vector2(backgroundTransform.anchoredPosition.x, (NovelColorManager.Instance().GetCanvasHeight()/2)-(backgroundTransform.rect.height/2));
        RectTransform uiContainerTransform = uiContainer.GetComponent<RectTransform>();
        uiContainerTransform.anchoredPosition = new Vector2(backgroundTransform.anchoredPosition.x, (NovelColorManager.Instance().GetCanvasHeight() / 2) - (backgroundTransform.rect.height / 2));
        Vector2 anchoredPosition = uiContainerTransform.anchoredPosition;
        anchoredPosition.y = 600;
        uiContainerTransform.anchoredPosition = anchoredPosition;
        RectTransform personTransform = person.GetComponent<RectTransform>();
        personTransform.anchoredPosition = new Vector2(backgroundTransform.anchoredPosition.x, (backgroundTransform.rect.width*0.9f)/2);
        personTransform.anchorMin = new Vector2(0.5f, 0.55f);
        personTransform.anchorMax = new Vector2(0.5f, 0.55f);
        personTransform.sizeDelta = new Vector2(backgroundTransform.rect.width*0.9f, backgroundTransform.rect.width*0.9f);
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
