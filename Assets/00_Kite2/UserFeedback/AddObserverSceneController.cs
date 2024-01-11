using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddObserverSceneController : SceneController, OnSuccessHandler
{
    [SerializeField] private RectTransform layout;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject addObserverServerCallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        BackStackManager.Instance().Push(SceneNames.ADD_OBSERVER_SCENE);
        confirmButton.onClick.AddListener(delegate { OnConfirmButton(); });
    }

    public void OnConfirmButton()
    {
        if (string.IsNullOrEmpty(inputField.text.Trim()))
        {
            this.DisplayInfoMessage("Bitte Email-Adresse angeben!");
            return;
        }
        AddReviewObserverServerCall call = Instantiate(addObserverServerCallPrefab).GetComponent<AddReviewObserverServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
        call.email = inputField.text.Trim();
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);
        inputField.text = "";
    }

    public void OnSuccess(Response response)
    {
        this.DisplayInfoMessage("Email-Adresse erfolgreich hinzugefügt!");
    }
}
