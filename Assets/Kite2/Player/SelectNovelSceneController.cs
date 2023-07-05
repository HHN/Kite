using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectNovelSceneController : SceneController
{
    public GameObject visualNovelRepresentationPrefab;
    public Sprite[] smalNovelSprites;
    public Sprite[] bigNovelSprites;
    public GameObject allNovelsView;
    public GameObject detailsView;
    public bool isDisplayingDetails = false;

    // 400 * 400 sprites for representation next to each other.
    public Sprite FindSmalSpriteById(long id)
    {
        if (smalNovelSprites.Length <= id)
        {
            return null;
        }
        return smalNovelSprites[id];
    }

    // 800 * 800 sprites for displaying it on big view.
    public Sprite FindBigSpriteById(long id)
    {
        if (bigNovelSprites.Length <= id)
        {
            return null;
        }
        return bigNovelSprites[id];
    }

    public List<VisualNovel> GetKiteNovels()
    {
        return new List<VisualNovel>
        {
            new VisualNovel {headline = "Bankgespräch", image = 0, description = "Du hast eine Einladung zu einem Bankgespräch erhalten, um mehr Informationen über einen Kredit zu erhalten und diesen darauf zu beantragen."},
            new VisualNovel {headline = "Telefonat mit den Eltern", image = 1, description = "Du beschließt, deine Eltern anzurufen, und ihnen von deinem Gründungsvorhaben zu berichten."},
            new VisualNovel {headline = "Pressegespräch", image = 2, description = "Du befindest dich auf einer Veranstaltung, bei der Jungunternehmer*innen ihre Geschäftsidee vor einem Publikum präsentieren können, um Rückmeldung zu der Idee zu erhalten und zu networken. Nachdem du  deine Geschäftsidee vor dem Publikum gepitcht hast, stellst du dich an einen Tisch mit anderen Gästen, um mit ihnen zu reden."},
            new VisualNovel {headline = "Telefonat mit dem Notar", image = 3, description = "Du hast ein Telefonat mit einem*einer Notar*in, um einen Termin für deine Gründung auszumachen."},
            new VisualNovel {headline = "Anmietung eines Büros", image = 4, description = "Du hast heute einen Termin für die Besichtigung von Büroräumen."},
            new VisualNovel {headline = "Erstgespräch Förderantrag", image = 5, description = "Du wurdest zu einem Termin beim Arbeitsamt eingeladen, wo du dich mit einem Berater über deine Geschäftsidee unterhalten kannst und hoffentlich Informationen zu passenden Förderungen erhalten wirst."},
            new VisualNovel {headline = "Gründerzuschuss", image = 6, description = "Du bist heute bei deinem örtlichen Arbeitsamt, um einen Gründerzuschuss zu beantragen."},
            new VisualNovel {headline = "Gespräch mit Bekannten", image = 7, description = "Du triffst dich mit einem*einer Bekannten, den*die du seit ein paar Jahren nicht mehr gesehen hast, in einem kleinen Café. Er*Sie hat dir soeben erzählt, wo er*sie zurzeit beruflich steht. Nun bist du an der Reihe."}
        };
    }

    public List<VisualNovel> GetUserNovels()
    {
        return new List<VisualNovel>
        {
            new VisualNovel {headline = "Gespräch mit Bekannten", image = 7, description = "Du triffst dich mit einem*einer Bekannten, den*die du seit ein paar Jahren nicht mehr gesehen hast, in einem kleinen Café. Er*Sie hat dir soeben erzählt, wo er*sie zurzeit beruflich steht. Nun bist du an der Reihe."},
            new VisualNovel {headline = "Gründerzuschuss", image = 6, description = "Du bist heute bei deinem örtlichen Arbeitsamt, um einen Gründerzuschuss zu beantragen."},
            new VisualNovel {headline = "Erstgespräch Förderantrag", image = 5, description = "Du wurdest zu einem Termin beim Arbeitsamt eingeladen, wo du dich mit einem Berater über deine Geschäftsidee unterhalten kannst und hoffentlich Informationen zu passenden Förderungen erhalten wirst."},
            new VisualNovel {headline = "Anmietung eines Büros", image = 4, description = "Du hast heute einen Termin für die Besichtigung von Büroräumen."},
            new VisualNovel {headline = "Telefonat mit dem Notar", image = 3, description = "Du hast ein Telefonat mit einem*einer Notar*in, um einen Termin für deine Gründung auszumachen."},
            new VisualNovel {headline = "Pressegespräch", image = 2, description = "Du befindest dich auf einer Veranstaltung, bei der Jungunternehmer*innen ihre Geschäftsidee vor einem Publikum präsentieren können, um Rückmeldung zu der Idee zu erhalten und zu networken. Nachdem du  deine Geschäftsidee vor dem Publikum gepitcht hast, stellst du dich an einen Tisch mit anderen Gästen, um mit ihnen zu reden."},
            new VisualNovel {headline = "Telefonat mit den Eltern", image = 1, description = "Du beschließt, deine Eltern anzurufen, und ihnen von deinem Gründungsvorhaben zu berichten."},
            new VisualNovel {headline = "Bankgespräch", image = 0, description = "Du hast eine Einladung zu einem Bankgespräch erhalten, um mehr Informationen über einen Kredit zu erhalten und diesen darauf zu beantragen."}
        };
    }

    public List<VisualNovel> FindNovelById(long id)
    {
        throw new System.NotImplementedException();
    }

    public void ShowDetailsViewWithNovel(VisualNovel novel)
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
