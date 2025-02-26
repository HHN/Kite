using UnityEngine;

namespace Assets._Scripts.Common.Managers
{
    public class GlobalVolumeManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private static GlobalVolumeManager _instance;

        public static GlobalVolumeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Versuche, eine bestehende Instanz in der Szene zu finden
                    _instance = FindObjectOfType<GlobalVolumeManager>();

                    // Wenn keine Instanz gefunden wurde, erstelle eine neue
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("GlobalVolumeManager");
                        _instance = obj.AddComponent<GlobalVolumeManager>();
                        DontDestroyOnLoad(obj);
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            // Singleton-Setup
            if (_instance == null)
            {
                _instance = this; // Setze die Instanz direkt auf die private Variable
                InitGlobalAudiosource();
                DontDestroyOnLoad(gameObject); // Behalte dieses Objekt beim Szenenwechsel
            }
            else
            {
                Destroy(gameObject); // Verhindere doppelte Instanzen
            }
        }

        // Setzt die globale Lautst�rke (0 bis 100)
        public void SetGlobalVolume(float volume)
        {
            if (audioSource == null)
            {
                Debug.LogError("AudioSource ist null.");
                return;
            }

            audioSource.volume = Mathf.Clamp(volume, 0f, 1f); // Wert begrenzen
        }


        // Holt die aktuelle Lautst�rke (in Prozent, 0 bis 100)
        public float GetGlobalVolume()
        {
            return audioSource.volume;
        }

        // Weist alle AudioSources in der Szene der Audio Mixer Group zu
        public void InitGlobalAudiosource()
        {
            if (audioSource == null)
            {
                Debug.LogError("AudioSource ist null. Initialisiere die AudioSource.");
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            float savedSoundeffectsVolume = UnityEngine.PlayerPrefs.GetFloat("SavedSoundeffectsVolume", 100);
            SetGlobalVolume(savedSoundeffectsVolume / 100);
        }


        public void PlaySound(AudioClip clip, bool loop = false)
        {
            if (audioSource == null)
            {
                Debug.LogError("AudioSource ist null.");
                return;
            }

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.volume = GetGlobalVolume(); 
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        }


        public void StopSound()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}