using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class NotarinNovelImageController : NovelImageController
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
    [SerializeField] private AudioClip decoGlasAudio;
    [SerializeField] private AudioClip decoPlantAudio;
    [SerializeField] private Sprite[] animationFramesPlant;
    [SerializeField] private Sprite[] animationFramesGlas;
    private GameObject background = null;
    private GameObject desk = null;
    private GameObject decoGlas = null;
    private GameObject decoPlant = null;
    private GameObject character = null;

    void Start()
    {
        SetInitialSpirtesForImages();
    }

    private void SetInitialSpirtesForImages()
    {
        Image image = decoGlasPrefab.GetComponent<Image>();
        image.sprite = animationFramesGlas[0];
        if (decoGlasContainer.transform.childCount > 0)
        {
            Destroy(decoGlasContainer.transform.GetChild(0).gameObject);
        }
        Instantiate(decoGlasPrefab, decoGlasContainer.transform);

        image = decoPlantPrefab.GetComponent<Image>();
        image.sprite = animationFramesPlant[0];
        if (decoPlantContainer.transform.childCount > 0)
        {
            Destroy(decoPlantContainer.transform.GetChild(0).gameObject);
        }
        Instantiate(decoPlantPrefab, decoPlantContainer.transform);
    }

    public override void SetBackground()
    {
        /*
        DestroyBackground();
        InstantiateBackground();
        RectTransform decoGlasRectTransform = decoGlasContainer.GetComponent<RectTransform>();
        if (decoGlasRectTransform != null && canvasRect != null)
        {
            decoGlasRectTransform.anchoredPosition = new Vector2(canvasRect.rect.width * 0.15f, canvasRect.rect.height * 0.1f);
            decoGlasRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.075f, canvasRect.rect.height * 0.1f);
        }
        
        RectTransform decoPlantRectTransform = decoPlantContainer.GetComponent<RectTransform>();
        if (decoPlantRectTransform != null && canvasRect != null)
        {
            decoPlantRectTransform.anchoredPosition = new Vector2(canvasRect.rect.width * 0.38f, canvasRect.rect.height * 0.25f);
            decoPlantRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.17f, canvasRect.rect.height * 0.25f);
        }

        NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
        NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
        */
    }

    private void DestroyBackground()
    {
        if (background != null)
        {
            Destroy(background);
        }
        if (desk != null)
        {
            Destroy(desk);
        }
        if (decoGlas != null)
        {
            Destroy(decoGlas);
        }
        if (decoPlant != null)
        {
            Destroy(decoPlant);
        }
    }

    private void InstantiateBackground()
    {
        /*
        background = Instantiate(backgroundPrefab, backgroundContainer.transform);
        desk = Instantiate(deskPrefab, deskContainer.transform);
        decoGlas = Instantiate(decoGlasPrefab, decoGlasContainer.transform);
        decoPlant = Instantiate(decoPlantPrefab, decoPlantContainer.transform);
        */
    }

    public override void DestroyCharacter()
    {
        if (character == null)
        {
            return;
        }
        Destroy(character);
    }

    public override void SetCharacter()
    {
        /*
        DestroyCharacter();
        character = Instantiate(characterPrefab, characterContainer.transform);
        RectTransform characterRectTransform = characterContainer.GetComponent<RectTransform>();
        characterRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.15f, 0);
        characterRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.25f, canvasRect.rect.height * 1f);
        */
        characterController = characterPrefab.GetComponent<CharacterController>();
    }

    public override bool HandleTouchEvent(float x, float y, AudioSource audioSource)
    {
        // Check if animations are allowed to proceed, return false if disabled
        if (AnimationFlagSingleton.Instance().GetFlag() == false)
        {
            return false;
        }

        // Get the RectTransforms of the objects to detect touch within their bounds
        RectTransform decoGlasRectTransform = decoGlasContainer.GetComponent<RectTransform>();
        RectTransform decoPlantRectTransform = decoPlantContainer.GetComponent<RectTransform>();

        // Get the world corners of the glass decoration container
        Vector3[] cornersDecoDesk = new Vector3[4];
        decoGlasRectTransform.GetWorldCorners(cornersDecoDesk);
        Vector3 bottomLeftDecoDesk = cornersDecoDesk[0];
        Vector3 topRightDecoDesk = cornersDecoDesk[2];

        // Get the world corners of the plant decoration container
        Vector3[] cornersDecoBackground = new Vector3[4];
        decoPlantRectTransform.GetWorldCorners(cornersDecoBackground);
        Vector3 bottomLeftDecoBackground = cornersDecoBackground[0];
        Vector3 topRightDecoBackground = cornersDecoBackground[2];

        // Check if the touch coordinates are within the glass decoration bounds
        if (x >= bottomLeftDecoDesk.x && x <= topRightDecoDesk.x &&
            y >= bottomLeftDecoDesk.y && y <= topRightDecoDesk.y)
        {
            StartCoroutine(OnDecoGlas(audioSource));
            return true;
        }
        // Check if the touch coordinates are within the plant decoration bounds
        else if (x >= bottomLeftDecoBackground.x && x <= topRightDecoBackground.x &&
                   y >= bottomLeftDecoBackground.y && y <= topRightDecoBackground.y)
        {
            StartCoroutine(OnDecoPlant(audioSource));
            return true;
        }

        // Return false if the touch is outside both bounds
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
                image.sprite = animationFramesGlas[6];
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