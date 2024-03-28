using System.Collections.Generic;
using UnityEngine;

public class NovelLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(new NovelReader().LoadAllNovelsWithJsonAproach());
    }
}
