using Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing.Markers.Actions;
using UnityEditor;

namespace Plugins.Febucci.Text_Animator.Scripts.Editor.Drawers.Scriptables.Database
{
    [CustomEditor(typeof(ActionDatabase), true)]
    class ActionDatabaseScriptableDrawer : UnityEditor.Editor
    {
        DatabaseSharedDrawer drawer = new DatabaseSharedDrawer();

        public override void OnInspectorGUI()
        {
            drawer.OnInspectorGUI(serializedObject);
        }
    }
}