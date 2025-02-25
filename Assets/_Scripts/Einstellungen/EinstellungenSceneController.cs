using Assets._Scripts.Common;
using Assets._Scripts.Common.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Einstellungen
{
    public class EinstellungenSceneController : SceneController
    {
        [SerializeField] private Button barrierefreiheitButton;
        [SerializeField] private Button impressumButton;
        [SerializeField] private Button nutzungsbedingungenButton;
        [SerializeField] private Button datenschutzButton; 
        [SerializeField] private Button soundeinstellungButton;
        [SerializeField] private TMP_Text versionInfo;

        public void Start()
        {
            barrierefreiheitButton.onClick.AddListener(OnBarrierefreiheitButton);
            impressumButton.onClick.AddListener(OnImpressumButton);
            nutzungsbedingungenButton.onClick.AddListener(OnNutzungsbedingungenButton);
            datenschutzButton.onClick.AddListener(OnDatenschutzButton);
            soundeinstellungButton.onClick.AddListener(OnSoundeinstellungButton);
            versionInfo.text = "Version: " + Application.version;
        }

        private void OnBarrierefreiheitButton()
        {
            SceneLoader.LoadBarrierefreiheitScene();
        }

        private void OnImpressumButton()
        {
            SceneLoader.LoadImpressumScene();
        }

        private void OnNutzungsbedingungenButton()
        {
            SceneLoader.LoadNutzungsbedingungenScene();
        }

        private void OnDatenschutzButton()
        {
            SceneLoader.LoadDatenschutzScene();
        }

        private void OnSoundeinstellungButton()
        {
            SceneLoader.SoundeinstellungScene();
        }
    }
}