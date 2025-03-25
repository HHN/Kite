using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing._Core;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core
{
    /// <summary>
    /// Contains animations that will be recognized and used by Text Animator
    /// </summary>
    [System.Serializable]
    [UnityEngine.CreateAssetMenu(fileName = "_Animations Database", menuName = "Text Animator/_Animations/Create _Animations Database", order = 100)]
    public class AnimationsDatabase : Database<AnimationScriptableBase>
    {
        
    }
}