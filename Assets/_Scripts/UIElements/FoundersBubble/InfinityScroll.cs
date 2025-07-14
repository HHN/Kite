using System;
using System.Collections.Generic;
using Assets._Scripts.Controller.SceneControllers;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Assets._Scripts.UI_Elements.Founders_Bubble;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._Scripts.UIElements.FoundersBubble
{
    public class InfinityScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private CustomScrollRect customScrollRect;
        [SerializeField] private RectTransform viewPortTransform;
        [SerializeField] private RectTransform contentPanelTransform;
        [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;

        [SerializeField] private RectTransform[] itemList; // ToDo: Create these items in the FoundersBubbleSceneController

        [SerializeField] private Vector2 oldVelocity;
        [SerializeField] private bool isUpdated;
        [SerializeField] private float maxSpeed = 1000f;
        [SerializeField] private InfinityScroll secondScrollRect;
        [SerializeField] private InfinityScroll middleLayer;
        [SerializeField] private FoundersBubbleSceneController foundersBubbleSceneController;

        [SerializeField] private Vector2 lastPosition;
        [SerializeField] private bool stoppedDraggingManually;

        [SerializeField] public bool isSnapped;
        [SerializeField] public bool isTextboxVisible;
        [SerializeField] public float snappingSpeed;
        [SerializeField] public float snappingForce;
        [SerializeField] public int currentTarget;

        [SerializeField] public int itemsToAdd;

        [SerializeField] private Vector2 lastDragPosition;
        [SerializeField] private Vector2 velocityDuringDrag;
        [SerializeField] private float lastDragTime;

        [SerializeField] private float yOffsetForZigzag = 200f;

        public List<RectTransform> novelButtonsList;
        public RectTransform[] novelButtons;

        private bool _instantiated;
        private int _numberOfNovels;

        private void Start()
        {
            lastDragPosition = Vector2.zero;
            snappingForce = 100f;
            isSnapped = true;
            snappingSpeed = 0f;
            isUpdated = false;
            oldVelocity = Vector2.zero;
            
            if (customScrollRect != null && customScrollRect.gameObject.name == "Novels")
            {
                novelButtons = new RectTransform[novelButtonsList.Count];
                for (int i = 0; i < novelButtonsList.Count; i++)
                {
                    novelButtons[i] = novelButtonsList[i];
                }
                
                _numberOfNovels = novelButtons.Length;
                
                itemsToAdd = 0;
            }
            else
            {
                novelButtons = itemList;
                
                // itemsToAdd = Mathf.CeilToInt(viewPortTransform.rect.width / (novelButtons[0].rect.width + horizontalLayoutGroup.spacing));
            }

            if (customScrollRect == null || customScrollRect.gameObject.name != "Novels")
            {
                for (int i = 0; i < itemsToAdd; i++)
                {
                    RectTransform rt = Instantiate(novelButtons[i % novelButtons.Length], contentPanelTransform);
                    rt.SetAsLastSibling();
                }
            
                for (int i = 0; i < itemsToAdd; i++)
                {
                    int num = novelButtons.Length - i - 1;
            
                    while (num < 0)
                    {
                        num += novelButtons.Length;
                    }
            
                    RectTransform rt = Instantiate(novelButtons[num], contentPanelTransform);
                    rt.SetAsFirstSibling();
                }
            }

            contentPanelTransform.localPosition = new Vector3(0 - (novelButtons[0].rect.width + horizontalLayoutGroup.spacing) * itemsToAdd,
                contentPanelTransform.localPosition.y, contentPanelTransform.localPosition.z);

            float memory = SceneMemoryManager.Instance().GetMemoryOfFoundersBubbleScene();

            if (memory != 0 && scrollRect)
            {
                scrollRect.horizontalNormalizedPosition = memory;
            }
            else if (memory != 0 && customScrollRect)
            {
                customScrollRect.horizontalNormalizedPosition = memory;
            }

            _instantiated = true;
        }

        private void Update()
        {
            if (!_instantiated) return;
            
            if (!scrollRect)
            {
                UpdateForCustomScrollRect();
                return;
            }

            UpdateForScrollRect();
        }

        private void UpdateForScrollRect()
        {
            if (!secondScrollRect)
            {
                if (isUpdated)
                {
                    isUpdated = false;
                    scrollRect.velocity = oldVelocity;
                }

                if (contentPanelTransform.localPosition.x >= 0)
                {
                    Canvas.ForceUpdateCanvases();
                    oldVelocity = scrollRect.velocity;
                    contentPanelTransform.localPosition -= new Vector3(novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                    isUpdated = true;
                }
                else if (contentPanelTransform.localPosition.x < 0 - (novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing)))
                {
                    Canvas.ForceUpdateCanvases();
                    oldVelocity = scrollRect.velocity;
                    contentPanelTransform.localPosition += new Vector3(novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                    isUpdated = true;
                }

                return;
            }

            if (scrollRect.velocity.magnitude > maxSpeed)
            {
                scrollRect.velocity = scrollRect.velocity.normalized * maxSpeed;
            }

            if (isUpdated)
            {
                isUpdated = false;
                scrollRect.velocity = oldVelocity;
            }

            if (contentPanelTransform.localPosition.x >= 0)
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = scrollRect.velocity;
                contentPanelTransform.localPosition -= new Vector3(novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                isUpdated = true;
                currentTarget += _numberOfNovels;
            }
            else if (contentPanelTransform.localPosition.x < 0 - (novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing)))
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = scrollRect.velocity;
                contentPanelTransform.localPosition += new Vector3(novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                isUpdated = true;
                currentTarget -= _numberOfNovels;
            }

            SnapToItem();
            Canvas.ForceUpdateCanvases();
        }

        private void UpdateForCustomScrollRect()
        {
            if (!secondScrollRect)
            {
                if (isUpdated)
                {
                    isUpdated = false;
                    customScrollRect.velocity = oldVelocity;
                }

                if (contentPanelTransform.localPosition.x >= 0)
                {
                    Canvas.ForceUpdateCanvases();
                    oldVelocity = customScrollRect.velocity;
                    contentPanelTransform.localPosition -= new Vector3(novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                    isUpdated = true;
                }
                else if (contentPanelTransform.localPosition.x < 0 - (novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing)))
                {
                    Canvas.ForceUpdateCanvases();
                    oldVelocity = customScrollRect.velocity;
                    contentPanelTransform.localPosition += new Vector3(novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                    isUpdated = true;
                }

                return;
            }

            if (customScrollRect.velocity.magnitude > maxSpeed)
            {
                customScrollRect.velocity = customScrollRect.velocity.normalized * maxSpeed;
            }

            if (isUpdated)
            {
                isUpdated = false;
                customScrollRect.velocity = oldVelocity;
            }

            if (contentPanelTransform.localPosition.x >= 0)
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = customScrollRect.velocity;
                contentPanelTransform.localPosition -= new Vector3(novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                isUpdated = true;
                currentTarget += _numberOfNovels;
            }
            else if (contentPanelTransform.localPosition.x <
                     0 - (novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing)))
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = customScrollRect.velocity;
                contentPanelTransform.localPosition += new Vector3(novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                isUpdated = true;
                currentTarget -= _numberOfNovels;
            }

            SnapToItem();
            Canvas.ForceUpdateCanvases();
        }

        private void SnapToItem()
        {
            float targetXPosition = 0 - (currentTarget * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing) -
                                         viewPortTransform.rect.width / 2 - novelButtons[0].rect.width / 2 -
                                         horizontalLayoutGroup.spacing);

            if (isSnapped)
            {
                if (!isTextboxVisible || contentPanelTransform.localPosition.x == targetXPosition) return;
                
                foundersBubbleSceneController.MakeTextboxInvisible();
                isTextboxVisible = false;

                return;
            }

            if (scrollRect)
            {
                scrollRect.velocity = Vector2.zero;
            }
            else
            {
                customScrollRect.velocity = Vector2.zero;
            }

            snappingSpeed += snappingForce * Time.deltaTime;
            contentPanelTransform.localPosition = new Vector3(
                Mathf.MoveTowards(contentPanelTransform.localPosition.x, targetXPosition, snappingSpeed),
                contentPanelTransform.localPosition.y,
                contentPanelTransform.localPosition.z);

            if (contentPanelTransform.localPosition.x == targetXPosition)
            {
                isSnapped = true;
                isTextboxVisible = true;
            }
        }

        public void MoveToVisualNovel(VisualNovelNames visualNovelNames)
        {
            isSnapped = false;
            snappingSpeed = 0;
            currentTarget = FoundersBubbleMetaInformation.GetIndexOfNovel(visualNovelNames) + itemsToAdd;

            if (IsCurrentlyInFirstHalf() && currentTarget > _numberOfNovels)
            {
                currentTarget -= _numberOfNovels;
            }
        }

        public void CalculateAndSetCurrentPositionForSecondScrollView(Vector2 positionChange)
        {
            if (!secondScrollRect)
            {
                return;
            }

            if ((contentPanelTransform.localPosition.x >= 0 ||
                 contentPanelTransform.localPosition.x < 0 - novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing)) &&
                customScrollRect.m_Dragging)
            {
                customScrollRect.OnEndDrag(new PointerEventData(null) { button = 0 });
                stoppedDraggingManually = true;
                return;
            }

            if (stoppedDraggingManually)
            {
                customScrollRect.OnBeginDrag(customScrollRect.LastDragBegin);
                stoppedDraggingManually = false;
            }

            Vector2 secondScrollViewChange = positionChange * 0.45f;
            Vector2 middleLayerChange = positionChange * 0.725f;

            secondScrollRect.scrollRect.content.anchoredPosition += secondScrollViewChange;
            middleLayer.scrollRect.content.anchoredPosition += middleLayerChange;
        }


        private bool IsCurrentlyInFirstHalf()
        {
            return (contentPanelTransform.localPosition.x > 0 - novelButtons.Length * (novelButtons[0].rect.width + horizontalLayoutGroup.spacing) / 2);
        }

        public float GetCurrentScrollPosition()
        {
            return scrollRect ? scrollRect.horizontalNormalizedPosition : customScrollRect.horizontalNormalizedPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            lastDragPosition = eventData.position;
            lastDragTime = Time.time;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 newPosition = eventData.position;
            float newTime = Time.time;

            float deltaTime = newTime - lastDragTime;

            if (deltaTime > 0)
            {
            }

            lastDragPosition = newPosition;
            lastDragTime = newTime;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            velocityDuringDrag = Vector2.zero;
        }

        public Vector2 GetCurrentVelocity()
        {
            if (!customScrollRect)
            {
                return Vector2.zero;
            }

            return customScrollRect.velocity + velocityDuringDrag;
        }
    }
}