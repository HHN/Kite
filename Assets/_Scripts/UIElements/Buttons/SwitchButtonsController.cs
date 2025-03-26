using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI_Elements.Buttons
{
    public class SwitchButtonsController : MonoBehaviour
    {
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI indexInfo;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Image image;
        [SerializeField] private long index;

        private void Start()
        {
            leftButton.onClick.AddListener(OnLeftButton);
            rightButton.onClick.AddListener(OnRightButton);
            SetIndex(0);
        }

        private void Select(long index)
        {
            if (index < 0 || index >= sprites.Length)
            {
                return;
            }

            image.sprite = sprites[index];
            this.index = index;

            if (index < 9)
            {
                this.indexInfo.text = "0" + (index + 1) + " / " + (sprites.Length);
            }
            else
            {
                this.indexInfo.text = (index + 1) + " / " + (sprites.Length);
            }
        }

        private void OnLeftButton()
        {
            if (index > 0)
            {
                Select(index - 1);
            }
            else
            {
                Select(sprites.Length - 1);
            }
        }

        private void OnRightButton()
        {
            if (index < (sprites.Length - 1))
            {
                Select(index + 1);
            }
            else
            {
                Select(0);
            }
        }

        public long GetIndex()
        {
            return index;
        }

        private void SetIndex(long index)
        {
            Select(index);
        }
    }
}