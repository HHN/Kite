using System.Collections.Generic;
using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Server_Communication;
using _00_Kite2.Server_Communication.Server_Calls;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _00_Kite2.UserFeedback
{
    public class AddObserverSceneController : SceneController
    {
        [SerializeField] private RectTransform layout;
        [SerializeField] private Button confirmButton;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private GameObject addObserverServerCallPrefab;
        [SerializeField] private GameObject getObserverServerCallPrefab;
        [SerializeField] private GameObject observerRepresentationPrefab;
        [SerializeField] private GameObject oberserverContainer;

        private void Start()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            BackStackManager.Instance().Push(SceneNames.ADD_OBSERVER_SCENE);
            confirmButton.onClick.AddListener(OnConfirmButton);
            InitializeObserverList();
        }

        public void InitializeObserverList()
        {
            GetReviewObserversServerCall call = Instantiate(getObserverServerCallPrefab)
                .GetComponent<GetReviewObserversServerCall>();
            call.sceneController = this;
            call.OnSuccessHandler = new GetObserversServerCallSuccessHandler(this);
            call.SendRequest();
        }

        private void OnConfirmButton()
        {
            if (string.IsNullOrEmpty(inputField.text.Trim()))
            {
                this.DisplayInfoMessage("Bitte Email-Adresse angeben!");
                return;
            }

            AddReviewObserverServerCall call = Instantiate(addObserverServerCallPrefab)
                .GetComponent<AddReviewObserverServerCall>();
            call.sceneController = this;
            call.OnSuccessHandler = new AddObserverServerCallSuccessHandler(this);
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
                    Instantiate(observerRepresentationPrefab, oberserverContainer.transform)
                        .GetComponent<TextMeshProUGUI>();
                observerGuiElement.text = "Keine Beobachter gefunden!";
                return;
            }

            foreach (ReviewObserver observer in observers)
            {
                TextMeshProUGUI observerGuiElement =
                    Instantiate(observerRepresentationPrefab, oberserverContainer.transform)
                        .GetComponent<TextMeshProUGUI>();
                observerGuiElement.text = observer.GetEmail();
            }
        }
    }

    public class AddObserverServerCallSuccessHandler : IOnSuccessHandler
    {
        private readonly AddObserverSceneController _addObserverSceneController;

        public AddObserverServerCallSuccessHandler(AddObserverSceneController addObserverSceneController)
        {
            this._addObserverSceneController = addObserverSceneController;
        }

        public void OnSuccess(Response response)
        {
            _addObserverSceneController.InitializeObserverList();
            _addObserverSceneController.DisplayInfoMessage("Email-Adresse erfolgreich hinzugefügt!");
        }
    }

    public class GetObserversServerCallSuccessHandler : IOnSuccessHandler
    {
        private readonly AddObserverSceneController _addObserverSceneController;

        public GetObserversServerCallSuccessHandler(AddObserverSceneController addObserverSceneController)
        {
            this._addObserverSceneController = addObserverSceneController;
        }

        public void OnSuccess(Response response)
        {
            _addObserverSceneController.InitializeObservers(response.GetReviewObserver());
        }
    }
}