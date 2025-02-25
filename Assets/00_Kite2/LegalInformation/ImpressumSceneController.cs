using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace _00_Kite2.NewPages
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