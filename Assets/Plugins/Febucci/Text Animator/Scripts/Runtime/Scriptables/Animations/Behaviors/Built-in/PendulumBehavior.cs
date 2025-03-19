using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Animator._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Behaviors._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Utilities;
using UnityEngine;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Behaviors.Built_in
{
    [UnityEngine.Scripting.Preserve]
    [CreateAssetMenu(fileName = "Pendulum Behavior", menuName = "Text Animator/_Animations/Behaviors/Pendulum")]
    [EffectInfo("pend", EffectCategory.Behaviors)]
    [DefaultValue(nameof(baseAmplitude), 24.7f)]
    [DefaultValue(nameof(baseFrequency), 3.1f)]
    [DefaultValue(nameof(baseWaveSize), .2f)]
    public sealed class PendulumBehavior : BehaviorScriptableSine
    {
        public bool anchorBottom;

        int targetVertex1;
        int targetVertex2;

        public override void ResetContext(TAnimCore animator)
        {
            base.ResetContext(animator);

            if (anchorBottom)
            {
                //anchored at the bottom
                targetVertex1 = 0;
                targetVertex2 = 3;
            }
            else
            {
                //anchored at the top
                targetVertex1 = 1;
                targetVertex2 = 2;
            }
        }

        public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
        {
            character.current.positions.RotateChar(
                Mathf.Sin(-animator.time.timeSinceStart * frequency + waveSize * character.index) * amplitude,
                (character.current.positions[targetVertex1] + character.current.positions[targetVertex2]) / 2 //bottom center as their rotation pivot
                );
        }
    }

}