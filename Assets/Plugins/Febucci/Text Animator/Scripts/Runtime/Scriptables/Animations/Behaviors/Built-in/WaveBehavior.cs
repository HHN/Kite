using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Animator._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Behaviors._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Utilities;
using UnityEngine;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Behaviors.Built_in
{
    [UnityEngine.Scripting.Preserve]
    [CreateAssetMenu(menuName = "Text Animator/_Animations/Behaviors/Wave", fileName = "Wave Behavior")]
    [EffectInfo("wave", EffectCategory.Behaviors)]
    [DefaultValue(nameof(baseAmplitude), 7.27f)]
    [DefaultValue(nameof(baseFrequency), 4f)]
    [DefaultValue(nameof(baseWaveSize), .4f)]
    public sealed class WaveBehavior : BehaviorScriptableSine
    {
        public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
        {
            character.current.positions.MoveChar(
                Vector3.up * Mathf.Sin(animator.time.timeSinceStart * frequency + character.index * waveSize) 
                * amplitude * character.uniformIntensity);
        }
    }

}