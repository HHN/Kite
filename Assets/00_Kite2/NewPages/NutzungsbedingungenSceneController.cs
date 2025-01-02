using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.NewPages
{
    public class NutzungsbedingungenSceneController : SceneController
    {
        [SerializeField] private RectTransform layout;
        [SerializeField] private RectTransform layout02;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.NUTZUNGSBEDINGUNGEN_SCENE);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout02);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }
    }
}