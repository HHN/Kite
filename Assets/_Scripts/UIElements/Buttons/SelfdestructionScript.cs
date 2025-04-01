using UnityEngine;

namespace Assets._Scripts.UIElements.Buttons
{
    public class SelfdestructionScript : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 5f);
        }
    }
}
