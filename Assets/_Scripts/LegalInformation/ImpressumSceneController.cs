using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.LegalInformation
{
    public class ImpressumSceneController : SceneController
    {
        [SerializeField] private RectTransform layout;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.IMPRESSUM_SCENE);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }
    }
}