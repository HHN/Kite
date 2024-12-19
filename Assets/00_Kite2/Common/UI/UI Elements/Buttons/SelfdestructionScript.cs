using UnityEngine;

namespace _00_Kite2.Common.UI.UI_Elements.Buttons
{
    public class SelfdestructionScript : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 5f);
        }
    }
}
