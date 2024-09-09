using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlreadyPlayedUpdater : MonoBehaviour
{
    [SerializeField] private VisualNovelNames visualNovel;
    [SerializeField] private TextMeshProUGUI number;
    [SerializeField] private Animator animator;
    private bool startedAnimation = false;

    void Update()
    {
        int numberOfPlays = PlayRecordManager.Instance().GetNumberOfPlaysForNovel(visualNovel);
        bool value = (PlayRecordManager.Instance().GetNumberOfPlaysForNovel(visualNovel) > 0);
        this.gameObject.GetComponent<Image>().enabled = value;
        number.gameObject.SetActive(value);

        if (PlaythrouCounterAnimationManager.Instance().IsAnimationTrue(visualNovel))
        {
            Debug.Log("Played Animation");
            StartCoroutine(SetValueIn90Frames(numberOfPlays));
        } 
        else
        {
            animator.enabled = false;
            number.text = numberOfPlays.ToString();
        }
    }

    public IEnumerator SetValueIn90Frames(int numberOfPlays)
    {
        if (startedAnimation)
        {
            yield break;
        }
        startedAnimation = true;
        animator.enabled = true;
        number.text = (numberOfPlays - 1).ToString();
        animator.Play("increase");
        yield return new WaitForSeconds(1.5f);
        number.text = numberOfPlays.ToString();
        PlaythrouCounterAnimationManager.Instance().SetAnimation(false, visualNovel);
        animator.enabled = false;
        startedAnimation = false;
    }
}
