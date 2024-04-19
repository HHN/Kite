using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NovelReviewGuiElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headButtonText;
    [SerializeField] private TextMeshProUGUI nameOfNovel;
    [SerializeField] private TextMeshProUGUI reviewText;
    [SerializeField] private Button star_01;
    [SerializeField] private Button star_02;
    [SerializeField] private Button star_03;
    [SerializeField] private Button star_04;
    [SerializeField] private Button star_05;
    [SerializeField] private Sprite starFullSprite;
    [SerializeField] private Sprite starEmptySprite;
    [SerializeField] private NovelReview novelReview;

    public void InitializeReview(NovelReview novelReview)
    {
        this.novelReview = novelReview;
        headButtonText.text = "Bewertung (ID:" + novelReview.GetId() + ")";
        nameOfNovel.text = "Bewertete Novel: " + novelReview.GetNovelName();
        reviewText.text = novelReview.GetReviewText();

        switch (novelReview.GetRating())
        {
            case (1):
                {
                    star_01.image.sprite = starFullSprite;
                    star_02.image.sprite = starEmptySprite;
                    star_03.image.sprite = starEmptySprite;
                    star_04.image.sprite = starEmptySprite;
                    star_05.image.sprite = starEmptySprite;
                    break;
                }
            case (2):
                {
                    star_01.image.sprite = starFullSprite;
                    star_02.image.sprite = starFullSprite;
                    star_03.image.sprite = starEmptySprite;
                    star_04.image.sprite = starEmptySprite;
                    star_05.image.sprite = starEmptySprite;
                    break;
                }
            case (3):
                {
                    star_01.image.sprite = starFullSprite;
                    star_02.image.sprite = starFullSprite;
                    star_03.image.sprite = starFullSprite;
                    star_04.image.sprite = starEmptySprite;
                    star_05.image.sprite = starEmptySprite;
                    break;
                }
            case (4):
                {
                    star_01.image.sprite = starFullSprite;
                    star_02.image.sprite = starFullSprite;
                    star_03.image.sprite = starFullSprite;
                    star_04.image.sprite = starFullSprite;
                    star_05.image.sprite = starEmptySprite;
                    break;
                }
            case (5):
                {
                    star_01.image.sprite = starFullSprite;
                    star_02.image.sprite = starFullSprite;
                    star_03.image.sprite = starFullSprite;
                    star_04.image.sprite = starFullSprite;
                    star_05.image.sprite = starFullSprite;
                    break;
                }
            default:
                {
                    star_01.image.sprite = starFullSprite;
                    star_02.image.sprite = starEmptySprite;
                    star_03.image.sprite = starEmptySprite;
                    star_04.image.sprite = starEmptySprite;
                    star_05.image.sprite = starEmptySprite;
                    break;
                }
        }
    }

    public string GetNovelName()
    {
        string novelName = novelReview?.GetNovelName();

        if (novelName == null)
        {
            return "";
        }
        return novelName;
    }
}
