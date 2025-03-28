using UnityEngine;

namespace Assets._Scripts.UI_Elements.Buttons
{
    public class SelfdestructionScript : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 5f);
        }
    }
}
