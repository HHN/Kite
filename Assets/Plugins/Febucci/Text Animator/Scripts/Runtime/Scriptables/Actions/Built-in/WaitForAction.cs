using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing.Markers.Actions;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Actions._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Utilities;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Actions.Built_in
{
    [System.Serializable]
    [UnityEngine.CreateAssetMenu(fileName = "WaitFor Action", menuName = "Text Animator/Actions/Wait For", order = 1)]
    [TagInfo("waitfor")]
    public sealed class WaitForAction : ActionScriptableBase
    {
        /// <summary>
        /// Time used in case the action does not have the first parameter
        /// </summary>
        [UnityEngine.Tooltip("Time used in case the action does not have the first parameter")]
        public float defaultTime = 1;

        public override System.Collections.IEnumerator DoAction(ActionMarker action, TypewriterCore typewriter, TypingInfo typingInfo)
        {
            float targetTime = defaultTime;
            if(action.parameters.Length > 0)
            {
                FormatUtils.TryGetFloat(action.parameters[0], defaultTime, out targetTime);
            }

            float t = 0;
            while(t<=targetTime)
            {
                t += typewriter.TextAnimator.time.deltaTime;
                yield return null;
            }
        }
    }
}