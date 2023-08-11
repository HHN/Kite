using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectNovelSceneController : NovelProvider
{
    public GameObject visualNovelRepresentationPrefab;
    public Sprite[] smalNovelSprites;
    public Sprite[] bigNovelSprites;
    public GameObject allNovelsView;
    public GameObject detailsView;
    public bool isDisplayingDetails = false;

    // 400 * 400 sprites for representation next to each other.
    public override Sprite FindSmalSpriteById(long id)
    {
        if (smalNovelSprites.Length <= id)
        {
            return null;
        }
        return smalNovelSprites[id];
    }

    // 800 * 800 sprites for displaying it on big view.
    public override Sprite FindBigSpriteById(long id)
    {
        if (bigNovelSprites.Length <= id)
        {
            return null;
        }
        return bigNovelSprites[id];
    }

    public override List<VisualNovel> GetKiteNovels()
    {
        return new List<VisualNovel>
        {
            new BankTalkNovel(),
            new CallWithParentsNovel(),
            new PressTalkNovel(),
            new CallWithNotaryNovel(),
            new BankAccountOpeningNovel(),
            new RentingAnOfficeNovel(),
            new InitialInterviewForGrantApplicationNovel(),
            new StartUpGrantNovel(),
            new ConversationWithAcquaintancesNovel(),
            new BankAppointmentNovel(),
            new FeeNegotiationNovel()
        };
    }

    public override List<VisualNovel> GetUserNovels()
    {
        return new List<VisualNovel>();
    }

    public override List<VisualNovel> GetAccountNovels()
    {
        return new List<VisualNovel>();
    }

    public List<VisualNovel> FindNovelById(long id)
    {
        throw new System.NotImplementedException();
    }

    public override void ShowDetailsViewWithNovel(VisualNovel novel)
    {
        isDisplayingDetails = true;
        DetailsView view = detailsView.GetComponent<DetailsView>();
        view.novelToDisplay = novel;
        view.Initialize();
        allNovelsView.SetActive(false);
        detailsView.SetActive(true);
    }

    public void ShowAllNovelsView()
    {
        isDisplayingDetails = false;
        detailsView.SetActive(false);
        allNovelsView.SetActive(true);
    }
}
