using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class BankNovelImageController : NovelImageController
{
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private GameObject backgroundContainer;
    [SerializeField] private GameObject deskPrefab;
    [SerializeField] private GameObject deskContainer;
    [SerializeField] private GameObject decoGlasPrefab;
    [SerializeField] private GameObject decoGlasContainer;
    [SerializeField] private GameObject decoPlantPrefab;
    [SerializeField] private GameObject decoPlantContainer;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject characterContainer;
    [SerializeField] private AudioClip decoGlasAudio;
    [SerializeField] private AudioClip decoPlantAudio;
    [SerializeField] private Sprite[] animationFramesPlant;
    [SerializeField] private Sprite[] animationFramesGlas;

    public override void SetVisualElements(RectTransform canvasRect)
    {
        Instantiate(backgroundPrefab, backgroundContainer.transform);
        Instantiate(deskPrefab, deskContainer.transform);
        Instantiate(decoGlasPrefab, decoGlasContainer.transform);
        Instantiate(decoPlantPrefab, decoPlantContainer.transform);
        Instantiate(characterPrefab, characterContainer.transform);
        
        RectTransform characterRectTransform = characterContainer.GetComponent<RectTransform>();
        RectTransform decoGlasRectTransform = decoGlasContainer.GetComponent<RectTransform>();
        RectTransform decoPlantRectTransform = decoPlantContainer.GetComponent<RectTransform>();
            
        characterRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.15f, 0);
        decoGlasRectTransform.anchoredPosition = new Vector2(canvasRect.rect.width * 0.15f, canvasRect.rect.height * 0.1f);
        decoPlantRectTransform.anchoredPosition = new Vector2(canvasRect.rect.width * 0.38f, canvasRect.rect.height * 0.25f);

        characterRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.25f, canvasRect.rect.height * 1f);
        decoGlasRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.075f, canvasRect.rect.height * 0.1f);
        decoPlantRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.17f, canvasRect.rect.height * 0.25f);

        NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
        NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
    }

    public override bool HandleTouchEvent(float x, float y, AudioSource audioSource)
    {
        RectTransform decoGlasRectTransform = decoGlasContainer.GetComponent<RectTransform>();
        RectTransform decoPlantRectTransform = decoPlantContainer.GetComponent<RectTransform>();

        Vector3[] cornersDecoDesk = new Vector3[4];
        decoGlasRectTransform.GetWorldCorners(cornersDecoDesk);
        Vector3 bottomLeftDecoDesk = cornersDecoDesk[0];
        Vector3 topRightDecoDesk = cornersDecoDesk[2];

        Vector3[] cornersDecoBackground = new Vector3[4];
        decoPlantRectTransform.GetWorldCorners(cornersDecoBackground);
        Vector3 bottomLeftDecoBackground = cornersDecoBackground[0];
        Vector3 topRightDecoBackground = cornersDecoBackground[2];



        if (x >= bottomLeftDecoDesk.x && x <= topRightDecoDesk.x &&
            y >= bottomLeftDecoDesk.y && y <= topRightDecoDesk.y)
        {
            StartCoroutine(OnDecoGlas(audioSource));
            return true;
        } 
        else if (x >= bottomLeftDecoBackground.x && x <= topRightDecoBackground.x &&
                   y >= bottomLeftDecoBackground.y && y <= topRightDecoBackground.y)
        {
            StartCoroutine(OnDecoPlant(audioSource));
            return true;
        }
        return false;
    }

    private IEnumerator OnDecoPlant(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.clip = decoPlantAudio;
            if (audioSource.clip != null)
            {
                audioSource.Play();
                Image image = decoPlantPrefab.GetComponent<Image>();
                image.sprite = animationFramesPlant[1];
                Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                Instantiate(decoPlantPrefab, decoPlantContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesPlant[2];
                Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                Instantiate(decoPlantPrefab, decoPlantContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesPlant[0];
                Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                Instantiate(decoPlantPrefab, decoPlantContainer.transform);
            }
            else
            {
                Debug.LogError("AudioClip couldn't be found.");
            }
        }
        yield return new WaitForSeconds(0f);
    }

    private IEnumerator OnDecoGlas(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.clip = decoGlasAudio;
            if (audioSource.clip != null)
            {
                audioSource.Play();
                Image image = decoGlasPrefab.GetComponent<Image>();
                image.sprite = animationFramesGlas[1];
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesGlas[2];
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesGlas[3];
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesGlas[4];
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesGlas[5];
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                Instantiate(decoGlasPrefab, decoGlasContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFramesGlas[0];
                Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
                Instantiate(decoGlasPrefab, decoGlasContainer.transform);
            }
            else
            {
                Debug.LogError("AudioClip couldn't be found.");
            }
        }
        yield return new WaitForSeconds(0f);
    }
}