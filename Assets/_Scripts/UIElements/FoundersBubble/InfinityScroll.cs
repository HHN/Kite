using Assets._Scripts.Managers;
using Assets._Scripts.Player;
using Assets._Scripts.SceneControllers;
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
        [SerializeField] private RectTransform[] itemList;
        [SerializeField] private Vector2 oldVelocity;
        [SerializeField] private bool isUpdated;
        [SerializeField] private float maxSpeed = 1000f;
        [SerializeField] private InfinityScroll secondScrollRect;
        [SerializeField] private InfinityScroll middleLayer;
        [SerializeField] private FoundersBubbleSceneController foundersBubbleSceneController;

        [SerializeField] private Vector2 lastPosition;
        [SerializeField] private bool stoppedDraggingManually;

        [SerializeField] public float widthBefore;
        [SerializeField] public float widthAfter;

        [SerializeField] public bool isSnapped;
        [SerializeField] public bool isTextboxVisible;
        [SerializeField] public float snappingSpeed;
        [SerializeField] public float snappingForce;
        [SerializeField] public int currentTarget;

        [SerializeField] public int itemsToAdd;

        [SerializeField] private Vector2 lastDragPosition;
        [SerializeField] private Vector2 velocityDuringDrag;
        [SerializeField] private float lastDragTime;

        private void Start()
        {
            lastDragPosition = Vector2.zero;
            snappingForce = 100f;
            isSnapped = true;
            snappingSpeed = 0f;
            isUpdated = false;
            oldVelocity = Vector2.zero;
            itemsToAdd = Mathf.CeilToInt(viewPortTransform.rect.width /
                                         (itemList[0].rect.width + horizontalLayoutGroup.spacing));

            for (int i = 0; i < itemsToAdd; i++)
            {
                RectTransform rt = Instantiate(itemList[i % itemList.Length], contentPanelTransform);
                rt.SetAsLastSibling();
            }

            for (int i = 0; i < itemsToAdd; i++)
            {
                int num = itemList.Length - i - 1;

                while (num < 0)
                {
                    num += itemList.Length;
                }

                RectTransform rt = Instantiate(itemList[num], contentPanelTransform);
                rt.SetAsFirstSibling();
            }

            contentPanelTransform.localPosition = new Vector3(
                0 - (itemList[0].rect.width + horizontalLayoutGroup.spacing) * itemsToAdd,
                contentPanelTransform.localPosition.y,
                contentPanelTransform.localPosition.z);

            widthBefore = itemList[0].rect.width * itemList.Length + (itemList.Length - 1) * horizontalLayoutGroup.spacing - viewPortTransform.rect.width;
            widthAfter = itemList[0].rect.width * (itemList.Length + 2 * itemsToAdd) + (itemList.Length + 2 * itemsToAdd - 1) * horizontalLayoutGroup.spacing - viewPortTransform.rect.width;

            float scrollPosition = SceneMemoryManager.Instance().GetMemoryOfFoundersBubbleScene();

            if (scrollRect != null)
            {
                scrollRect.horizontalNormalizedPosition = scrollPosition;
            }
            else if (customScrollRect != null)
            {
                customScrollRect.horizontalNormalizedPosition = scrollPosition;
            }
        }

        private void Update()
        {
            if (scrollRect == null)
            {
                UpdateForCustomScrollRect();
                return;
            }

            UpdateForScrollRect();
        }

        private void UpdateForScrollRect()
        {
            if (secondScrollRect == null)
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
                    contentPanelTransform.localPosition -=
                        new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                    isUpdated = true;
                }
                else if (contentPanelTransform.localPosition.x <
                         0 - itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing))
                {
                    Canvas.ForceUpdateCanvases();
                    oldVelocity = scrollRect.velocity;
                    contentPanelTransform.localPosition +=
                        new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
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
                contentPanelTransform.localPosition -=
                    new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                isUpdated = true;
                currentTarget = currentTarget + FoundersBubbleMetaInformation.NumberOfNovelsToDisplay;
            }
            else if (contentPanelTransform.localPosition.x <
                     0 - itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing))
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = scrollRect.velocity;
                contentPanelTransform.localPosition +=
                    new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                isUpdated = true;
                currentTarget = currentTarget - FoundersBubbleMetaInformation.NumberOfNovelsToDisplay;
            }

            SnapToItem();
            Canvas.ForceUpdateCanvases();
        }

        private void UpdateForCustomScrollRect()
        {
            if (secondScrollRect == null)
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
                    contentPanelTransform.localPosition -=
                        new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                    isUpdated = true;
                }
                else if (contentPanelTransform.localPosition.x <
                         0 - itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing))
                {
                    Canvas.ForceUpdateCanvases();
                    oldVelocity = customScrollRect.velocity;
                    contentPanelTransform.localPosition +=
                        new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
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
                contentPanelTransform.localPosition -=
                    new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                isUpdated = true;
                currentTarget = currentTarget + FoundersBubbleMetaInformation.NumberOfNovelsToDisplay;
            }
            else if (contentPanelTransform.localPosition.x <
                     0 - itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing))
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = customScrollRect.velocity;
                contentPanelTransform.localPosition +=
                    new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                isUpdated = true;
                currentTarget = currentTarget - FoundersBubbleMetaInformation.NumberOfNovelsToDisplay;
            }

            SnapToItem();
            Canvas.ForceUpdateCanvases();
        }

        private void SnapToItem()
        {
            float targetXPosition = 0 - (currentTarget * (itemList[0].rect.width + horizontalLayoutGroup.spacing) -
                                         viewPortTransform.rect.width / 2 - itemList[0].rect.width / 2 -
                                         horizontalLayoutGroup.spacing);

            if (isSnapped)
            {
                if (isTextboxVisible && contentPanelTransform.localPosition.x != targetXPosition)
                {
                    foundersBubbleSceneController.MakeTextboxInvisible();
                    isTextboxVisible = false;
                }

                return;
            }

            if (scrollRect != null)
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

            if (IsCurrentlyInFirstHalf() && currentTarget > FoundersBubbleMetaInformation.NumberOfNovelsToDisplay)
            {
                currentTarget = currentTarget - FoundersBubbleMetaInformation.NumberOfNovelsToDisplay;
            }
        }

        public void CalculateAndSetCurrentPositionForSecondScrollView(Vector2 positionChange)
        {
            if (secondScrollRect == null)
            {
                return;
            }

            if ((contentPanelTransform.localPosition.x >= 0 ||
                 contentPanelTransform.localPosition.x <
                 0 - itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing)) &&
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
            return contentPanelTransform.localPosition.x >
                   0 - itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing) / 2;
        }

        public float GetCurrentScrollPosition()
        {
            if (scrollRect != null)
            {
                return scrollRect.horizontalNormalizedPosition;
            }

            return customScrollRect.horizontalNormalizedPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            lastDragPosition = eventData.position;
            lastDragTime = Time.time;
            velocityDuringDrag = Vector2.zero;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 newPosition = eventData.position;
            float newTime = Time.time;

            Vector2 deltaPosition = newPosition - lastDragPosition;
            float deltaTime = newTime - lastDragTime;

            if (deltaTime > 0)
            {
                velocityDuringDrag = deltaPosition / deltaTime;
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
            if (customScrollRect == null)
            {
                return Vector2.zero;
            }

            return customScrollRect.velocity + velocityDuringDrag;
        }
    }
}