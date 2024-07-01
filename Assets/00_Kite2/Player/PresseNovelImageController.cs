using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresseNovelImageController : NovelImageController
{
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private GameObject backgroundContainer;
    [SerializeField] private GameObject deskPrefab;
    [SerializeField] private GameObject deskContainer;
    [SerializeField] private GameObject decoVasePrefab;
    [SerializeField] private GameObject decoVaseContainer;
    [SerializeField] private GameObject decoGlasPrefab;
    [SerializeField] private GameObject decoGlasContainer;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject characterContainer;
    [SerializeField] private AudioClip decoVaseAudio;
    [SerializeField] private Sprite[] animationFrames;

    public override void SetVisualElements(RectTransform canvasRect)
    {
        Instantiate(backgroundPrefab, backgroundContainer.transform);
        Instantiate(deskPrefab, deskContainer.transform);
        Instantiate(decoVasePrefab, decoVaseContainer.transform);
        Instantiate(decoGlasPrefab, decoGlasContainer.transform);
        Instantiate(characterPrefab, characterContainer.transform);
        
        RectTransform characterRectTransform = characterContainer.GetComponent<RectTransform>();
        RectTransform decoVaseRectTransform = decoVaseContainer.GetComponent<RectTransform>();
        RectTransform decoGlasRectTransform = decoGlasContainer.GetComponent<RectTransform>();
        RectTransform deskRectTransform = deskContainer.GetComponent<RectTransform>();
            
        characterRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.1f, 0);
        decoVaseRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.1f, canvasRect.rect.height * 0.12f);
        decoGlasRectTransform.anchoredPosition = new Vector2(canvasRect.rect.width * 0.25f, canvasRect.rect.height * 0.08f);
        deskRectTransform.anchoredPosition = new Vector2(0, -canvasRect.rect.height * 0.05f);

        characterRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.25f, canvasRect.rect.height * 1f);
        decoVaseRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.09f, canvasRect.rect.height * 0.265f);
        decoGlasRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.064f, canvasRect.rect.height * 0.08f);
        deskRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.35f, canvasRect.rect.height * 0.25f);

        NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
        NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
    }

    public override bool HandleTouchEvent(float x, float y, AudioSource audioSource)
    {
        RectTransform decoVaseRectTransform = decoVaseContainer.GetComponent<RectTransform>();

        Vector3[] cornersDecoVase = new Vector3[4];
        decoVaseRectTransform.GetWorldCorners(cornersDecoVase);
        Vector3 bottomLeftDecoVase = cornersDecoVase[0];
        Vector3 topRightDecoVase = cornersDecoVase[2];
        if (x >= bottomLeftDecoVase.x && x <= topRightDecoVase.x &&
            y >= bottomLeftDecoVase.y && y <= topRightDecoVase.y)
        {
            StartCoroutine(OnDecoVase(audioSource));
            return true;
        }
        return false;
    }

    private IEnumerator OnDecoVase(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.clip = decoVaseAudio;
            if (audioSource.clip != null)
            {
                audioSource.Play();
                Image image = decoVasePrefab.GetComponent<Image>();
                image.sprite = animationFrames[1];
                Destroy(decoVaseContainer.transform.GetChild(0).gameObject);
                Instantiate(decoVasePrefab, decoVaseContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFrames[2];
                Destroy(decoVaseContainer.transform.GetChild(0).gameObject);
                Instantiate(decoVasePrefab, decoVaseContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFrames[0];
                Destroy(decoVaseContainer.transform.GetChild(0).gameObject);
                Instantiate(decoVasePrefab, decoVaseContainer.transform);
            }
            else
            {
                Debug.LogError("AudioClip couldn't be found.");
            }
        }
        yield return new WaitForSeconds(0f);
    }

    
}

    