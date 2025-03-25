using System;

namespace Plugins.Febucci.Text_Animator.Scripts.Runtime.Parsing.Regions._Core
{
    public struct ModifierInfo: IEquatable<ModifierInfo>
    {
        public string name;
        public float value;

        public ModifierInfo(string name, float value)
        {
            this.name = name;
            this.value = value;
        }

        public bool Equals(ModifierInfo other) => value.Equals(other.value) && name.Equals(other.name);

        public override string ToString() => $"{name}={value}";
    }

}