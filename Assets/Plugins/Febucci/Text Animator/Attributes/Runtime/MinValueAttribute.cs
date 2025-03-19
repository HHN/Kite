using UnityEngine;

namespace Plugins.Febucci.Text_Animator.Attributes.Runtime
{
    public class MinValueAttribute : PropertyAttribute
    {
        public float min = 0;
        public MinValueAttribute(float min)
        {
            this.min = min;
        }
    }

}