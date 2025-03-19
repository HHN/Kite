using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing.Markers.Actions;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Actions._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core;
using UnityEngine;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Actions.Built_in
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "WaitAnyInput Action", menuName = "Text Animator/Actions/Wait Any Input", order = 1)]
    [TagInfo("waitinput")]
    public sealed class WaitAnyInputAction : ActionScriptableBase
    {
        public override System.Collections.IEnumerator DoAction(ActionMarker action, TypewriterCore typewriter, TypingInfo typingInfo)
        {
            while(!Input.anyKeyDown)
                yield return null;
        }
    }
}