using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UserFeedback
{
    public class NovelReviewGuiElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headButtonText;
        [SerializeField] private TextMeshProUGUI nameOfNovel;
        [SerializeField] private TextMeshProUGUI reviewText;
        [SerializeField] private Button star01;
        [SerializeField] private Button star02;
        [SerializeField] private Button star03;
        [SerializeField] private Button star04;
        [SerializeField] private Button star05;
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
                    star01.image.sprite = starFullSprite;
                    star02.image.sprite = starEmptySprite;
                    star03.image.sprite = starEmptySprite;
                    star04.image.sprite = starEmptySprite;
                    star05.image.sprite = starEmptySprite;
                    break;
                }
                case (2):
                {
                    star01.image.sprite = starFullSprite;
                    star02.image.sprite = starFullSprite;
                    star03.image.sprite = starEmptySprite;
                    star04.image.sprite = starEmptySprite;
                    star05.image.sprite = starEmptySprite;
                    break;
                }
                case (3):
                {
                    star01.image.sprite = starFullSprite;
                    star02.image.sprite = starFullSprite;
                    star03.image.sprite = starFullSprite;
                    star04.image.sprite = starEmptySprite;
                    star05.image.sprite = starEmptySprite;
                    break;
                }
                case (4):
                {
                    star01.image.sprite = starFullSprite;
                    star02.image.sprite = starFullSprite;
                    star03.image.sprite = starFullSprite;
                    star04.image.sprite = starFullSprite;
                    star05.image.sprite = starEmptySprite;
                    break;
                }
                case (5):
                {
                    star01.image.sprite = starFullSprite;
                    star02.image.sprite = starFullSprite;
                    star03.image.sprite = starFullSprite;
                    star04.image.sprite = starFullSprite;
                    star05.image.sprite = starFullSprite;
                    break;
                }
                default:
                {
                    star01.image.sprite = starFullSprite;
                    star02.image.sprite = starEmptySprite;
                    star03.image.sprite = starEmptySprite;
                    star04.image.sprite = starEmptySprite;
                    star05.image.sprite = starEmptySprite;
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
}