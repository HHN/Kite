using Assets._Scripts.Common.Managers;
using Assets._Scripts.Common.Novel;
using UnityEngine;

namespace Assets._Scripts.Common.UI_Elements
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