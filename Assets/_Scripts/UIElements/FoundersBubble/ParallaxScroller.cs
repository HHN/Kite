using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.FoundersBubble
{
    public class ParallaxScroller : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        
        [SerializeField] private RectTransform[] backgroundItemList;
        [SerializeField] private RectTransform[] midgroundItemList;

        [Header("Parallax-Layer")] 
        [SerializeField] private RectTransform backgroundParent;
        [SerializeField] private RectTransform midgroundParent;
        [SerializeField] private RectTransform foreground;

        [Range(0f, 1f)] [SerializeField] private float backgroundSpeed = 0.3f;
        [Range(0f, 1f)] [SerializeField] private float midgroundSpeed = 0.6f;

        private float _width;

        private Vector2 _bgStartPos;
        private Vector2 _mgStartPos;
        
        private HorizontalLayoutGroup _bgLayoutGroup;
        private HorizontalLayoutGroup _mgLayoutGroup;

        private void Start()
        {
            _bgLayoutGroup = backgroundParent.GetComponent<HorizontalLayoutGroup>();
            _mgLayoutGroup = midgroundParent.GetComponent<HorizontalLayoutGroup>();

            if (!_bgLayoutGroup || !_mgLayoutGroup)
            {
                Debug.LogError("HorizontalLayoutGroup missing on backgroundParent or midgroundParent! Please add it in the Inspector.");
                return;
            }
            
            for (int i = 0; i < backgroundItemList.Length; i++)
            {
                Instantiate(backgroundItemList[i], backgroundParent);
            }
            
            for (int i = 0; i < backgroundItemList.Length; i++)
            {
                int itemIndex = backgroundItemList.Length - 1 - i;
                RectTransform rt = Instantiate(backgroundItemList[itemIndex], backgroundParent);
                rt.SetAsFirstSibling();
            }
            
            for (int i = 0; i < midgroundItemList.Length; i++)
            {
                Instantiate(midgroundItemList[i], midgroundParent);
            }
            
            for (int i = 0; i < midgroundItemList.Length; i++)
            {
                int itemIndex = midgroundItemList.Length - 1 - i;
                RectTransform rt = Instantiate(midgroundItemList[itemIndex], midgroundParent);
                rt.SetAsFirstSibling();
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundParent);
            LayoutRebuilder.ForceRebuildLayoutImmediate(midgroundParent);
            
            _width = scrollRect.content.rect.width - scrollRect.viewport.rect.width;

            scrollRect.onValueChanged.AddListener(OnScroll);

            StartCoroutine(SetInitialScrollDelayed());
        }

        private IEnumerator SetInitialScrollDelayed()
        {
            yield return null;
            scrollRect.horizontalNormalizedPosition = 0.5f;
            OnScroll(new Vector2(scrollRect.horizontalNormalizedPosition, 0));
        }

        private void OnScroll(Vector2 scrollPos)
        {
            float scrollX = scrollPos.x;
            float baseScroll = scrollX * _width;

            backgroundParent.anchoredPosition = new Vector2(_bgStartPos.x - baseScroll * backgroundSpeed, _bgStartPos.y);
            midgroundParent.anchoredPosition = new Vector2(_mgStartPos.x - baseScroll * midgroundSpeed, _mgStartPos.y);
        }
    }
}