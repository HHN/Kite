using UnityEngine.UI;
using TMPro;

public class GenerateNovelSceneController : SceneController
{
    public Button generateNovelButton;
    public Button skipGenerationButton;

    public TMP_InputField selfDescriptionField;
    public TMP_InputField partnerDescriptionField;
    public TMP_InputField locationDescriptionField;
    public TMP_InputField dialogDescriptionField;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.GENERATE_NOVEL_SCENE);

        generateNovelButton.onClick.AddListener(delegate { OnGenerateNovelButton(); });
        skipGenerationButton.onClick.AddListener(delegate { OnSkipGenerationButton(); });
    }

    public void OnGenerateNovelButton()
    {
        if (string.IsNullOrEmpty(selfDescriptionField.text) ||
            string.IsNullOrEmpty(partnerDescriptionField.text) ||
            string.IsNullOrEmpty(locationDescriptionField.text) ||
            string.IsNullOrEmpty(dialogDescriptionField.text))
        {
            this.DisplayErrorMessage(ErrorMessages.NOT_EVERYTHING_ENTERED);
        }
        else
        {

        }
    }

    public void OnSkipGenerationButton()
    {
        SceneLoader.LoadCharacterMakerScene();
    }
}
