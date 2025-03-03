using System.Collections.Generic;
using Assets._Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets._Scripts.SceneControllers
{
    public class KnowledgeSceneController : MonoBehaviour
    {
        [SerializeField] private Button barrierenButton;
        [SerializeField] private Button erwartungenNormenButton;
        [SerializeField] private Button wahrnehmungFuehrungsrollenButton;
        [SerializeField] private Button barrierenHindernisseButton;

        [SerializeField] private GameObject biasGroups;
        [SerializeField] private GameObject biasInformation;

        [SerializeField] private GameObject barrierenInfoGroup;
        [SerializeField] private GameObject erwartungenNormenInfoGroup;
        [SerializeField] private GameObject wahrnehmungFuehrungsrollenInfoGroup;
        [SerializeField] private GameObject barrierenHindernisseInfoGroup;
        
        [SerializeField] private Button barrierenInfoButton;
        [SerializeField] private Button erwartungenNormenInfoButton;
        [SerializeField] private Button wahrnehmungFuehrungsrollenInfoButton;
        [SerializeField] private Button barrierenHindernisseInfoButton;
        
        private Dictionary<Button, System.Action> _buttonActions;

        public void Start()
        {
            InitializeButtonActions();
            AddButtonListeners();
        }

        private void InitializeButtonActions()
        {
            _buttonActions = new Dictionary<Button, System.Action>
            {
                { barrierenButton, OnBarrierenButton },
                { erwartungenNormenButton, OnErwartungenNormen },
                { wahrnehmungFuehrungsrollenButton, OnWahrnehmungFuehrungsrollenButton },
                { barrierenHindernisseButton, OnBarrierenHindernisseButton },
                { barrierenInfoButton, OnBarrierenInfoButton },
                { erwartungenNormenInfoButton, OnErwartungenNormenInfoButton },
                { wahrnehmungFuehrungsrollenInfoButton, OnWahrnehmungFuehrungsrollenInfoButton },
                { barrierenHindernisseInfoButton, OnBarrierenHindernisseInfoButton }
            };
        }

        private void AddButtonListeners()
        {
            foreach (var buttonAction in _buttonActions)
            {
                buttonAction.Key.onClick.AddListener(() => buttonAction.Value.Invoke());
            }
        }

        private void OnBarrierenButton()
        {
            biasGroups.SetActive(false);
            biasInformation.SetActive(true);
            
            barrierenInfoGroup.SetActive(true);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(false);
        }

        private void OnErwartungenNormen()
        {
            biasGroups.SetActive(false);
            biasInformation.SetActive(true);
            
            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(true);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(false);
        }

        private void OnWahrnehmungFuehrungsrollenButton()
        {
            biasGroups.SetActive(false);
            biasInformation.SetActive(true);
            
            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(true);
            barrierenHindernisseInfoGroup.SetActive(false);
        }

        private void OnBarrierenHindernisseButton()
        {
            biasGroups.SetActive(false);
            biasInformation.SetActive(true);
            
            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(true);
        }

        private void OnBarrierenInfoButton()
        {
            biasGroups.SetActive(true);
            biasInformation.SetActive(false);
            
            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(false);
        }

        private void OnErwartungenNormenInfoButton()
        {
            biasGroups.SetActive(true);
            biasInformation.SetActive(false);
            
            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(false);
        }

        private void OnWahrnehmungFuehrungsrollenInfoButton()
        {
            biasGroups.SetActive(true);
            biasInformation.SetActive(false);
            
            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(false);
        }

        private void OnBarrierenHindernisseInfoButton()
        {
            biasGroups.SetActive(true);
            biasInformation.SetActive(false);
            
            barrierenInfoGroup.SetActive(false);
            erwartungenNormenInfoGroup.SetActive(false);
            wahrnehmungFuehrungsrollenInfoGroup.SetActive(false);
            barrierenHindernisseInfoGroup.SetActive(false);
        }
    }
}
