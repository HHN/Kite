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
    private GameObject background = null;
    private GameObject decoPlant = null;
    private GameObject character = null;

    public override void SetBackground()
    {
        DestroyBackground();
        InstantiateBackground();

        RectTransform decoPlantRectTransform = decoPlantContainer.GetComponent<RectTransform>();
        if (decoPlantRectTransform != null && canvasRect != null)
        {
            decoPlantRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.38f, canvasRect.rect.height * 0.235f);
            decoPlantRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.17f, canvasRect.rect.height * 0.25f);
        }

        NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
        NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
    }

    public override void SetCharacter()
    {
        DestroyCharacter();
        character = Instantiate(characterPrefab, characterContainer.transform);
        RectTransform characterRectTransform = characterContainer.GetComponent<RectTransform>();
        characterRectTransform.anchoredPosition = new Vector2(canvasRect.rect.width * 0.15f, 0);
        characterRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.25f, canvasRect.rect.height * 1f);
        characterController = character.GetComponent<CharacterController>();
    }

    public override bool HandleTouchEvent(float x, float y, AudioSource audioSource)
    {
        RectTransform decoPlantRectTransform = decoPlantContainer.GetComponent<RectTransform>();

        Vector3[] cornersDecoPlant = new Vector3[4];
        decoPlantRectTransform.GetWorldCorners(cornersDecoPlant);
        Vector3 bottomLeftDecoPlant = cornersDecoPlant[0];
        Vector3 topRightDecoPlant = cornersDecoPlant[2];
        if (x >= bottomLeftDecoPlant.x && x <= topRightDecoPlant.x &&
            y >= bottomLeftDecoPlant.y && y <= topRightDecoPlant.y)
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

    private void DestroyBackground()
    {
        if (background != null)
        {
            Destroy(background);
        }
        if (decoPlant != null)
        {
            Destroy(decoPlant);
        }
    }

    private void InstantiateBackground()
    {
        background = Instantiate(backgroundPrefab, backgroundContainer.transform);
        decoPlant = Instantiate(decoPlantPrefab, decoPlantContainer.transform);
    }

    public override void DestroyCharacter()
    {
        if (character == null)
        {
            return;
        }
        Destroy(character);
    }
}