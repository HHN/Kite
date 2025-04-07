using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Managers;
using Assets._Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UndoChoice
{
    public class UndoChoiceMessageBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageBoxBody;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button confirmButton;
        [SerializeField] private GameObject background;
        [SerializeField] private GameObject backgroundLeave;
        [SerializeField] private GameObject textStay;
        [SerializeField] private GameObject person;

        private Color _novelColor;
        private PlayNovelSceneController _playNovelSceneController;

        private void Start()
        {
            _playNovelSceneController = FindAnyObjectByType<PlayNovelSceneController>();

            cancelButton.onClick.AddListener(OnCancelButton);
            confirmButton.onClick.AddListener(OnConfirmButton);

            InitUI();
            FontSizeManager.Instance().UpdateAllTextComponents();
        }

        private void InitUI()
        {
            // Retrieve the color once and assign it to all necessary elements
            _novelColor = NovelColorManager.Instance().GetColor();

            ApplyColorToUI(background, _novelColor);
            ApplyColorToUI(backgroundLeave, _novelColor);
            textStay.GetComponent<TextMeshProUGUI>().color = _novelColor;
        }

        // Method for applying color to reduce redundancy
        private static void ApplyColorToUI(GameObject uiElement, Color color)
        {
            if (uiElement != null && uiElement.TryGetComponent(out Image image))
            {
                image.color = color;
            }
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        private void OnCancelButton()
        {
            this.CloseMessageBox();
        }

        private void OnConfirmButton()
        {
            CloseMessageBox();
            _playNovelSceneController.RestoreChoice();
        }

        public void CloseMessageBox()
        {
            gameObject.SetActive(false); // Only deactivate instead of destroying
        }
    }
}