using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Common.UI.UI_Elements.Toggle
{
    public class CustomToggle : MonoBehaviour
    {
        [SerializeField] private Button toggleButton;
        [SerializeField] private GameObject visualIndicator;
        [SerializeField] private bool isClicked;

        // Start is called before the first frame update
        private void Start()
        {
            isClicked = false;
            visualIndicator.SetActive(false);
        }

        public void OnButtonPressed()
        {
            isClicked = !isClicked;
            visualIndicator.SetActive(isClicked);
        }

        public bool IsClicked()
        {
            return isClicked;
        }
    }
}