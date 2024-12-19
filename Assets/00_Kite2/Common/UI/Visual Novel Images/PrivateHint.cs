using _00_Kite2.Common.Managers;
using _00_Kite2.Common.Novel;
using UnityEngine;

namespace _00_Kite2.Common.UI.Visual_Novel_Images
{
    public class PrivateHint : MonoBehaviour
    {
        public GameObject privateHint;

        private void Start()
        {
            VisualNovel visualNovel = PlayManager.Instance().GetVisualNovelToPlay();

            if (visualNovel == null)
            {
                return;
            }

            privateHint.SetActive(visualNovel.id == 0);
        }
    }
}