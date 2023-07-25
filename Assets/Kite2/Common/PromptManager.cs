using System.Text;

public class PromptManager
{
    private static PromptManager instance;
    public StringBuilder prompt;

    private PromptManager() { }

    public static PromptManager Instance()
    {
        if (instance == null)
        {
            instance = new PromptManager();
        }
        return instance;
    }

    public string GetPrompt()
    {
        if (prompt == null)
        {
            return "";
        }
        return prompt.ToString();
    }

    public void InitializePrompt()
    {
        prompt = new StringBuilder();

        prompt.Append("Du bist eine Fachperson auf dem Gebiet der Diskriminierung von Gründerinnen. ");
        prompt.Append(
            "Ich schildere dir jetzt ein Szenario und du bewertest das " +
            "Szenario dahingehend, ob dort Diskriminierung stattgefunden " +
            "hat. Gib der Unternehmerin Feedback über ihr verhalten. " +
            "Hier kommt das Szenario: ");
    }

    public void AddLineToPrompt(string line)
    {
        if (prompt == null)
        {
            prompt = new StringBuilder();
        }
        prompt.Append(line + " ");
    }
}
