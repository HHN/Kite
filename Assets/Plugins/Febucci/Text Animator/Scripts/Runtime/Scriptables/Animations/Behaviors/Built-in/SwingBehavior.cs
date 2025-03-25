using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Animator._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Behaviors._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Utilities;
using UnityEngine;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Behaviors.Built_in
{
    [UnityEngine.Scripting.Preserve]
    [CreateAssetMenu(menuName = "Text Animator/_Animations/Behaviors/Swing", fileName = "Swing Behavior")]
    [EffectInfo("swing", EffectCategory.Behaviors)]
    [DefaultValue(nameof(baseAmplitude), 22.74f)]
    [DefaultValue(nameof(baseFrequency), 3.65f)]
    [DefaultValue(nameof(baseWaveSize), .171f)]
    public sealed class SwingBehavior : BehaviorScriptableSine
    {
        public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
        {           
            character.current.positions.RotateChar(Mathf.Cos(animator.time.timeSinceStart * frequency + character.index * waveSize) * amplitude);
        }
    }
}