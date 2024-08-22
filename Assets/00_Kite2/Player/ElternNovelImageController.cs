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
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject characterContainer;
    [SerializeField] private AudioClip decoGlasAudio;
    [SerializeField] private AudioClip decoVaseAudio;
    [SerializeField] private Sprite[] animationFramesVase;
    [SerializeField] private Sprite[] animationFramesGlas;
    [SerializeField] private Sprite[] faceSprites;   
    private GameObject background = null;
    private GameObject desk = null;
    private GameObject decoGlas = null;
    private GameObject decoVase = null;
    private GameObject character = null; 

    public override void SetBackground()
    {
        DestroyBackground();
        InstantiateBackground();
        
        //RectTransform decoVaseRectTransform = decoVaseContainer.GetComponent<RectTransform>();
        //if (decoVaseRectTransform != null && canvasRect != null)
        //{
        //    decoVaseRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.1f, canvasRect.rect.height * 0.12f);
        //    decoVaseRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.09f, canvasRect.rect.height * 0.265f);
        //}
        //decoVaseRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.1f, canvasRect.rect.height * 0.12f);
        //decoVaseRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.09f, canvasRect.rect.height * 0.265f);

        //RectTransform decoGlasRectTransform = decoGlasContainer.GetComponent<RectTransform>();
        //if (decoGlasRectTransform != null && canvasRect != null)
        //{
        //    decoGlasRectTransform.anchoredPosition = new Vector2(canvasRect.rect.width * 0.21f, canvasRect.rect.height * 0.08f);
        //    decoGlasRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.064f, canvasRect.rect.height * 0.08f);
        //}

        RectTransform deskRectTransform = deskContainer.GetComponent<RectTransform>();
        if (deskRectTransform != null && canvasRect != null)
        {
            deskRectTransform.anchoredPosition = new Vector2(0, -canvasRect.rect.height * 0.05f);
            deskRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.35f, canvasRect.rect.height * 0.25f);
        }
        
        NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
        NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
    }

    public override void SetCharacter()
    {
        DestroyCharacter();
        character = Instantiate(characterPrefab, characterContainer.transform);
        RectTransform characterRectTransform = characterContainer.GetComponent<RectTransform>();
        characterRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.1f, 0);
        characterRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.25f, canvasRect.rect.height * 1f);
        characterController = character.GetComponent<CharacterController>();
    }

    public override bool HandleTouchEvent(float x, float y, AudioSource audioSource)
    {
        //RectTransform decoVaseRectTransform = decoVaseContainer.GetComponent<RectTransform>();
        //RectTransform decoGlasRectTransform = decoGlasContainer.GetComponent<RectTransform>();

        //Vector3[] cornersDecoVase = new Vector3[4];
        //decoVaseRectTransform.GetWorldCorners(cornersDecoVase);
        //Vector3 bottomLeftDecoVase = cornersDecoVase[0];
        //Vector3 topRightDecoVase = cornersDecoVase[2];

        //Vector3[] cornersDecoGlas = new Vector3[4];
        //decoGlasRectTransform.GetWorldCorners(cornersDecoGlas);
        //Vector3 bottomLeftDecoGlas = cornersDecoGlas[0];
        //Vector3 topRightDecoGlas = cornersDecoGlas[2];
        //if (x >= bottomLeftDecoVase.x && x <= topRightDecoVase.x &&
        //    y >= bottomLeftDecoVase.y && y <= topRightDecoVase.y)
        //{
        //    StartCoroutine(OnDecoVase(audioSource));
        //    return true;
        //} else if ( x >= bottomLeftDecoGlas.x && x <= topRightDecoGlas.x &&
        //            y >= bottomLeftDecoGlas.y && y <= topRightDecoGlas.y)
        //{
        //    StartCoroutine(OnDecoGlas(audioSource));
        //    return true;
        //}
        return false;
    }

    private IEnumerator OnDecoVase(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            if (audioSource.clip != null)
            {
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
            if (audioSource.clip != null)
            {
                
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
        if (desk != null)
        {
            Destroy(desk);
        }
        if (decoGlas != null)
        {
            Destroy(decoGlas);
        }
        if (decoVase != null)
        {
            Destroy(decoVase);
        }
    }

    private void InstantiateBackground()
    {
        background = Instantiate(backgroundPrefab, backgroundContainer.transform);
        desk = Instantiate(deskPrefab, deskContainer.transform);
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

    