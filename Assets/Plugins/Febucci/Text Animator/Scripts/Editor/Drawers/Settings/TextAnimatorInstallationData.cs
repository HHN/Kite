using UnityEngine;

namespace Plugins.Febucci.Text_Animator.Scripts.Editor.Drawers.Settings
{
    [System.Serializable]
    internal class TextAnimatorInstallationData : ScriptableObject
    {
        [SerializeField] internal string latestVersion = "None"; //stores the latest version
    }
}