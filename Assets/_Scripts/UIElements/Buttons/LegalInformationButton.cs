using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.Buttons
{
    public class LegalInformationButton : MonoBehaviour
    {
        [SerializeField] private Button legalInformationButton;

        private void Start()
        {
            legalInformationButton.onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            Debug.Log("Fuck");
        }
    }
}
