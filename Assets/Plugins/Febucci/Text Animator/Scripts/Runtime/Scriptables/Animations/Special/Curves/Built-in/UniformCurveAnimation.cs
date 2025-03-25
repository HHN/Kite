using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Animator._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing.Regions._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Special.Curves._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Utilities;
using UnityEngine;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations.Special.Curves.Built_in
{
    [UnityEngine.Scripting.Preserve]
    [CreateAssetMenu(fileName = "Uniform Curve Animation", menuName = "Text Animator/_Animations/Special/Uniform Curve")]
    [EffectInfo("", EffectCategory.All)]
    public sealed class UniformCurveAnimation : AnimationScriptableBase
    {
        public TimeMode timeMode = new TimeMode(true);
        [EmissionCurveProperty] public EmissionCurve emissionCurve = new EmissionCurve();
        public AnimationData animationData = new AnimationData();

        //--- Modifiers ---
        float weightMult;
        float timeSpeed;

        bool hasTransformEffects;


        public override void ResetContext(TAnimCore animator)
        {
            weightMult = 1;
            timeSpeed = 1;
        }

        public override void SetModifier(ModifierInfo modifier)
        {
            switch (modifier.name)
            {
                case "f": //frequency, increases the time speed
                    timeSpeed = modifier.value;
                    break;

                case "a": //increase the amplitude
                    weightMult = modifier.value;
                    break;
            }
        }

        float timePassed;
        public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
        {
            timePassed = timeMode.GetTime(animator.time.timeSinceStart * timeSpeed, character.passedTime * timeSpeed, character.index);
            if (timePassed < 0)
                return;
            
            float weight = weightMult * emissionCurve.Evaluate(timePassed);
            
            if(animationData.TryCalculatingMatrix(character, timePassed, weight, out var matrix, out var offset))
            {
                for (byte i = 0; i < TextUtilities.verticesPerChar; i++)
                {
                    character.current.positions[i] = matrix.MultiplyPoint3x4(character.current.positions[i] - offset) + offset;
                }
            }

            if(animationData.TryCalculatingColor(character, timePassed, weight, out var color))
            {
                character.current.colors.LerpUnclamped(color, Mathf.Clamp01(weight));
            }

        }

        public override float GetMaxDuration() => emissionCurve.GetMaxDuration();

        public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator) => true;
    }
}