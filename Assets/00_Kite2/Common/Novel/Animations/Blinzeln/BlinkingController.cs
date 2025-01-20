using System.Collections;
using UnityEngine;

namespace _00_Kite2.Common.Novel.Animations.Blinzeln
{
    public class BlinkingController : MonoBehaviour
    {
        // Static variable to hold the hash for the "Blink" trigger in the Animator
        private static readonly int Blink = Animator.StringToHash("Blink");

        [SerializeField] private float minBlinkTime = 3f; // Minimum time between blinks
        [SerializeField] private float maxBlinkTime = 5f; // Maximum time between blinks
        
        private Animator _animator; // Reference to the Animator component

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

        // Coroutine that controls the timing of blinks
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

        // Triggers the blink animation and optionally a double blink
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

        // Handles the short delay between double blinks
        private IEnumerator DoubleBlinkDelay()
        {
            // Wait for a short duration (e.g., 0.2 seconds)
            yield return new WaitForSeconds(0.2f);

            // Trigger the Blink animation again
            _animator.SetTrigger(Blink);
        }
    }
}
