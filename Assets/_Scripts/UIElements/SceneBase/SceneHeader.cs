using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using Assets._Scripts.UIElements.Messages;
using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.SceneBase
{
    public class SceneHeader : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button legalInformationButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private GameObject warningMessageBox;
        [SerializeField] private LeaveNovelAndGoBackMessageBox warningMessageBoxObject;
        [SerializeField] private Canvas canvas;

        private PlayNovelSceneController _playNovelSceneController; // Reference to the PlayNovelSceneController to manage novel actions

        private void Start()
        {
            GameObject persistentController = GameObject.Find("PlayNovelSceneController");
            if (persistentController != null)
            {
                _playNovelSceneController = persistentController.GetComponent<PlayNovelSceneController>();
            }
            
            backButton.gameObject.SetActive(true);

            if (GameManager.Instance.IsIntroNovelLoadedFromMainMenu)
            {
                backButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);
                backButton.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                backButton.interactable = false;

                GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            }
            else
            {
                backButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                backButton.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1); 
                backButton.interactable = true;
            }

            backButton.onClick.AddListener(OnBackButton);
            legalInformationButton.onClick.AddListener(OnLegalInformationButton);
            settingsButton.onClick.AddListener(OnSettingsButton);
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
                        bool isAdditiveSubScene = active.name != "PlayNovelScene" && active.isLoaded;
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

                        SceneLoader.LoadFoundersBubbleScene(); 
                        return;
                    }
                    if (lastScene == "PlayNovelScene")
                    {
                        Debug.Log($"lastScene == PlayNovelScene: {_playNovelSceneController != null}");
                        if (_playNovelSceneController)
                        {
                            _playNovelSceneController.IsPaused = false;
                            _playNovelSceneController.Continue();
                        }
                        
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

            HandleMessageBox(sceneName);
        }

        private void HandleMessageBox(string sceneName)
        {
            if (!warningMessageBox) return;
            
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

        private void OnLegalInformationButton()
        {
            if (_playNovelSceneController != null)
            {
                _playNovelSceneController.IsPaused = true;
            }
            
            SceneLoader.LoadLegalInformationScene();
        }

        private void OnSettingsButton()
        {
            if (_playNovelSceneController != null)
            {
                _playNovelSceneController.IsPaused = true;
            }
            
            SceneLoader.LoadSettingsScene();
        }
    }
}