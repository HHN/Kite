using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    public class LegalNoticeSceneController : SceneController
    {
        [SerializeField] private RectTransform layout;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.LegalNoticeScene);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }
    }
}