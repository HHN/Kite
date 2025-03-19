using Plugins.Febucci.Text_Animator.Scripts.Runtime.Components.Typewriter.Built_in;
using UnityEditor;

namespace Plugins.Febucci.Text_Animator.Scripts.Editor.Drawers.Components
{
    [CustomEditor(typeof(TypewriterByWord), true)]
    class TypewriterByWordDrawer : TypewriterCoreDrawer
    {
        SerializedProperty waitForNormalWord;
        SerializedProperty waitForWordWithPunctuation;
        PropertyWithDifferentLabel disappearanceDelay;

        protected override void OnEnable()
        {
            base.OnEnable();

            waitForNormalWord = serializedObject.FindProperty("waitForNormalWord");
            waitForWordWithPunctuation = serializedObject.FindProperty("waitForWordWithPunctuation");
            disappearanceDelay = new PropertyWithDifferentLabel(serializedObject, "disappearanceDelay", "Disappearances Wait");
        }

        protected override string[] GetPropertiesToExclude()
        {
            string[] newProperties = new string[] {
                "script",
                "waitForNormalWord",
                "waitForWordWithPunctuation",
                "disappearanceDelay",
            };

            string[] baseProperties = base.GetPropertiesToExclude();

            string[] mergedArray = new string[newProperties.Length + baseProperties.Length];

            for (int i = 0; i < baseProperties.Length; i++)
            {
                mergedArray[i] = baseProperties[i];
            }

            for (int i = 0; i < newProperties.Length; i++)
            {
                mergedArray[i + baseProperties.Length] = newProperties[i];
            }

            return mergedArray;
        }

        protected override void OnTypewriterSectionGUI()
        {
            EditorGUILayout.PropertyField(waitForNormalWord);
            EditorGUILayout.PropertyField(waitForWordWithPunctuation);
        }

        protected override void OnDisappearanceSectionGUI()
        {
            disappearanceDelay.PropertyField();
        }
    }
}