using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Animator._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Behaviors._Core;
using UnityEngine;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Behaviors.Built_in
{
    [UnityEngine.Scripting.Preserve]
    [CreateAssetMenu(fileName = "Slide Behavior", menuName = "Text Animator/_Animations/Behaviors/Slide")]
    [EffectInfo("slide", EffectCategory.Behaviors)]
    [DefaultValue(nameof(baseAmplitude), 5)]
    [DefaultValue(nameof(baseFrequency), 3)]
    [DefaultValue(nameof(baseWaveSize), 0)]
    public sealed class SlideBehavior : BehaviorScriptableSine
    {
        float sin;

        public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
        {
            sin = Mathf.Sin(frequency * animator.time.timeSinceStart 
                + character.index * waveSize) * amplitude * character.uniformIntensity;

            //bottom, torwards one direction
            character.current.positions[0] += Vector3.right * sin;
            character.current.positions[3] += Vector3.right * sin;
            //top, torwards the opposite dir
            character.current.positions[1] += Vector3.right * -sin;
            character.current.positions[2] += Vector3.right * -sin;
        }
    }
}