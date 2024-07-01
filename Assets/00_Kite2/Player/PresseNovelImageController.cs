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
        decoVaseRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.1f, canvasRect.rect.height * 0.08f);
        decoGlasRectTransform.anchoredPosition = new Vector2(canvasRect.rect.width * 0.25f, canvasRect.rect.height * 0.08f);
        deskRectTransform.anchoredPosition = new Vector2(0, -canvasRect.rect.height * 0.05f);

        characterRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.25f, canvasRect.rect.height * 1f);
        decoVaseRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.09f, canvasRect.rect.height * 0.265f);
        decoGlasRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.064f, canvasRect.rect.height * 0.08f);
        deskRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.35f, canvasRect.rect.height * 0.25f);

        NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
        NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
    }

    
}

    