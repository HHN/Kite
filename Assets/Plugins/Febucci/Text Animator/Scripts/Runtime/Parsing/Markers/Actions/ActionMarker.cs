using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing.Markers._Core;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing.Markers.Actions
{
    /// <summary>
    /// Contains information about an action tag in text.
    /// </summary>
    public sealed class ActionMarker : MarkerBase
    {
        public ActionMarker(string name, int index, int internalOrder, string[] parameters) : base(name, index, internalOrder, parameters) { }
    }
}