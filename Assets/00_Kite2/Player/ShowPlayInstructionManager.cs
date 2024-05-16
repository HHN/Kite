using UnityEngine;

public class ShowPlayInstructionManager
{
    private static ShowPlayInstructionManager instance;

    private bool showInstruction;
    private const string KEY = "ShowPlayInstruction";

    private ShowPlayInstructionManager()
    {
        showInstruction = LoadValue();
    }

    public static ShowPlayInstructionManager Instance()
    {
        if (instance == null)
        {
            instance = new ShowPlayInstructionManager();
        }
        return instance;
    }

    public bool ShowInstruction()
    {
        return showInstruction;
    }

    public void SetShowInstruction(bool value)
    {
        showInstruction = value;
        Save();
    }

    public bool LoadValue()
    {
        if (PlayerPrefs.HasKey(KEY))
        {
            string value = PlayerPrefs.GetString(KEY);
            return bool.Parse(value);
        }
        else
        {
            return true;
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString(KEY, showInstruction.ToString());
        PlayerPrefs.Save();
    }
}
