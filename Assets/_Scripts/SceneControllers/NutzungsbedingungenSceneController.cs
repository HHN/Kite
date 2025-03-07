using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.SceneControllers
{
    public class NutzungsbedingungenSceneController : SceneController
    {
        [SerializeField] private RectTransform layout;
        [SerializeField] private RectTransform layout02;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.NutzungsbedingungenScene);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout02);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }
    }
}