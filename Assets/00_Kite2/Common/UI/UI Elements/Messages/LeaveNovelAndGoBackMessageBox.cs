using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaveNovelAndGoBackMessageBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageBoxHeadline;
    [SerializeField] private TextMeshProUGUI messageBoxBody;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject backgroundLeave;
    [SerializeField] private GameObject textStay;
    [SerializeField] private GameObject person;

    void Start()
    {
        cancelButton.onClick.AddListener(delegate { OnCancleButton(); });
        confirmButton.onClick.AddListener(delegate { OnConfirmButton(); });
        InitUI();
    }

    private void InitUI()
    {
        Debug.Log(NovelColorManager.Instance().GetColor());
        background.GetComponent<Image>().color = NovelColorManager.Instance().GetColor();
        backgroundLeave.GetComponent<Image>().color = NovelColorManager.Instance().GetColor();
        textStay.GetComponent<TextMeshProUGUI>().color = NovelColorManager.Instance().GetColor();
        RectTransform backgroundTransform = background.GetComponent<RectTransform>();
        /*backgroundTransform.anchoredPosition = new Vector2(backgroundTransform.anchoredPosition.x, (NovelColorManager.Instance().GetCanvasHeight()/2)-(backgroundTransform.rect.height/2));
        RectTransform personTransform = person.GetComponent<RectTransform>();
        personTransform.anchoredPosition = new Vector2(backgroundTransform.anchoredPosition.x, (backgroundTransform.rect.width*0.9f)/2);
        personTransform.anchorMin = new Vector2(0.5f, 0.55f);
        personTransform.anchorMax = new Vector2(0.5f, 0.55f);
        personTransform.sizeDelta = new Vector2(backgroundTransform.rect.width*0.9f, backgroundTransform.rect.width*0.9f);*/
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

    public void OnCancleButton()
    {
        this.CloseMessageBox();
    }

    public void OnConfirmButton()
    {
        // Disable animations after confirmation
        AnimationFlagSingleton.Instance().SetFlag(false);

        // Cancel any ongoing speech and audio from the Text-to-Speech service
        TextToSpeechService.Instance().CancelSpeechAndAudio();

        // Retrieve the last scene for the back button functionality
        string lastScene = SceneRouter.GetTargetSceneForBackButton();

        // Check if there is no last scene, and if so, load the main menu scene
        if (string.IsNullOrEmpty(lastScene))
        {
            SceneLoader.LoadMainMenuScene();
            return; // Exit the method after loading the main menu
        }

        // If the last scene is the PLAY_INSTRUCTION_SCENE, load the FOUNDERS_BUBBLE_SCENE instead
        if (lastScene == SceneNames.PLAY_INSTRUCTION_SCENE.ToString())
        {
            SceneLoader.LoadScene(SceneNames.FOUNDERS_BUBBLE_SCENE.ToString());
            BackStackManager.Instance().Pop(); // Remove the instruction scene from the back stack
            return; // Exit the method after loading the new scene
        }

        // Load the last scene retrieved from the back button functionality
        SceneLoader.LoadScene(lastScene);
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
