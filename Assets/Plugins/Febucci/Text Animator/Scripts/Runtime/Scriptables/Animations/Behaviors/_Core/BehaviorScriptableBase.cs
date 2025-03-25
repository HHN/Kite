using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Animator._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Behaviors._Core
{
    /// <summary>
    /// Base class for animating letters in Text Animator
    /// </summary>
    public abstract class BehaviorScriptableBase : AnimationScriptableBase
    {
        public override float GetMaxDuration() => -1; //infinite
        public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator) => true;
    }

}