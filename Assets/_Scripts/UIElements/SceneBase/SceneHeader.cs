using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UIElements.Messages;
using Assets._Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.SceneBase
{
    public class SceneHeader : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button legalInformationButton;
        [SerializeField] private GameObject warningMessageBox;
        [SerializeField] private GameObject warningMessageBoxIntro;
        [SerializeField] private GameObject warningMessageBoxClose;
        [SerializeField] private LeaveNovelAndGoBackMessageBox warningMessageBoxObject;
        [SerializeField] private LeaveNovelAndGoBackMessageBox warningMessageBoxObjectIntro;
        [SerializeField] private LeaveNovelAndGoBackToMainMenuMessageBox warningMessageBoxObjectClose;
        [SerializeField] private Canvas canvas;

        private PlayNovelSceneController _playNovelSceneController; // Reference to the PlayNovelSceneController to manage novel actions

        private void Start()
        {
            GameObject controllerObject = GameObject.Find("Controller");
            if (controllerObject != null)
            {
                _playNovelSceneController = controllerObject.GetComponent<PlayNovelSceneController>();
            }
            
            backButton.gameObject.SetActive(true);

            if (GameManager.Instance.IsIntroNovelLoadedFromMainMenu)
            {
                backButton.gameObject.SetActive(false);
                GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            }
            else
            {
                backButton.gameObject.SetActive(true);
            }

            backButton.onClick.AddListener(OnBackButton);
            legalInformationButton.onClick.AddListener(OnLegalInformationButton);
        }

        private void OnBackButton()
        {
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName.Contains("PlayNovelScene") && _playNovelSceneController != null)
            {
                _playNovelSceneController.IsPaused = true; // Pause the novel progression
            }

            if (!sceneName.Contains("PlayNovelScene"))
            {
                if (sceneName.Contains("KnowledgeScene"))
                {
                    KnowledgeSceneController knowledgeSceneController = FindAnyObjectByType<KnowledgeSceneController>();
                    knowledgeSceneController.NavigateScene();
                }
                else
                {
                    // string lastScene = SceneRouter.GetTargetSceneForBackButton();
                    string lastScene = BackStackManager.Instance().Pop();
                    
                    // As long as the loaded scene is the active scene, load the next scene
                    while (!string.IsNullOrEmpty(lastScene) && lastScene == SceneManager.GetActiveScene().name)
                    {
                        lastScene = BackStackManager.Instance().Pop();
                    }

                    if (string.IsNullOrEmpty(lastScene))
                    {
                        Scene active = SceneManager.GetActiveScene();
                        bool isAdditiveSubScene = active.name != "PlayNovelScene" 
                                                  && active.isLoaded;
                        if (isAdditiveSubScene)
                        {
                            // A) Sub-Szene entladen
                            SceneManager.UnloadSceneAsync(active);

                            // B) PlayNovelScene wieder aktivieren
                            Scene baseScene = SceneManager.GetSceneByName("PlayNovelScene");
                            if (baseScene.IsValid() && baseScene.isLoaded)
                            {
                                SceneManager.SetActiveScene(baseScene);
                            }
                            return;
                        }
                        else
                        {
                            SceneLoader.LoadFoundersBubbleScene(); 
                            return;
                        }
                    }
                    if (lastScene == "PlayNovelScene")
                    {
                        // Wenn der Backstack gerade genau "PlayNovelScene" geliefert hat,
                        // soll die Basis-Szene nur wieder aktiv gesetzt werden –
                        // auf keinen Fall neu geladen.
                        // Falls aktuell noch eine Additive-Sub-Szene offen ist, entlade sie…
                        Scene active = SceneManager.GetActiveScene();
                        if (active.name != "PlayNovelScene")
                        {
                            SceneManager.UnloadSceneAsync(active);
                        }

                        // Nun PlayNovelScene zum Active machen
                        var playNovel = SceneManager.GetSceneByName("PlayNovelScene");
                        if (playNovel.IsValid() && playNovel.isLoaded)
                        {
                            SceneManager.SetActiveScene(playNovel);
                        }
                        return;
                    }

#if UNITY_IOS
                    TextToSpeechManager.Instance.CancelSpeak();
#endif

                    SceneLoader.LoadScene(lastScene);
                }
            }

            if (warningMessageBox != null)
            {
                if (!warningMessageBoxObject.IsNullOrDestroyed())
                {
                    warningMessageBoxObject.CloseMessageBox();
                }

                if (canvas.IsNullOrDestroyed())
                {
                    return;
                }

                warningMessageBoxObject = null;
                warningMessageBoxObject = Instantiate(warningMessageBox, canvas.transform).GetComponent<LeaveNovelAndGoBackMessageBox>();

                if (sceneName.Contains("PlayNovelScene") && _playNovelSceneController.NovelToPlay.id == 13)
                {
                    warningMessageBoxObject.HandleButtons();
                }

                warningMessageBoxObject.Activate();
            }
        }
        
        public void OnLegalInformationButton()
        {
            Debug.Log($"OnLegalInformationButton");
            SceneLoader.LoadLegalInformationScene();
        }

        public void HandleButtons(bool isIntroScene)
        {
            Debug.Log("HandleButtons");
            backButton.gameObject.SetActive(!isIntroScene);
            legalInformationButton.gameObject.SetActive(isIntroScene);
        }
    }
}