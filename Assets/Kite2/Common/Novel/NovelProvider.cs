using System.Collections.Generic;
using UnityEngine;

public abstract class NovelProvider : SceneController
{
    public abstract List<VisualNovel> GetKiteNovels();

    public abstract List<VisualNovel> GetUserNovels();

    public abstract List<VisualNovel> GetAccountNovels();

    public abstract Sprite FindSmalSpriteById(long id);

    public abstract Sprite FindBigSpriteById(long id);

    public abstract void ShowDetailsViewWithNovel(VisualNovel novel);
}
