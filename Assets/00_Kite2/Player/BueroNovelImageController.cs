using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BueroNovelImageController : NovelImageController
{
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private GameObject backgroundContainer;
    [SerializeField] private GameObject decoPlantPrefab;
    [SerializeField] private GameObject decoPlantContainer;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject characterContainer;
    [SerializeField] private AudioClip decoPlantAudio;
    [SerializeField] private Sprite[] animationFrames;

    public override void SetVisualElements(RectTransform canvasRect)
    {
        Instantiate(backgroundPrefab, backgroundContainer.transform);
        Instantiate(decoPlantPrefab, decoPlantContainer.transform);
        Instantiate(characterPrefab, characterContainer.transform);
        
        RectTransform characterRectTransform = characterContainer.GetComponent<RectTransform>();
        RectTransform decoPlantRectTransform = decoPlantContainer.GetComponent<RectTransform>();
            
        characterRectTransform.anchoredPosition = new Vector2(canvasRect.rect.width * 0.15f, 0);
        decoPlantRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.38f, canvasRect.rect.height * 0.235f);

        characterRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.25f, canvasRect.rect.height * 1f);
        decoPlantRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.17f, canvasRect.rect.height * 0.25f);

        NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
        NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
    }

    public override bool HandleTouchEvent(float x, float y, AudioSource audioSource)
    {
        RectTransform decoPlantRectTransform = decoPlantContainer.GetComponent<RectTransform>();

        Vector3[] cornersDecoBackground = new Vector3[4];
        decoPlantRectTransform.GetWorldCorners(cornersDecoBackground);
        Vector3 bottomLeftDecoBackground = cornersDecoBackground[0];
        Vector3 topRightDecoBackground = cornersDecoBackground[2];
        if (x >= bottomLeftDecoBackground.x && x <= topRightDecoBackground.x &&
            y >= bottomLeftDecoBackground.y && y <= topRightDecoBackground.y)
        {
            StartCoroutine(OnDecoBackground(audioSource));
            return true;
        }
        return false;
    }

    private IEnumerator OnDecoBackground(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.clip = decoPlantAudio;
            if (audioSource.clip != null)
            {
                audioSource.Play();
                Image image = decoPlantPrefab.GetComponent<Image>();
                image.sprite = animationFrames[1];
                Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                Instantiate(decoPlantPrefab, decoPlantContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFrames[2];
                Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
                Instantiate(decoPlantPrefab, decoPlantContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFrames[0];
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
}