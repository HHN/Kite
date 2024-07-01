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
    [SerializeField] private GameObject decoDeskPrefab;
    [SerializeField] private GameObject decoDeskContainer;
    [SerializeField] private GameObject decoBackgroundPrefab;
    [SerializeField] private GameObject decoBackgroundContainer;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject characterContainer;

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
        RectTransform deskRectTransform = deskContainer.GetComponent<RectTransform>();
            
        characterRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.1f, 0);
        decoDeskRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.1f, canvasRect.rect.height * 0.08f);
        decoBackgroundRectTransform.anchoredPosition = new Vector2(canvasRect.rect.width * 0.25f, canvasRect.rect.height * 0.08f);
        deskRectTransform.anchoredPosition = new Vector2(0, -canvasRect.rect.height * 0.05f);

        characterRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.25f, canvasRect.rect.height * 1f);
        decoDeskRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.09f, canvasRect.rect.height * 0.265f);
        decoBackgroundRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.064f, canvasRect.rect.height * 0.08f);
        deskRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.35f, canvasRect.rect.height * 0.25f);

        NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
        NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
    }

    
}

    