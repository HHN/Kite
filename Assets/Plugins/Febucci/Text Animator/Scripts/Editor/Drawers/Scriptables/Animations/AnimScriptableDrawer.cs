using Plugins.Febucci.Text_Animator.Scripts.Editor.Drawers._Core;
using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Animations._Core;
using UnityEditor;

namespace Plugins.Febucci.Text_Animator.Scripts.Editor.Drawers.Scriptables.Animations
{
    [CustomEditor(typeof(AnimationScriptableBase), true)]
    class AnimScriptableDrawer : UnityEditor.Editor
    {
        GenericSharedDrawer drawer = new GenericSharedDrawer(true);
        public override void OnInspectorGUI()
        {
            drawer.OnInspectorGUI(serializedObject);
        }
    }
}