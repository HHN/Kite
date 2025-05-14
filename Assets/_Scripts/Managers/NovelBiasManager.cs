using System.Collections.Generic;

namespace Assets._Scripts.Managers
{
    public class NovelBiasManager
    {
        private static NovelBiasManager _instance;
        private readonly HashSet<string> _relevantBiases = new HashSet<string>();
        

        private NovelBiasManager() { }

        public static NovelBiasManager Instance()
        {
            return _instance ??= new NovelBiasManager();
        }

        public void MarkBiasAsRelevant(string biasName)
        {
            _relevantBiases.Add(biasName);
        }
        
        public bool IsBiasRelevant(string biasName)
        {
            return _relevantBiases.Contains(biasName);
        }

        public void Clear()
        {
            if (_instance == null)
            {
                _instance = new NovelBiasManager();
            }
            else
            {
                _instance._relevantBiases.Clear();
            }
        }
        
        public IEnumerable<string> GetAllRelevantBiases()
        {
            return _relevantBiases;
        }
    }
}