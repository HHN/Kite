using UnityEngine;

namespace _00_Kite2.Common.Novel.Character.CharacterController
{
    public class IntroNovelImageController : NovelImageController
    {
        [SerializeField] private GameObject characterPrefab;
    
        public override void SetCharacter()
        {
            CharacterController = characterPrefab.GetComponent<CharacterController>();
        }

        public override bool HandleTouchEvent(float x, float y, AudioSource audioSource)
        {
            // Check if animations are allowed to proceed, return false if disabled
            if (AnimationFlagSingleton.Instance().GetFlag() == false)
            {
                return false;
            }
            return false;
        }
    }
}