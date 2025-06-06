using System.IO;
using Assets._Scripts.Managers;
using Assets._Scripts.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    public class TermsOfUseSceneController : SceneController
    {
        [SerializeField] private RectTransform layout;
        [SerializeField] private RectTransform layout02;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.TermsOfUseScene);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout02);
            FontSizeManager.Instance().UpdateAllTextComponents();
        }
    }
}