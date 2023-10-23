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
            "Er hat unangemessene Fragen zu ihrem Familienstand gestellt, was irrelevant f�r den Kreditantrag ist. " +
            "Es ist nicht seine Aufgabe, die Familienplanung der Unternehmerin zu beurteilen oder zu ber�cksichtigen. " +
            "Es ist auch nicht angebracht, nach einem Ehepartner oder einem gr��eren Startkapital zu fragen, " +
            "da dies nicht relevant f�r den Kreditantrag ist." + " \n\n" + "Die Unternehmerin hat sich jedoch gut verhalten, " +
            "indem sie ruhig und sachlich geblieben ist und die Fragen des Bankmitarbeiters beantwortet hat. " +
            "Sie hat auch den Ablauf des Gespr�chs und ihre Erwartungen klar ausgedr�ckt und um konkrete Fragen gebeten. \n\n" + 
            "Die Unternehmerin sollte das Verhalten des Bankmitarbeiters ansprechen und gegebenenfalls eine " +
            "Beschwerde einreichen. Es ist wichtig, dass der Bankmitarbeiter sein Verhalten �berdenkt und sich " +
            "in Zukunft professioneller verh�lt, um Diskriminierung zu vermeiden.";
       
        novelToPlay.feedback = feedback;
        */

        return novelToPlay;
    }
}