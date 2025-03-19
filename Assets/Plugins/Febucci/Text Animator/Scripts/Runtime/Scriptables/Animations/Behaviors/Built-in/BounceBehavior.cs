using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Animator._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Behaviors._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Utilities;
using UnityEngine;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Behaviors.Built_in
{
    [UnityEngine.Scripting.Preserve]
    [CreateAssetMenu(fileName = "Bounce", menuName = "Text Animator/_Animations/Behaviors/Bounce")]
    [EffectInfo("bounce", EffectCategory.Behaviors)]
    [DefaultValue(nameof(baseAmplitude), 13.19f)]
    [DefaultValue(nameof(baseFrequency), 1f)]
    [DefaultValue(nameof(baseWaveSize), .2f)]
    public sealed class BounceBehavior : BehaviorScriptableSine
    {
        //Calculates the tween percentage
        float BounceTween(float t)
        {
            const float stillTime = .2f;
            const float easeIn = .2f;
            const float bounce = 1 - stillTime - easeIn;

            if (t <= easeIn)
                return Tween.EaseInOut(t / easeIn);
            t -= easeIn;

            if (t <= bounce)
                return 1 - Tween.BounceOut(t / bounce);

            return 0;
        }

        public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
        {
            character.current.positions.MoveChar(
                Vector3.up * character.uniformIntensity *
                BounceTween((Mathf.Repeat(animator.time.timeSinceStart * frequency - waveSize * character.index, 1))) * amplitude
                );
        }
    }

}