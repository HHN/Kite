using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElternNovelImageController : NovelImageController
{
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private GameObject backgroundContainer;
    [SerializeField] private GameObject deskPrefab;
    [SerializeField] private GameObject deskContainer;
    [SerializeField] private GameObject decoTasse1Prefab;
    [SerializeField] private GameObject decoTasse1Container;
    [SerializeField] private GameObject decoTasse2Prefab;
    [SerializeField] private GameObject decoTasse2Container;
    [SerializeField] private GameObject decoKannePrefab;
    [SerializeField] private GameObject decoKanneContainer;
    [SerializeField] private GameObject decoLampePrefab;
    [SerializeField] private GameObject decoLampeContainer;
    [SerializeField] private AudioClip decoTasseAudio;
    [SerializeField] private AudioClip decoKanneAudio;
    [SerializeField] private AudioClip decoLampeOnAudio;
    [SerializeField] private AudioClip decoLampeOffAudio;
    [SerializeField] private Sprite[] animationFramesTasse1;
    [SerializeField] private Sprite[] animationFramesTasse2;
    [SerializeField] private Sprite[] animationFramesKanne;
    [SerializeField] private Sprite decoLampeOff;
    [SerializeField] private Sprite decoLampeOn;
    private bool decoLampeStatus = true;

    public override void SetBackground()
    {
        
        
        NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
        NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
    }

    

    public override bool HandleTouchEvent(float x, float y, AudioSource audioSource)
    {
        RectTransform decoTasse1RectTransform = decoTasse1Container.GetComponent<RectTransform>();
        RectTransform decoTasse2RectTransform = decoTasse2Container.GetComponent<RectTransform>();
        RectTransform decoKanneRectTransform = decoKanneContainer.GetComponent<RectTransform>();
        RectTransform decoLampeRectTransform = decoLampeContainer.GetComponent<RectTransform>();

        Vector3[] cornersDecoTasse1 = new Vector3[4];
        decoTasse1RectTransform.GetWorldCorners(cornersDecoTasse1);
        Vector3 bottomLeftDecoTasse1 = cornersDecoTasse1[0];
        Vector3 topRightDecoTasse1 = cornersDecoTasse1[2];

        Vector3[] cornersDecoTasse2 = new Vector3[4];
        decoTasse2RectTransform.GetWorldCorners(cornersDecoTasse2);
        Vector3 bottomLeftDecoTasse2 = cornersDecoTasse2[0];
        Vector3 topRightDecoTasse2 = cornersDecoTasse2[2];

        Vector3[] cornersDecoKanne = new Vector3[4];
        decoKanneRectTransform.GetWorldCorners(cornersDecoKanne);
        Vector3 bottomLeftDecoKanne = cornersDecoKanne[0];
        Vector3 topRightDecoKanne = cornersDecoKanne[2];

        Vector3[] cornersDecoLampe = new Vector3[4];
        decoLampeRectTransform.GetWorldCorners(cornersDecoLampe);
        Vector3 bottomLeftDecoLampe = cornersDecoLampe[0];
        Vector3 topRightDecoLampe = cornersDecoLampe[2];

        if (x >= bottomLeftDecoTasse1.x && x <= topRightDecoTasse1.x &&
            y >= bottomLeftDecoTasse1.y && y <= topRightDecoTasse1.y)
        {
            StartCoroutine(OnDecoTasse1(audioSource));
            return true;
        }
        else if (x >= bottomLeftDecoTasse2.x && x <= topRightDecoTasse2.x &&
                 y >= bottomLeftDecoTasse2.y && y <= topRightDecoTasse2.y)
        {
            StartCoroutine(OnDecoTasse2(audioSource));
            return true;
        }
        else if (x >= bottomLeftDecoKanne.x && x <= topRightDecoKanne.x &&
                 y >= bottomLeftDecoKanne.y && y <= topRightDecoKanne.y)
        {
            StartCoroutine(OnDecoKanne(audioSource));
            return true;
        }
        else if (x >= bottomLeftDecoLampe.x && x <= topRightDecoLampe.x &&
                 y >= bottomLeftDecoLampe.y && y <= topRightDecoLampe.y)
        {
            StartCoroutine(OnDecoLampe(audioSource));
            return true;
        }
        return false;
    }

    private IEnumerator OnDecoTasse1(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.clip = decoTasseAudio;
            if (audioSource.clip != null)
            {
                audioSource.Play();
                Image image = decoTasse1Prefab.GetComponent<Image>();
                image.sprite = animationFramesTasse1[1];
                if (decoTasse1Container.transform.childCount > 0)
                {
                    Destroy(decoTasse1Container.transform.GetChild(0).gameObject);
                }
                Instantiate(decoTasse1Prefab, decoTasse1Container.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesTasse1[2];
                if (decoTasse1Container.transform.childCount > 0)
                {
                    Destroy(decoTasse1Container.transform.GetChild(0).gameObject);
                }
                Instantiate(decoTasse1Prefab, decoTasse1Container.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesTasse1[3];
                if (decoTasse1Container.transform.childCount > 0)
                {
                    Destroy(decoTasse1Container.transform.GetChild(0).gameObject);
                }
                Instantiate(decoTasse1Prefab, decoTasse1Container.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesTasse1[0];
                if (decoTasse1Container.transform.childCount > 0)
                {
                    Destroy(decoTasse1Container.transform.GetChild(0).gameObject);
                }
                Instantiate(decoTasse1Prefab, decoTasse1Container.transform);
            }
            else
            {
                Debug.LogError("AudioClip couldn't be found.");
            }
        }
        yield return new WaitForSeconds(0f);
    }

    private IEnumerator OnDecoTasse2(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.clip = decoTasseAudio;
            if (audioSource.clip != null)
            {
                audioSource.Play();
                Image image = decoTasse2Prefab.GetComponent<Image>();
                image.sprite = animationFramesTasse2[1];
                if (decoTasse2Container.transform.childCount > 0)
                {
                    Destroy(decoTasse2Container.transform.GetChild(0).gameObject);
                }
                Instantiate(decoTasse2Prefab, decoTasse2Container.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesTasse2[2];
                if (decoTasse2Container.transform.childCount > 0)
                {
                    Destroy(decoTasse2Container.transform.GetChild(0).gameObject);
                }
                Instantiate(decoTasse2Prefab, decoTasse2Container.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesTasse2[3];
                if (decoTasse2Container.transform.childCount > 0)
                {
                    Destroy(decoTasse2Container.transform.GetChild(0).gameObject);
                }
                Instantiate(decoTasse2Prefab, decoTasse2Container.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesTasse2[0];
                if (decoTasse2Container.transform.childCount > 0)
                {
                    Destroy(decoTasse2Container.transform.GetChild(0).gameObject);
                }
                Instantiate(decoTasse2Prefab, decoTasse2Container.transform);
            }
            else
            {
                Debug.LogError("AudioClip couldn't be found.");
            }
        }
        yield return new WaitForSeconds(0f);
    }

    private IEnumerator OnDecoKanne(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.clip = decoKanneAudio;
            if (audioSource.clip != null)
            {
                audioSource.Play();
                Image image = decoKannePrefab.GetComponent<Image>();
                image.sprite = animationFramesKanne[1];
                if (decoKanneContainer.transform.childCount > 0)
                {
                    Destroy(decoKanneContainer.transform.GetChild(0).gameObject);
                }
                Instantiate(decoKannePrefab, decoKanneContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesKanne[2];
                if (decoKanneContainer.transform.childCount > 0)
                {
                    Destroy(decoKanneContainer.transform.GetChild(0).gameObject);
                }
                Instantiate(decoKannePrefab, decoKanneContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesKanne[3];
                if (decoKanneContainer.transform.childCount > 0)
                {
                    Destroy(decoKanneContainer.transform.GetChild(0).gameObject);
                }
                Instantiate(decoKannePrefab, decoKanneContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesKanne[0];
                if (decoKanneContainer.transform.childCount > 0)
                {
                    Destroy(decoKanneContainer.transform.GetChild(0).gameObject);
                }
                Instantiate(decoKannePrefab, decoKanneContainer.transform);
            }
            else
            {
                Debug.LogError("AudioClip couldn't be found.");
            }
        }
        yield return new WaitForSeconds(0f);
    }

    private IEnumerator OnDecoLampe(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            if (decoLampeStatus)
            {
                audioSource.clip = decoLampeOffAudio;
            }
            else
            {
                audioSource.clip = decoLampeOnAudio;
            }
            if (audioSource.clip != null)
            {
                audioSource.Play();
                if (decoLampeStatus)
                {
                    Image image = decoLampePrefab.GetComponent<Image>();
                    image.sprite = decoLampeOff;
                    if (decoLampeContainer.transform.childCount > 0)
                    {
                        Destroy(decoLampeContainer.transform.GetChild(0).gameObject);
                    }
                    Instantiate(decoLampePrefab, decoLampeContainer.transform);
                    decoLampeStatus = false;
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    Image image = decoLampePrefab.GetComponent<Image>();
                    image.sprite = decoLampeOn;
                    if (decoLampeContainer.transform.childCount > 0)
                    {
                        Destroy(decoLampeContainer.transform.GetChild(0).gameObject);
                    }
                    Instantiate(decoLampePrefab, decoLampeContainer.transform);
                    decoLampeStatus = true;
                    yield return new WaitForSeconds(0.5f);
                }
                
            }
            else
            {
                Debug.LogError("AudioClip couldn't be found.");
            }
        }
        yield return new WaitForSeconds(0f);
    }

    
}

    