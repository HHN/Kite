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
    [CreateAssetMenu(menuName = "Text Animator/_Animations/Behaviors/Rainbow", fileName = "Rainbow Behavior")]
    [EffectInfo("rainb", EffectCategory.Behaviors)]
    public sealed class RainbowBehavior : BehaviorScriptableBase
    {
        public float baseFrequency = 0.5f;
        public float baseWaveSize = 0.08f;


        float frequency;
        float waveSize;
        public override void SetModifier(ModifierInfo modifier)
        {
            switch (modifier.name)
            {
                //frequency
                case "f": frequency = baseFrequency * modifier.value; break;
                //wave size
                case "s": waveSize = baseWaveSize * modifier.value; break;
            }
        }

        public override void ResetContext(TAnimCore animator)
        {
            frequency = baseFrequency;
            waveSize = baseWaveSize;
        }

        Color32 temp;
        public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
        {
            for (byte i = 0; i < TextUtilities.verticesPerChar; i++)
            {
                //shifts hue
                temp = Color.HSVToRGB(Mathf.PingPong(animator.time.timeSinceStart * frequency + character.index * waveSize, 1), 1, 1);
                temp.a = character.current.colors[i].a; //preserves original alpha
                character.current.colors[i] = temp;
            }
        }
    }
}