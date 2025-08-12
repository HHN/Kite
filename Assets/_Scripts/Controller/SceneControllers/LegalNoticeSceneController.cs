using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    /// <summary>
    /// Handles the logic and flow for the legal notice scene in the application.
    /// Inherits from the base <c>SceneController</c> class, allowing it to manage UI elements
    /// such as message boxes for information and error messages within the context of the legal notice scene.
    /// </summary>
    public class LegalNoticeSceneController : SceneController
    {
        [SerializeField] private RectTransform layout;

        /// <summary>
        /// Initializes the legal notice scene by managing its stack in the navigation flow,
        /// updating the layout components, and ensuring font sizes for all text elements
        /// are properly adjusted.
        /// </summary>
        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.LegalNoticeScene);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }
    }
}