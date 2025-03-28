using System.Collections;
using Assets._Scripts.Managers;
using Assets._Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.Founders_Bubble
{
    public class AlreadyPlayedUpdater : MonoBehaviour
    {
        [SerializeField] private VisualNovelNames visualNovel;
        [SerializeField] private TextMeshProUGUI number;
        [SerializeField] private Animator animator;
        private bool _startedAnimation;

        private void Update()
        {
            int numberOfPlays = PlayRecordManager.Instance().GetNumberOfPlaysForNovel(visualNovel);
            bool value = (PlayRecordManager.Instance().GetNumberOfPlaysForNovel(visualNovel) > 0);
            this.gameObject.GetComponent<Image>().enabled = value;
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

        private IEnumerator SetValueIn90Frames(int numberOfPlays)
        {
            Debug.Log(numberOfPlays);
            if (_startedAnimation)
            {
                Debug.Log("break");
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