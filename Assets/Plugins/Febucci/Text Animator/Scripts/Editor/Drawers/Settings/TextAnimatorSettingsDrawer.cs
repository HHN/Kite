using Plugins.Febucci.Text_Animator.Scripts.Runtime.Scriptables.Settings;
using UnityEditor;
using UnityEngine;

namespace Plugins.Febucci.Text_Animator.Scripts.Editor.Drawers.Settings
{
    [CustomEditor(typeof(TextAnimatorSettings))]
    public class TextAnimatorSettingsDrawer : UnityEditor.Editor
    {
        bool extraSettings = false;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            extraSettings = EditorGUILayout.Foldout(extraSettings, "Extra Settings", EditorStyles.foldoutHeader);
            if (extraSettings)
            {
                if (GUILayout.Button("Reset Default Effects and Actions"))
                {
                    if (EditorUtility.DisplayDialog("Text Animator",
                            "Are you sure you want to reset the default effects and actions?", "Yes", "No"))
                    {
                        TextAnimatorSetupWindow.ResetToBuiltIn();
                    }
                }
            }
        }
    }
}