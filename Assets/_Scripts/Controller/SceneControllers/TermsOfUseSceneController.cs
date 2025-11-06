using System.IO;
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Manages the behavior and functionality specific to the "Terms of Use" scene in the application.
    /// Inherits common scene management functionality from the SceneController class.
    /// </summary>
    public class TermsOfUseSceneController : SceneController
    {
        [SerializeField] private RectTransform layout;
        [SerializeField] private RectTransform layout02;

        /// <summary>
        /// Initializes the "Terms of Use" scene by performing setup tasks, such as managing the back stack,
        /// rebuilding the layout for specified components, and updating text elements to ensure proper customization.
        /// </summary>
        private void Start()
        {
            BackStackManager.Instance.Push(SceneNames.TermsOfUseScene);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout02);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }
    }
}