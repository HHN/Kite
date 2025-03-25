using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Animator._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing.Regions._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Appearances._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Utilities;
using UnityEngine;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Appearances.Built_in
{
    [UnityEngine.Scripting.Preserve]
    [CreateAssetMenu(fileName = "Rotating Appearance", menuName = "Text Animator/_Animations/Appearances/Rotating")]
    [EffectInfo("rot", EffectCategory.Appearances)]
    [DefaultValue(nameof(baseDuration), .7f)]
    public sealed class RotatingAppearance : AppearanceScriptableBase
    {
        public float baseTargetAngle = 50;
        float targetAngle;
        
        public override void ResetContext(TAnimCore animator)
        {
            base.ResetContext(animator);
            targetAngle = baseTargetAngle;
        }

        public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
        {
            character.current.positions.RotateChar(
                Mathf.Lerp(
                    targetAngle,
                    0,
                    Tween.EaseInOut(character.passedTime / duration)
                )
            );
        }

        public override void SetModifier(ModifierInfo modifier)
        {
            switch (modifier.name)
            {
                case "a": targetAngle = baseTargetAngle * modifier.value; break;
                default: base.SetModifier(modifier); break;
            }
        }
    }

}