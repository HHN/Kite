using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing.Markers._Core;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing.Markers.Events
{
    /// <summary>
    /// Contains information about an event called in text
    /// </summary>
    public class EventMarker : MarkerBase
    {
        public EventMarker(string name, int index, int internalOrder, string[] parameters) : base(name, index, internalOrder, parameters) { }
    }
}