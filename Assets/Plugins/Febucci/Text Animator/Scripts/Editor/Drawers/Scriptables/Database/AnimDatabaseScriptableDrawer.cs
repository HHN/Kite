using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core;
using UnityEditor;

namespace Plugins.Febucci.Text_Animator.Scripts.Editor.Drawers.Scriptables.Database
{
    [CustomEditor(typeof(AnimationsDatabase), true)]
    class AnimDatabaseScriptableDrawer : UnityEditor.Editor
    {
        DatabaseSharedDrawer drawer = new DatabaseSharedDrawer();

        public override void OnInspectorGUI()
        {
            drawer.OnInspectorGUI(serializedObject);
        }
    }
}