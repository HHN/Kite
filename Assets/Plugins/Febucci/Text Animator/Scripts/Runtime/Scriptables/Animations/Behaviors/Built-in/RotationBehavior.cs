using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Animator._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing.Regions._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Behaviors._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Utilities;
using UnityEngine;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Behaviors.Built_in
{
    [UnityEngine.Scripting.Preserve]
    [CreateAssetMenu(menuName = "Text Animator/_Animations/Behaviors/Rotation", fileName = "Rotation Behavior")]
    [EffectInfo("rot", EffectCategory.Behaviors)]
    public sealed class RotationBehavior : BehaviorScriptableBase
    {

        public float baseRotSpeed = 180;
        public float baseDiffBetweenChars = 10;

        float angleSpeed;
        float angleDiffBetweenChars;

        public override void SetModifier(ModifierInfo modifier)
        {
            switch (modifier.name)
            {
                //frequency
                case "f": angleSpeed = baseRotSpeed * modifier.value; break;
                //angle diff
                case "w": angleDiffBetweenChars = baseDiffBetweenChars * modifier.value; break;
            }
        }

        public override void ResetContext(TAnimCore animator)
        {
            angleSpeed = baseRotSpeed;
            angleDiffBetweenChars = baseDiffBetweenChars;
        }

        public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
        {
            character.current.positions.RotateChar(-animator.time.timeSinceStart * angleSpeed 
                + angleDiffBetweenChars * character.index);
        }
    }

}