using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfdestructionScript : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }
}
