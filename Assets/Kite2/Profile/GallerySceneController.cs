using System.Collections.Generic;
using UnityEngine;

public class GallerySceneController : NovelProvider
{
    public override Sprite FindBigSpriteById(long id)
    {
        return null;
    }

    public override Sprite FindSmalSpriteById(long id)
    {
        return null;
    }

    public override List<VisualNovel> GetAccountNovels()
    {
        return new List<VisualNovel>();
    }

    public override List<VisualNovel> GetKiteNovels()
    {
        return new List<VisualNovel>();
    }

    public override List<VisualNovel> GetUserNovels()
    {
        return new List<VisualNovel>();
    }

    public override void ShowDetailsViewWithNovel(VisualNovel novel)
    {
    }
}
