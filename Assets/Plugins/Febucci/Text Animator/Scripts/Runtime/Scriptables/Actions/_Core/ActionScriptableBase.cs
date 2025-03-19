using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing.Markers.Actions;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Actions._Core
{
    [System.Serializable]
    public abstract class ActionScriptableBase : UnityEngine.ScriptableObject, ITagProvider
    {
        [UnityEngine.SerializeField] string tagID;
        public string TagID
        {
            get => tagID;
            set => tagID = value;
        }

        public abstract System.Collections.IEnumerator DoAction(ActionMarker action, TypewriterCore typewriter, TypingInfo typingInfo);
    }
}