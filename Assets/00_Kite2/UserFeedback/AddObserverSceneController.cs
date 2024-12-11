using System;
using System.Collections.Generic;
using _00_Kite2.Common.Managers;
using _00_Kite2.Server_Communication;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddObserverSceneController : SceneController
{
    [SerializeField] private RectTransform layout;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject addObserverServerCallPrefab;
    [SerializeField] private GameObject getObserverServerCallPrefab;
    [SerializeField] private GameObject observerRepresntationPrefab;
    [SerializeField] private GameObject oberserverContainer;

    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        BackStackManager.Instance().Push(SceneNames.ADD_OBSERVER_SCENE);
        confirmButton.onClick.AddListener(delegate { OnConfirmButton(); });
        InitializeObserverList();
    }

    public void InitializeObserverList()
    {
        GetReviewObserversServerCall call = Instantiate(getObserverServerCallPrefab).GetComponent<GetReviewObserversServerCall>();
        call.sceneController = this;
        call.OnSuccessHandler = new GetObserversServerCallSuccessHandler(this);
        call.SendRequest();
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
        call.OnSuccessHandler = new AddObserverServalCallSuccessHandler(this);
        call.email = inputField.text.Trim();
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);
        inputField.text = "";
    }

    public void InitializeObservers(List<ReviewObserver> observers)
    {
        foreach (Transform child in oberserverContainer.transform)
        {
            Destroy(child.gameObject);
        }
        if (observers == null || observers.Count == 0)
        {
            TextMeshProUGUI observerGuiElement =
            Instantiate(observerRepresntationPrefab, oberserverContainer.transform)
            .GetComponent<TextMeshProUGUI>();
            observerGuiElement.text = "Keine Beobachter gefunden!";
            return;
        }
        foreach (ReviewObserver observer in observers)
        {
            TextMeshProUGUI observerGuiElement = 
                Instantiate(observerRepresntationPrefab, oberserverContainer.transform)
                .GetComponent<TextMeshProUGUI>();
            observerGuiElement.text = observer.GetEmail();
        }
    }
}
public class AddObserverServalCallSuccessHandler : OnSuccessHandler 
{
    private AddObserverSceneController addObserverSceneController;

    public AddObserverServalCallSuccessHandler(AddObserverSceneController addObserverSceneController)
    {
        this.addObserverSceneController = addObserverSceneController;
    }

    public void OnSuccess(Response response)
    {
        addObserverSceneController.InitializeObserverList();
        addObserverSceneController.DisplayInfoMessage("Email-Adresse erfolgreich hinzugef�gt!");
    }
}

public class GetObserversServerCallSuccessHandler : OnSuccessHandler
{
    private AddObserverSceneController addObserverSceneController;

    public GetObserversServerCallSuccessHandler(AddObserverSceneController addObserverSceneController)
    {
        this.addObserverSceneController = addObserverSceneController;
    }

    public void OnSuccess(Response response)
    {
        addObserverSceneController.InitializeObservers(response.GetReviewObserver());
    }
}