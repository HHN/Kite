using Assets._Scripts.Managers;
using UnityEngine;

namespace Assets._Scripts.UIElements.TextBoxes
{
    public class PlayOnAwake : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;

        private void Start()
        {
            GlobalVolumeManager.Instance.PlaySound(audioClip);
        }
    }
}
