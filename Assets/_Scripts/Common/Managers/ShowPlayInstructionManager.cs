namespace Assets._Scripts.Common.Managers
{
    public class ShowPlayInstructionManager
    {
        private static ShowPlayInstructionManager _instance;

        private bool _showInstruction;
        private const string Key = "ShowPlayInstruction";

        private ShowPlayInstructionManager()
        {
            _showInstruction = LoadValue();
        }

        public static ShowPlayInstructionManager Instance()
        {
            if (_instance == null)
            {
                _instance = new ShowPlayInstructionManager();
            }

            return _instance;
        }

        public bool ShowInstruction()
        {
            return _showInstruction;
        }

        public void SetShowInstruction(bool value)
        {
            _showInstruction = value;
            Save();
        }

        private bool LoadValue()
        {
            if (PlayerDataManager.Instance().HasKey(Key))
            {
                string value = PlayerDataManager.Instance().GetPlayerData(Key);
                return bool.Parse(value);
            }
            else
            {
                return true;
            }
        }

        private void Save()
        {
            PlayerDataManager.Instance().SavePlayerData(Key, _showInstruction.ToString());
        }
    }
}