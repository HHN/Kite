using Assets._Scripts.Common.Managers;
using UnityEngine;

namespace Assets._Scripts.Common.UI_Elements.TextBoxes
{
    public class PlayOnAwake : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;

        void Start()
        {
            GlobalVolumeManager.Instance.PlaySound(audioClip);
        }
    }
}
