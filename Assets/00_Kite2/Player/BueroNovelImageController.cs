using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BueroNovelImageController : MonoBehaviour
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

    public void SetVisualElements(RectTransform canvasRect)
    {
        Debug.Log(""+canvasRect.rect.width + "  " + canvasRect.rect.height);
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
        decoBackgroundRectTransform.anchoredPosition = new Vector2(-canvasRect.rect.width * 0.35f, canvasRect.rect.height * 0.25f);

        characterRectTransform.sizeDelta = new Vector2(canvasRect.rect.width * 0.25f, canvasRect.rect.height * 1f);
        decoDeskRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.075f, canvasRect.rect.height * 0.1f);
        decoBackgroundRectTransform.sizeDelta = new Vector2(canvasRect.rect.height * 0.17f, canvasRect.rect.height * 0.25f);

        NovelColorManager.Instance().SetCanvasHeight(canvasRect.rect.height);
        NovelColorManager.Instance().SetCanvasWidth(canvasRect.rect.width);
    }
}