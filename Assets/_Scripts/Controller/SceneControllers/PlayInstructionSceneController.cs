using System;
using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Handles the behavior and logic specifically for the instructional scenes within the game.
    /// </summary>
    /// <remarks>
    /// This controller inherits from the <see cref="SceneController"/> class and is designed to manage
    /// user interactions, messages, and other functionality specific to instructional scenes.
    /// </remarks>
    public class PlayInstructionSceneController : SceneController
    {
        [SerializeField] private GameObject novel;
        [SerializeField] private Image textBoxImage;
        [SerializeField] private TextMeshProUGUI novelName;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Color backgroundColor;

        [SerializeField] private Button backToFoundersBubbleButton;
        [SerializeField] private Button playButton;
        [SerializeField] private Button knowledgeButton;

        [SerializeField] private Image backToFoundersBubbleButtonImage;
        [SerializeField] private Image playButtonImage;
        [SerializeField] private Image knowledgeButtonImage;
        [SerializeField] private Image headerImage;

        private bool _isSyncing;

        /// <summary>
        /// Called when the PlayInstructionScene is initialized.
        /// This method sets up various properties and behaviors specific to the instructional scene.
        /// </summary>
        /// <remarks>
        /// This includes updating UI elements, setting colors and states, and configuring button and toggle listeners.
        /// It integrates with the <see cref="BackStackManager"/> to track scene history
        /// and with <see cref="PlayManager"/> to retrieve relevant information about the visual novel.
        /// </remarks>
        private void Start()
        {
            BackStackManager.Instance.Push(SceneNames.PlayInstructionScene);
            
            backgroundColor = PlayManager.Instance().GetColorOfVisualNovelToPlay();
            
            VisualNovel visualNovel = PlayManager.Instance().GetVisualNovelToPlay();
            
            Image[] images = novel.GetComponentsInChildren<Image>(true);

            foreach (Image img in images)
            {
                if (img.gameObject.name == "ButtonFrame")
                {
                    img.color = visualNovel.novelFrameColor;
                }
                else if (img.gameObject.name == "NovelName")
                {
                    img.color = visualNovel.novelColor;
                }
            }

            backToFoundersBubbleButton.onClick.AddListener(OnBackToFoundersBubbleButton);
            playButton.onClick.AddListener(OnPlayButton);
            knowledgeButton.onClick.AddListener(OnKnowledgeButton);

            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        /// <summary>
        /// Handles the event when the "Back to Founders Bubble" button is clicked.
        /// </summary>
        /// <remarks>
        /// This method transitions the current scene to the "FoundersBubble" scene
        /// using the <see cref="SceneLoader.LoadFoundersBubbleScene"/> method.
        /// It allows users to navigate back to the FoundersBubble context from the instructional scene.
        /// </remarks>
        private void OnBackToFoundersBubbleButton()
        {
            SceneLoader.LoadFoundersBubbleScene();
        }

        /// <summary>
        /// This method manages toggling behaviors when the "never show again" toggle is set
        /// and transitions the current scene to the play novel scene using the <see cref="SceneLoader.LoadPlayNovelScene"/> method.
        /// </summary>
        private void OnPlayButton()
        {
            GameManager.Instance.IsIntroNovelLoadedFromMainMenu = false;
            SceneLoader.LoadPlayNovelScene();
        }

        /// <summary>
        /// Handles the action triggered when the Knowledge button is clicked.
        /// </summary>
        /// <remarks>
        /// This method loads the knowledge scene by invoking the <see cref="SceneLoader.LoadKnowledgeScene"/> method.
        /// It facilitates navigation between the instructional scene and the knowledge content of the application.
        /// </remarks>
        private void OnKnowledgeButton()
        {
            SceneLoader.LoadKnowledgeScene();
        }
    }
}