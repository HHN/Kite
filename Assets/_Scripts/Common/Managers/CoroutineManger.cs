using UnityEngine;

namespace Assets._Scripts.Common.Managers
{
    public class CoroutineManger : MonoBehaviour
    {
        private static CoroutineManger _instance;

        private Coroutine _speakingCoroutine;

        public static CoroutineManger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<CoroutineManger>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("CoroutineManger");
                        _instance = obj.AddComponent<CoroutineManger>();
                        DontDestroyOnLoad(obj);
                    }
                }

                return _instance;
            }
        }

        public void SetSpeakingCoroutine(Coroutine coroutine)
        {
            _speakingCoroutine = coroutine;
        }

        public Coroutine GetSpeakingCoroutine()
        {
            return _speakingCoroutine;
        }

        public void StopSpeakingCoroutine()
        {
            StopCoroutine(_speakingCoroutine);
        }
    }
}