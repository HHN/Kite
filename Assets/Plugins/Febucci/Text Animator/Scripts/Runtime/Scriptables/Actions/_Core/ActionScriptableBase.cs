using Febucci.UI.Core;
using Febucci.UI.Core.Parsing;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter._Core;

namespace Febucci.UI.Actions
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