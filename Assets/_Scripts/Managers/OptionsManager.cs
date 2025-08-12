using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Controller.CharacterController;
using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Assets._Scripts.UIElements.TextBoxes;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// The OptionsManager class is responsible for managing user-selectable options
    /// in a visual novel scenario. This includes initializing options, handling user
    /// selections, and triggering necessary events or actions based on the chosen option.
    /// </summary>
    public class OptionsManager : MonoBehaviour
    {
        [SerializeField] private ChatMessageBox optionA;
        [SerializeField] private ChatMessageBox optionB;
        [SerializeField] private ChatMessageBox optionC;
        [SerializeField] private ChatMessageBox optionD;
        [SerializeField] private ChatMessageBox optionE;
        [SerializeField] private AudioClip decisionSound;

        private string _idA;
        private string _idB;
        private string _idC;
        private string _idD;
        private string _idE;

        private bool _displayAfterSelectionA;
        private bool _displayAfterSelectionB;
        private bool _displayAfterSelectionC;
        private bool _displayAfterSelectionD;
        private bool _displayAfterSelectionE;

        private string _stringA;
        private string _stringB;
        private string _stringC;
        private string _stringD;
        private string _stringE;

        private bool _selected;

        [SerializeField] private AudioClip selectedSound;

        private PlayNovelSceneController _sceneController;
        private ConversationContentGuiController _conversationContentGuiController;

        /// <summary>
        /// Initializes the options manager by setting up options and their associated data,
        /// linking them to the scene controller, and updating the GUI controller.
        /// </summary>
        /// <param name="sceneController">The scene controller managing the current play novel scene.</param>
        /// <param name="options">A list of visual novel events representing the available options.</param>
        public void Initialize(PlayNovelSceneController sceneController, List<VisualNovelEvent> options)
        {
            _sceneController = sceneController;
            _conversationContentGuiController = FindAnyObjectByType<ConversationContentGuiController>();

            GlobalVolumeManager.Instance.PlaySound(selectedSound);

            if (options.Count == 0)
            {
                gameObject.SetActive(false);
                return;
            }

            optionA.SetMessage(options[0].text);
            _idA = options[0].onChoice;
            _stringA = options[0].text;
            _displayAfterSelectionA = options[0].show;

            if (options.Count == 1)
            {
                optionB.gameObject.SetActive(false);
                optionC.gameObject.SetActive(false);
                optionD.gameObject.SetActive(false);
                optionE.gameObject.SetActive(false);
                return;
            }

            optionB.SetMessage(options[1].text);
            _idB = options[1].onChoice;
            _stringB = options[1].text;
            _displayAfterSelectionB = options[1].show;

            if (options.Count == 2)
            {
                optionC.gameObject.SetActive(false);
                optionD.gameObject.SetActive(false);
                optionE.gameObject.SetActive(false);
                return;
            }

            optionC.SetMessage(options[2].text);
            _idC = options[2].onChoice;
            _stringC = options[2].text;
            _displayAfterSelectionC = options[2].show;

            if (options.Count == 3)
            {
                optionD.gameObject.SetActive(false);
                optionE.gameObject.SetActive(false);
                return;
            }

            optionD.SetMessage(options[3].text);
            _idD = options[3].onChoice;
            _stringD = options[3].text;
            _displayAfterSelectionD = options[3].show;

            if (options.Count == 4)
            {
                optionE.gameObject.SetActive(false);
                return;
            }

            optionE.SetMessage(options[4].text);
            _idE = options[4].onChoice;
            _stringE = options[4].text;
            _displayAfterSelectionE = options[4].show;
        }

        /// <summary>
        /// Handles the selection of Option A in the options menu. This method triggers the required follow-up actions, such as invoking a coroutine
        /// to handle post-selection events.
        /// </summary>
        public void OnOptionA()
        {
            StartCoroutine(AfterSelection("Selected A", _stringA, _idA, _displayAfterSelectionA, 0));
        }

        /// <summary>
        /// Handles the selection of Option B in the visual novel scenario.
        /// This method initiates a post-selection process,
        /// and triggers corresponding actions or updates based on Option B being chosen.
        /// </summary>
        public void OnOptionB()
        {
            StartCoroutine(AfterSelection("Selected B", _stringB, _idB, _displayAfterSelectionB, 1));
        }

        /// <summary>
        /// Handles the selection of Option C in the visual novel scenario.
        /// Triggers post-selection actions such as initiating the next event or updating the UI.
        /// </summary>
        public void OnOptionC()
        {
            StartCoroutine(AfterSelection("Selected C", _stringC, _idC, _displayAfterSelectionC, 2));
        }

        /// <summary>
        /// Handles the selection of Option D by initiating the next steps in the visual novel sequence.
        /// </summary>
        public void OnOptionD()
        {
            StartCoroutine(AfterSelection("Selected D", _stringD, _idD, _displayAfterSelectionD, 3));
        }

        /// <summary>
        /// Handles the selection of option E in the visual novel by marking it as chosen and starting the post-selection process.
        /// </summary>
        public void OnOptionE()
        {
            StartCoroutine(AfterSelection("Selected E", _stringE, _idE, _displayAfterSelectionE, 4));
        }

        /// <summary>
        /// Plays the decision sound effect to provide audio feedback for user interaction.
        /// Uses the GlobalVolumeManager to handle playback of the associated audio clip.
        /// </summary>
        public void PlayDecisionSound()
        {
            GlobalVolumeManager.Instance.PlaySound(decisionSound);
        }

        /// <summary>
        /// Handles the post-selection logic after an option is chosen, including updating
        /// the game state, playing animations and audio, and managing scene transitions.
        /// </summary>
        /// <param name="parameterName">The name of the animation or effect associated with the selected option.</param>
        /// <param name="answer">The string response corresponding to the selected option.</param>
        /// <param name="nextEventID">The ID of the next event triggered by this selection.</param>
        /// <param name="displayAfterSelection">Indicates whether additional UI or content should be displayed after the selection.</param>
        /// <param name="index">The index of the selected option.</param>
        /// <returns>An IEnumerator used to manage the timing of events following the selection.</returns>
        private IEnumerator AfterSelection(string parameterName, string answer, string nextEventID,
            bool displayAfterSelection, int index)
        {
#if UNITY_ANDROID
        TextToSpeechManager.Instance.CancelSpeak();
#elif UNITY_IOS
        TextToSpeechManager.Instance.CancelSpeak();
#endif
            GameManager.Instance.calledFromReload = false;

            // Disable animations after the selection
            var animationFlagSingleton = AnimationFlagSingleton.Instance();
            if (animationFlagSingleton != null)
            {
                animationFlagSingleton.SetFlag(false);
            }
            else
            {
                Debug.LogWarning("AnimationFlagSingleton.Instance() returned null.");
            }

            // If already selected, exit the coroutine
            if (_selected)
            {
                yield break;
            }

            _selected = true; // Mark as selected to prevent repeated selections

            // Add the current path (selection) to the visual novel path history
            if (_sceneController != null)
            {
                foreach (var novelEvent in _sceneController.NovelToPlay.novelEvents.Where(novelEvent =>
                             novelEvent.text == answer))
                {
                    _conversationContentGuiController.VisualNovelEvents.Add(
                        novelEvent);
                }

                _sceneController.AddPathToNovel(index);
            }
            else
            {
                Debug.LogWarning("sceneController is null. Cannot add path to novel.");
            }

            // Trigger the animation associated with the parameter
            Animator animator = GetComponent<Animator>();
            if (animator != null && !string.IsNullOrEmpty(parameterName))
            {
                animator.SetBool(parameterName, true);
            }
            else
            {
                if (animator == null)
                {
                    Debug.LogWarning("Animator component not found on GameObject.");
                }

                if (string.IsNullOrEmpty(parameterName))
                {
                    Debug.LogWarning("parameterName is null or empty.");
                }
            }
            
            if (selectedSound != null)
            {
                GlobalVolumeManager.Instance.PlaySound(selectedSound);
            }
            else
            {
                Debug.LogWarning("selectedSound is null. Cannot play audio clip.");
            }

            // Wait for the audio and animation to complete
            yield return new WaitForSeconds(0.5f);

            // Hide the current object after the selection
            gameObject.SetActive(false);

            // If the scene controller is set, proceed with further actions
            if (_sceneController != null)
            {
                // Show confirmation areas
                if (_sceneController.confirmArea != null && _sceneController.confirmArea.gameObject != null)
                {
                    _sceneController.confirmArea.gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("sceneController.confirmArea or its GameObject is null.");
                }

                if (_sceneController.confirmArea2 != null && _sceneController.confirmArea2.gameObject != null)
                {
                    _sceneController.confirmArea2.gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("sceneController.confirmArea2 or its GameObject is null.");
                }

                // Display the selected answer and handle post-selection behavior
                _sceneController.ShowAnswer(answer, displayAfterSelection);
                _sceneController.SetWaitingForConfirmation(true); // Indicate that a confirmation is expected
                _sceneController.SetNextEvent(nextEventID); // Set the next event ID for the scene

                // Automatically confirm if post-selection display is not required
                if (!displayAfterSelection)
                {
                    _sceneController.OnConfirm(); // Trigger confirmation if no further display is needed
                }
            }
            else
            {
                Debug.LogWarning("sceneController is null.");
            }
        }
    }
}