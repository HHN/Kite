using Plugins.Febucci.Text_Animator.Scripts.Editor.Drawers._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Actions._Core;
using UnityEditor;

namespace Plugins.Febucci.Text_Animator.Scripts.Editor.Drawers.Scriptables.Actions
{
    [CustomEditor(typeof(ActionScriptableBase), true)]
    class ActionScriptableDrawer : UnityEditor.Editor
    {
        GenericSharedDrawer drawer = new GenericSharedDrawer(true);
        public override void OnInspectorGUI()
        {
            drawer.OnInspectorGUI(serializedObject);
        }
    }
}