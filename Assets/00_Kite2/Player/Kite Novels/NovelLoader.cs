using UnityEngine;

public class NovelLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(new NovelReader().LoadAllNovels());
    }
}
