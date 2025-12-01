using System.Collections;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.FoundersBubble
{
    /// <summary>
    /// Updates the display for how many times a specific visual novel has been played.
    /// It controls the visibility of an image and a text counter, and can trigger
    /// an animation when the play count increases.
    /// </summary>
    public class AlreadyPlayedUpdater : MonoBehaviour
    {
        [SerializeField] private string visualNovel;
        [SerializeField] private TextMeshProUGUI number;
        [SerializeField] private Animator animator;
        private bool _startedAnimation;

        /// <summary>
        /// Gets or sets the <see cref="VisualNovel"/> associated with this updater.
        /// </summary>
        public string VisualNovel
        {
            get => visualNovel;
            set => visualNovel = value;
        }

        /// <summary>
        /// Called once per frame.
        /// Updates the visibility of the "already played" indicator and the play count text
        /// based on the novel's play history. It also triggers an animation if a new play is registered.
        /// </summary>
        private void Update()
        {
            int numberOfPlays = PlayRecordManager.Instance().GetNumberOfPlaysForNovel(visualNovel);
            bool value = PlayRecordManager.Instance().GetNumberOfPlaysForNovel(visualNovel) > 0;
            gameObject.GetComponent<Image>().enabled = value;
            number.gameObject.SetActive(value);

            if (PlayThroughCounterAnimationManager.Instance().IsAnimationTrue(visualNovel))
            {
                StartCoroutine(SetValueIn90Frames(numberOfPlays));
            }
            else
            {
                animator.enabled = false;
                number.text = numberOfPlays.ToString();
            }
        }

        /// <summary>
        /// A coroutine that animates the play count increase.
        /// It temporarily shows the previous count, plays an "increase" animation,
        /// then updates to the new count after a delay.
        /// </summary>
        /// <param name="numberOfPlays">The final number of plays to display after the animation.</param>
        /// <returns>An IEnumerator for coroutine execution.</returns>
        private IEnumerator SetValueIn90Frames(int numberOfPlays)
        {
            if (_startedAnimation)
            {
                yield break;
            }

            _startedAnimation = true;
            animator.enabled = true;
            number.text = (numberOfPlays - 1).ToString();
            animator.Play("increase");
            yield return new WaitForSeconds(1.5f);
            number.text = numberOfPlays.ToString();
            PlayThroughCounterAnimationManager.Instance().SetAnimation(false, visualNovel);
            animator.enabled = false;
            _startedAnimation = false;
        }
    }
}