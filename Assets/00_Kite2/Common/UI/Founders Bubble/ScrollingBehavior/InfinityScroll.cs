using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfinityScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private ScrollRect scollRect;
    [SerializeField] private CustomScrollRect customScollRect;
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
    [SerializeField] private bool stoppedDragingManually;

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

    void Start()
    {
        lastDragPosition = Vector2.zero;
        snappingForce = 100f;
        isSnapped = true;
        snappingSpeed = 0f;
        isUpdated = false;
        oldVelocity = Vector2.zero;
        itemsToAdd = Mathf.CeilToInt(viewPortTransform.rect.width / (itemList[0].rect.width + horizontalLayoutGroup.spacing));

        for (int i = 0; i < itemsToAdd; i++)
        {
            RectTransform RT = Instantiate(itemList[i % itemList.Length], contentPanelTransform);
            RT.SetAsLastSibling();
        }
        for (int i = 0; i < itemsToAdd; i++)
        {
            int num = itemList.Length - i - 1;

            while (num < 0)
            {
                num += itemList.Length;
            }
            RectTransform RT = Instantiate(itemList[num], contentPanelTransform);
            RT.SetAsFirstSibling();
        }
        contentPanelTransform.localPosition = new Vector3(0 - (itemList[0].rect.width + horizontalLayoutGroup.spacing) * itemsToAdd,
            contentPanelTransform.localPosition.y,
            contentPanelTransform.localPosition.z);

        widthBefore = (itemList[0].rect.width * itemList.Length) + ((itemList.Length - 1) * horizontalLayoutGroup.spacing) - (viewPortTransform.rect.width);
        widthAfter = (itemList[0].rect.width * (itemList.Length + (2 * itemsToAdd))) + ((itemList.Length + (2 * itemsToAdd) - 1) * horizontalLayoutGroup.spacing) - (viewPortTransform.rect.width);

        FoundersBubbleSceneMemory memory = SceneMemoryManager.Instance().GetMemoryOfFoundersBubbleScene();

        if (memory != null && scollRect != null)
        {
            scollRect.horizontalNormalizedPosition = (float)memory.scrollPosition;
        }
        else if (memory != null && customScollRect != null)
        {
            customScollRect.horizontalNormalizedPosition = (float)memory.scrollPosition;
        }
    }

    void Update()
    {
        if (scollRect == null)
        {
            UpdateForCustomScrollRect();
            return;
        }
        UpdateForScrollRect();
    }

    public void UpdateForScrollRect()
    {
        if (secondScrollRect == null)
        {
            if (isUpdated)
            {
                isUpdated = false;
                scollRect.velocity = oldVelocity;
            }
            if (contentPanelTransform.localPosition.x >= 0)
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = scollRect.velocity;
                contentPanelTransform.localPosition -= new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                isUpdated = true;
            }
            else if (contentPanelTransform.localPosition.x < 0 - (itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing)))
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = scollRect.velocity;
                contentPanelTransform.localPosition += new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                isUpdated = true;
            }
            return;
        }
        if (scollRect.velocity.magnitude > maxSpeed)
        {
            scollRect.velocity = scollRect.velocity.normalized * maxSpeed;
        }
        if (isUpdated)
        {
            isUpdated = false;
            scollRect.velocity = oldVelocity;
        }
        if (contentPanelTransform.localPosition.x >= 0)
        {
            Canvas.ForceUpdateCanvases();
            oldVelocity = scollRect.velocity;
            contentPanelTransform.localPosition -= new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
            isUpdated = true;
            currentTarget = currentTarget + FoundersBubbleMetaInformation.numerOfNovelsToDisplay;
        }
        else if (contentPanelTransform.localPosition.x < 0 - (itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing)))
        {
            Canvas.ForceUpdateCanvases();
            oldVelocity = scollRect.velocity;
            contentPanelTransform.localPosition += new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
            isUpdated = true;
            currentTarget = currentTarget - FoundersBubbleMetaInformation.numerOfNovelsToDisplay;
        }
        SnapToItem();
        Canvas.ForceUpdateCanvases();
    }

    public void UpdateForCustomScrollRect()
    {
        if (secondScrollRect == null)
        {
            if (isUpdated)
            {
                isUpdated = false;
                customScollRect.velocity = oldVelocity;
            }
            if (contentPanelTransform.localPosition.x >= 0)
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = customScollRect.velocity;
                contentPanelTransform.localPosition -= new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                isUpdated = true;
            }
            else if (contentPanelTransform.localPosition.x < 0 - (itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing)))
            {
                Canvas.ForceUpdateCanvases();
                oldVelocity = customScollRect.velocity;
                contentPanelTransform.localPosition += new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
                isUpdated = true;
            }
            return;
        }
        if (customScollRect.velocity.magnitude > maxSpeed)
        {
            customScollRect.velocity = customScollRect.velocity.normalized * maxSpeed;
        }
        if (isUpdated)
        {
            isUpdated = false;
            customScollRect.velocity = oldVelocity;
        }
        if (contentPanelTransform.localPosition.x >= 0)
        {
            Canvas.ForceUpdateCanvases();
            oldVelocity = customScollRect.velocity;
            contentPanelTransform.localPosition -= new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
            isUpdated = true;
            currentTarget = currentTarget + FoundersBubbleMetaInformation.numerOfNovelsToDisplay;
        }
        else if (contentPanelTransform.localPosition.x < 0 - (itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing)))
        {
            Canvas.ForceUpdateCanvases();
            oldVelocity = customScollRect.velocity;
            contentPanelTransform.localPosition += new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
            isUpdated = true;
            currentTarget = currentTarget - FoundersBubbleMetaInformation.numerOfNovelsToDisplay;
        }
        SnapToItem();
        Canvas.ForceUpdateCanvases();
    }

    public void SnapToItem()
    {
        float targetXPosition = 0 - ((currentTarget * (itemList[0].rect.width + horizontalLayoutGroup.spacing)) - (viewPortTransform.rect.width / 2) - (itemList[0].rect.width / 2) - horizontalLayoutGroup.spacing);

        if (isSnapped)
        {
            if (isTextboxVisible && contentPanelTransform.localPosition.x != targetXPosition)
            {
                foundersBubbleSceneController.MakeTextboxInvisible();
                isTextboxVisible = false;
            }
            return;
        }

        if (scollRect != null)
        {
            scollRect.velocity = Vector2.zero;
        } else
        {
            customScollRect.velocity = Vector2.zero;
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

        if (IsCurrentlyInFirstHalf() && currentTarget > FoundersBubbleMetaInformation.numerOfNovelsToDisplay)
        {
            currentTarget = currentTarget - FoundersBubbleMetaInformation.numerOfNovelsToDisplay;
        }
    }

    public void CalculateAndSetCurrentPositionForSecondScrollView(Vector2 positionChange)
    {
        if (secondScrollRect == null)
        {
            return;
        }
        if ((contentPanelTransform.localPosition.x >= 0 ||
            contentPanelTransform.localPosition.x < 0 - (itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing))) &&
            customScollRect.m_Dragging)
        {
            customScollRect.OnEndDrag(new PointerEventData(null) { button = 0 }) ;
            stoppedDragingManually = true;
            return;
        }
        if (stoppedDragingManually)
        {
            customScollRect.OnBeginDrag(customScollRect.lastDragBegin);
            stoppedDragingManually = false;
        }
        Vector2 secondScrollViewChange = positionChange * 0.45f;
        Vector2 middleLayerChange = positionChange * 0.725f;

        secondScrollRect.scollRect.content.anchoredPosition += secondScrollViewChange;
        middleLayer.scollRect.content.anchoredPosition += middleLayerChange;
    }


    public bool IsCurrentlyInFirstHalf()
    {
        return (contentPanelTransform.localPosition.x > (0 - (itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing))/2));
    }

    public float GetCurrentScrollPosition() 
    {
        if (scollRect != null)
        {
            return scollRect.horizontalNormalizedPosition;
        }
        return customScollRect.horizontalNormalizedPosition;

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
        if (customScollRect == null) { return Vector2.zero; }
        return customScollRect.velocity + velocityDuringDrag;
    }
}
