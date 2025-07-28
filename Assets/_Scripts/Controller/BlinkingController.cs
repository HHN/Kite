using System.Collections;
using UnityEngine;

namespace Assets._Scripts.Controller
{
    /// <summary>
    /// Controls a blinking animation behavior for a character or object using an Animator component.
    /// It triggers a "Blink" animation at random intervals and has a chance to trigger a double blink.
    /// </summary>
    [RequireComponent(typeof(Animator))] // Ensures that an Animator component is present on the GameObject.
    public class BlinkingController : MonoBehaviour
    {
        // Static variable to hold the hash for the "Blink" trigger in the Animator
        private static readonly int Blink = Animator.StringToHash("Blink");

        [SerializeField] private float minBlinkTime = 3f; // Minimum time between blinks
        [SerializeField] private float maxBlinkTime = 5f; // Maximum time between blinks
        
        private Animator _animator; // Reference to the Animator component

        /// <summary>
        /// Called when the script instance is being loaded.
        /// It initializes the Animator component and starts the blinking coroutine.
        /// </summary>
        private void Start()
        {
            // Get the Animator component from the GameObject
            _animator = GetComponent<Animator>();
            
            // Check if the Animator component exists
            if (_animator == null)
            {
                Debug.LogError("Animator not found on GameObject. Please assign an Animator component.");
                return;
            }

            // Start the blinking coroutine
            StartCoroutine(BlinkRoutine());
        }

        /// <summary>
        /// This coroutine continuously triggers the blink animation at random intervals.
        /// It waits for a random duration between <see cref="minBlinkTime"/> and <see cref="maxBlinkTime"/>
        /// before calling <see cref="TriggerBlink"/>.
        /// </summary>
        /// <returns>An IEnumerator to support coroutine execution.</returns>
        private IEnumerator BlinkRoutine()
        {
            while (true)
            {
                // Wait for a random time between minBlinkTime and maxBlinkTime
                float waitTime = Random.Range(minBlinkTime, maxBlinkTime);
                yield return new WaitForSeconds(waitTime);

                // Trigger a blink
                TriggerBlink();
            }
        }

        /// <summary>
        /// Triggers the primary blink animation.
        /// It also includes a 20% chance to trigger a second "double blink" shortly after the first.
        /// </summary>
        private void TriggerBlink()
        {
            // Determine if a double blink should occur (20% probability)
            bool doubleBlink = Random.Range(0, 1f) < 0.2f;

            // Set the Blink trigger in the Animator
            _animator.SetTrigger(Blink);

            // If double blink is true, initiate a short delay and blink again
            if (doubleBlink)
            {
                StartCoroutine(DoubleBlinkDelay());
            }
        }

        /// <summary>
        /// This coroutine handles the short delay between two blinks when a double blink occurs.
        /// After the delay, it triggers the blink animation again.
        /// </summary>
        /// <returns>An IEnumerator to support coroutine execution.</returns>
        private IEnumerator DoubleBlinkDelay()
        {
            // Wait for a short duration (e.g., 0.2 seconds)
            yield return new WaitForSeconds(0.2f);

            // Trigger the Blink animation again
            _animator.SetTrigger(Blink);
        }
    }
}
