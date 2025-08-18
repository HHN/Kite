using System;
using System.Threading.Tasks;

public static class Program
{
    // HIER umstellen: welches „Programm“ soll laufen?
    //   App.Twee  -> startet TweePathCalculator
    //   App.Audit -> startet FeedbackPathAudit
    private const App Active = App.Audit;

    public static async Task Main(string[] args)
    {
        switch (Active)
        {
            case App.Twee:
                Console.WriteLine("[Bootstrap] Starte TweePathCalculator …");
                await TweePathCalculator.RunAsync();   // <— siehe Anpassung unten
                break;

            case App.Audit:
                Console.WriteLine("[Bootstrap] Starte FeedbackPathAudit …");
                FeedbackPathAudit.FeedbackPathAudit.Run();
                break;


            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private enum App { Twee, Audit }
}