using UnityEngine;

namespace Assets._Scripts.UI_Elements.Buttons
{
    public class RadioButtonHandler : MonoBehaviour
    {
        public UnityEngine.UI.Toggle kiteNovelsToggle;
        public UnityEngine.UI.Toggle userNovelsToggle;
        public UnityEngine.UI.Toggle accountNovelsToggle;
        public UnityEngine.UI.Toggle favorites;
        private int _indexToActivateOnStart;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            ActivateIndex(_indexToActivateOnStart);
        }

        private bool IsKiteNovelsOn()
        {
            return kiteNovelsToggle.isOn;
        }

        private bool IsUserNovelsOn()
        {
            return userNovelsToggle.isOn;
        }

        private bool IsAccountNovelsOn()
        {
            return accountNovelsToggle.isOn;
        }

        private bool IsFavoritesOn()
        {
            return favorites.isOn;
        }

        public int GetIndex()
        {
            if (IsKiteNovelsOn())
            {
                return 0;
            }
            else if (IsUserNovelsOn())
            {
                return 1;
            }
            else if (IsAccountNovelsOn())
            {
                return 2;
            }
            else if (IsFavoritesOn())
            {
                return 3;
            }

            return 0;
        }

        public void SetIndex(int index)
        {
            this._indexToActivateOnStart = index;
        }

        private void ActivateIndex(int index)
        {
            switch (index)
            {
                case 0:
                {
                    kiteNovelsToggle.isOn = true;
                    return;
                }
                case 1:
                {
                    userNovelsToggle.isOn = true;
                    return;
                }
                case 2:
                {
                    accountNovelsToggle.isOn = true;
                    return;
                }
                case 3:
                {
                    favorites.isOn = true;
                    return;
                }
                default:
                {
                    kiteNovelsToggle.isOn = true;
                    return;
                }
            }
        }
    }
}