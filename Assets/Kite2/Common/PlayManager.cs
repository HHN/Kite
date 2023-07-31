using System.Collections.Generic;

public class PlayManager
{
    private static PlayManager instance;
    private VisualNovel novelToPlay;

    private PlayManager() { }

    public static PlayManager Instance()
    {
        if (instance == null)
        {
            instance = new PlayManager();
        }
        return instance;
    }

    public void SetVisualNovelToPlay(VisualNovel novelToPlay)
    {
        this.novelToPlay = novelToPlay;
    }

    public VisualNovel GetVisualNovelToPlay()
    {
        if (novelToPlay == null)
        {
            return null;
        }
        

        /**
        string feedback = "In diesem Szenario hat der Bankmitarbeiter die Unternehmerin diskriminiert. " +
            "Er hat unangemessene Fragen zu ihrem Familienstand gestellt, was irrelevant für den Kreditantrag ist. " +
            "Es ist nicht seine Aufgabe, die Familienplanung der Unternehmerin zu beurteilen oder zu berücksichtigen. " +
            "Es ist auch nicht angebracht, nach einem Ehepartner oder einem größeren Startkapital zu fragen, " +
            "da dies nicht relevant für den Kreditantrag ist." + " \n\n" + "Die Unternehmerin hat sich jedoch gut verhalten, " +
            "indem sie ruhig und sachlich geblieben ist und die Fragen des Bankmitarbeiters beantwortet hat. " +
            "Sie hat auch den Ablauf des Gesprächs und ihre Erwartungen klar ausgedrückt und um konkrete Fragen gebeten. \n\n" + 
            "Die Unternehmerin sollte das Verhalten des Bankmitarbeiters ansprechen und gegebenenfalls eine " +
            "Beschwerde einreichen. Es ist wichtig, dass der Bankmitarbeiter sein Verhalten überdenkt und sich " +
            "in Zukunft professioneller verhält, um Diskriminierung zu vermeiden.";
       
        novelToPlay.feedback = feedback;
        */

        return novelToPlay;
    }
}