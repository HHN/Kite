using System;
using System.Threading.Tasks;
using StandAloneKITE2;

public static class Program
{
    // Change HERE: which “program” should run?
    //   App.Twee      -> starts TweePathCalculator
    //   App.Audit     -> starts FeedbackPathAudit
    //   App.Coverage  -> starts DialogListCoverageAudit
    private const App Active = App.Coverage;

    public static async Task Main(string[] args)
    {
        switch (Active)
        {
            case App.Twee:
                Console.WriteLine("[Bootstrap] Starte TweePathCalculator …");
                await TweePathCalculator.RunAsync();
                break;

            case App.Audit:
                Console.WriteLine("[Bootstrap] Starte FeedbackPathAudit …");
                FeedbackPathAudit.FeedbackPathAudit.Run();
                break;

            case App.Coverage:
                Console.WriteLine("[Bootstrap] Starte DialogListCoverageAudit …");
                DialogListCoverageAudit.Run();
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private enum App { Twee, Audit, Coverage }
}
