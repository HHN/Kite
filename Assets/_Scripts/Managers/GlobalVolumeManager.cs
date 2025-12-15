using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// Manages global audio settings and playback in the application.
    /// </summary>
    public class GlobalVolumeManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private static GlobalVolumeManager _instance;

        /// <summary>
        /// Provides a singleton instance of the GlobalVolumeManager.
        /// Ensures that there is only one instance of this manager throughout the application
        /// and allows access to manage global audio settings and playback.
        /// </summary>
        /// <remarks>
        /// If no existing instance is present in the scene, a new instance is created and persisted across scene loads.
        /// This property is thread-safe and lazily initialized.
        /// </remarks>
        public static GlobalVolumeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GlobalVolumeManager>();

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

        /// <summary>
        /// Ensures that a single instance of the GlobalVolumeManager exists in the scene.
        /// Initializes the global AudioSource if it is the first instance and prevents the destruction of this GameObject across scenes.
        /// If an instance already exists, destroys the duplicate instance.
        /// </summary>
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                InitGlobalAudioSource();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Sets the global volume for the application's audio.
        /// Adjusts the volume of the associated AudioSource, ensuring the value is clamped within a range of 0 to 1.
        /// </summary>
        /// <param name="volume">The desired global volume level, where 0 represents mute and 1 represents the maximum volume.</param>
        public void SetGlobalVolume(float volume)
        {
            if (audioSource == null)
            {
                LogManager.Error("AudioSource ist null.");
                return;
            }

            audioSource.volume = Mathf.Clamp(volume, 0f, 1f);
        }


        /// <summary>
        /// Retrieves the current global volume level of the application's audio.
        /// </summary>
        /// <returns>The current volume level as a float, where 0 represents mute and 1 represents the maximum volume.</returns>
        private float GetGlobalVolume()
        {
            return audioSource.volume;
        }

        /// <summary>
        /// Initializes the global AudioSource component for the GlobalVolumeManager.
        /// Ensures an AudioSource exists on the GameObject, creates one if absent, and sets the initial global volume level
        /// based on a previously saved value from PlayerPrefs.
        /// </summary>
        private void InitGlobalAudioSource()
        {
            if (audioSource == null)
            {
                LogManager.Error("AudioSource ist null. Initialisiere die AudioSource.");
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            float savedSoundEffectsVolume = PlayerPrefs.GetFloat("SavedSoundeffectsVolume", 100);
            SetGlobalVolume(savedSoundEffectsVolume / 100);
        }


        /// <summary>
        /// Plays the specified audio clip through the managed AudioSource.
        /// Optionally sets whether the audio should loop during playback. The playback volume is controlled by the global volume setting.
        /// </summary>
        /// <param name="clip">The audio clip to be played.</param>
        /// <param name="loop">Optional parameter to specify whether the audio should loop. Defaults to false.</param>
        public void PlaySound(AudioClip clip, bool loop = false)
        {
            if (audioSource == null)
            {
                LogManager.Error("AudioSource ist null.");
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

        /// <summary>
        /// Stops the playback of any sound currently being played by the associated AudioSource.
        /// Ensures that if no audio is playing, the method exits without performing any action.
        /// </summary>
        public void StopSound()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}