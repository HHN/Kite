using UnityEngine;

public class PlayOnAwake : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    void Start()
    {
        GlobalVolumeManager.Instance.PlaySound(audioClip);
    }
}
