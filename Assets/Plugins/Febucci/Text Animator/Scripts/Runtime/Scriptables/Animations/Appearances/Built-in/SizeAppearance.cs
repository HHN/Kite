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
    [CreateAssetMenu(fileName = "Size Appearance", menuName = "Text Animator/_Animations/Appearances/Size")]
    [EffectInfo("size", EffectCategory.Appearances)]
    public sealed class SizeAppearance : AppearanceScriptableBase
    {
        float amplitude;
        public float baseAmplitude = 2;

        public override void ResetContext(TAnimCore animator)
        {
            base.ResetContext(animator);
            amplitude = baseAmplitude * -1 + 1;
        }

        public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
        {
            character.current.positions.LerpUnclamped(
                character.current.positions.GetMiddlePos(),
                Tween.EaseIn(1 - (character.passedTime / duration)) * amplitude
                );
        }

        public override void SetModifier(ModifierInfo modifier)
        {
            switch (modifier.name)
            {
                case "a": amplitude = baseAmplitude * modifier.value; break;
                default: base.SetModifier(modifier); break;
            }
        }
    }
}