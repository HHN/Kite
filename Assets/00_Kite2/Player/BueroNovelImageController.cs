using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BueroNovelImageController : NovelImageController
{
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private GameObject backgroundContainer;
    [SerializeField] private GameObject deskPrefab;
    [SerializeField] private GameObject deskContainer;
    [SerializeField] private GameObject decoDeskPrefab;
    [SerializeField] private GameObject decoDeskContainer;
    [SerializeField] private GameObject decoBackgroundPrefab;
    [SerializeField] private GameObject decoBackgroundContainer;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject characterContainer;
    [SerializeField] private AudioClip decoBackgroundAudio;
    [SerializeField] private Sprite[] animationFrames;

    public override void SetVisualElements(RectTransform canvasRect)
    {
        Instantiate(backgroundPrefab, backgroundContainer.transform);
        Instantiate(deskPrefab, deskContainer.transform);
        Instantiate(decoDeskPrefab, decoDeskContainer.transform);
        Instantiate(decoBackgroundPrefab, decoBackgroundContainer.transform);
        Instantiate(characterPrefab, characterContainer.transform);
        
        RectTransform characterRectTransform = characterContainer.GetComponent<RectTransform>();
        RectTransform decoDeskRectTransform = decoDeskContainer.GetComponent<RectTransform>();
        RectTransform decoBackgroundRectTransform = decoBackgroundContainer.GetComponent<RectTransform>();
            
        characterRectTransform.anchoredPosition = new Vector2(canvasRect.rect.width * 0.15f, 0);
        decoDeskRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.2f, canvasRect.rect.height * 0.1f);
        decoBackgroundRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.38f, canvasRect.rect.height * 0.235f);

        characterRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.25f, canvasRect.rect.height * 1f);
        decoDeskRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.075f, canvasRect.rect.height * 0.1f);
        decoBackgroundRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.17f, canvasRect.rect.height * 0.25f);

        NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
        NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
    }

    public override bool HandleTouchEvent(float x, float y, AudioSource audioSource)
    {
        RectTransform decoBackgroundRectTransform = decoBackgroundContainer.GetComponent<RectTransform>();

        Vector3[] cornersDecoBackground = new Vector3[4];
        decoBackgroundRectTransform.GetWorldCorners(cornersDecoBackground);
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
            audioSource.clip = decoBackgroundAudio;
            if (audioSource.clip != null)
            {
                Debug.Log("Start Animation");
                audioSource.Play();
                Image image = decoBackgroundPrefab.GetComponent<Image>();
                image.sprite = animationFrames[1];
                Destroy(decoBackgroundContainer.transform.GetChild(0).gameObject);
                Instantiate(decoBackgroundPrefab, decoBackgroundContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFrames[2];
                Destroy(decoBackgroundContainer.transform.GetChild(0).gameObject);
                Instantiate(decoBackgroundPrefab, decoBackgroundContainer.transform);
                yield return new WaitForSeconds(0.5f);
                image.sprite = animationFrames[0];
                Destroy(decoBackgroundContainer.transform.GetChild(0).gameObject);
                Instantiate(decoBackgroundPrefab, decoBackgroundContainer.transform);
            }
            else
            {
                Debug.LogError("AudioClip couldn't be found.");
            }
        }
        yield return new WaitForSeconds(0f);
    }
}