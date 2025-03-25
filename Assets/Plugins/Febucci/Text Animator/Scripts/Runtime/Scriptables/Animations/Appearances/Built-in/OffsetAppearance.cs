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
    [CreateAssetMenu(fileName = "Offset Appearance", menuName = "Text Animator/_Animations/Appearances/Offset")]
    [EffectInfo("offset", EffectCategory.Appearances)]
    public sealed class OffsetAppearance : AppearanceScriptableBase
    {
        public float baseAmount = 10;
        float amount;
        public Vector2 baseDirection = Vector2.one;

        public override void ResetContext(TAnimCore animator)
        {
            base.ResetContext(animator);
            amount = baseAmount;
        }

        public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
        {
            character.current.positions.MoveChar(baseDirection * amount * character.uniformIntensity * Tween.EaseIn(1 - character.passedTime / duration));
        }

        public override void SetModifier(ModifierInfo modifier)
        {
            switch (modifier.name)
            {
                case "a": amount = baseAmount * modifier.value; break;
                default: base.SetModifier(modifier); break;
            }
        }
    }

}