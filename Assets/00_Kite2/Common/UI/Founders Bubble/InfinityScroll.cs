using UnityEngine;
using UnityEngine.UI;

public class InfinityScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect scollRect;
    [SerializeField] private RectTransform viewPortTransform;
    [SerializeField] private RectTransform contentPanelTransform;
    [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
    [SerializeField] private RectTransform[] itemList;
    [SerializeField] private Vector2 oldVelocity;
    [SerializeField] private bool isUpdated;
    [SerializeField] private float maxSpeed = 1000f;
    [SerializeField] private InfinityScroll secondScrollRect;

    [SerializeField] public float widthBefore;
    [SerializeField] public float widthAfter;

    void Start()
    {
        isUpdated = false;
        oldVelocity = Vector2.zero;
        int ItemsToAdd = Mathf.CeilToInt(viewPortTransform.rect.width / (itemList[0].rect.width + horizontalLayoutGroup.spacing));

        for (int i = 0; i < ItemsToAdd; i++)
        {
            RectTransform RT = Instantiate(itemList[i % itemList.Length], contentPanelTransform);
            RT.SetAsLastSibling();
        }
        for (int i = 0; i < ItemsToAdd; i++)
        {
            int num = itemList.Length - i - 1;

            while (num < 0)
            {
                num += itemList.Length;
            }
            RectTransform RT = Instantiate(itemList[num], contentPanelTransform);
            RT.SetAsFirstSibling();
        }
        contentPanelTransform.localPosition = new Vector3(0 - (itemList[0].rect.width + horizontalLayoutGroup.spacing) * ItemsToAdd,
            contentPanelTransform.localPosition.y,
            contentPanelTransform.localPosition.z);

        widthBefore = (itemList[0].rect.width * itemList.Length) + ((itemList.Length - 1) * horizontalLayoutGroup.spacing) - (viewPortTransform.rect.width);
        widthAfter = (itemList[0].rect.width * (itemList.Length + (2 * ItemsToAdd))) + ((itemList.Length + (2 * ItemsToAdd) - 1) * horizontalLayoutGroup.spacing) - (viewPortTransform.rect.width);
    }

    void Update()
    {
        if (secondScrollRect == null)
        {
            return;
        }
        CalculateAndSetCurrentPositionForSecondScrollView();

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
            return;
        }
        if (contentPanelTransform.localPosition.x < 0 - (itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing)))
        {
            Canvas.ForceUpdateCanvases();
            oldVelocity = scollRect.velocity;
            contentPanelTransform.localPosition += new Vector3(itemList.Length * (itemList[0].rect.width + horizontalLayoutGroup.spacing), 0, 0);
            isUpdated = true;
            return;
        }
    }

    public void CalculateAndSetCurrentPositionForSecondScrollView()
    {
        if (secondScrollRect == null)
        {
            return;
        }
        float widthFromLeftAfter = scollRect.horizontalNormalizedPosition * widthAfter;
        float widthFromleftBefore = widthFromLeftAfter - ((widthAfter - widthBefore) / 2);
        float currentPointOnFirstViewBefore = widthFromleftBefore / (widthBefore + viewPortTransform.rect.width + horizontalLayoutGroup.spacing);
        float widthFromLeftBeforeOnSecondScrollView = currentPointOnFirstViewBefore * (secondScrollRect.widthBefore + secondScrollRect.viewPortTransform.rect.width + secondScrollRect.horizontalLayoutGroup.spacing);
        float widthFromLeftOnSecondScrollViewAfter = widthFromLeftBeforeOnSecondScrollView + ((secondScrollRect.widthAfter - secondScrollRect.widthBefore) / 2);
        float result = widthFromLeftOnSecondScrollViewAfter / secondScrollRect.widthAfter;

        if (result < 0)
        {
            currentPointOnFirstViewBefore = 1 + currentPointOnFirstViewBefore;
            widthFromLeftBeforeOnSecondScrollView = currentPointOnFirstViewBefore * (secondScrollRect.widthBefore + secondScrollRect.viewPortTransform.rect.width + secondScrollRect.horizontalLayoutGroup.spacing);
            widthFromLeftOnSecondScrollViewAfter = widthFromLeftBeforeOnSecondScrollView + ((secondScrollRect.widthAfter - secondScrollRect.widthBefore) / 2);
            result = widthFromLeftOnSecondScrollViewAfter / secondScrollRect.widthAfter;
        }
        secondScrollRect.scollRect.horizontalNormalizedPosition = result;    }

    /**
    public void CalculateAndSetCurrentPositionForSecondScrollView(bool print)
    {
        if (secondScrollRect == null)
        {
            return;
        }
        float widthFromLeftAfter = scollRect.horizontalNormalizedPosition * widthAfter;
        float widthFromleftBefore = widthFromLeftAfter - ((widthAfter - widthBefore) / 2);
        float currentPointOnFirstViewBefore = widthFromleftBefore / (widthBefore + viewPortTransform.rect.width + horizontalLayoutGroup.spacing);
        float widthFromLeftBeforeOnSecondScrollView = currentPointOnFirstViewBefore * (secondScrollRect.widthBefore + secondScrollRect.viewPortTransform.rect.width + secondScrollRect.horizontalLayoutGroup.spacing);
        float widthFromLeftOnSecondScrollViewAfter = widthFromLeftBeforeOnSecondScrollView + ((secondScrollRect.widthAfter - secondScrollRect.widthBefore) / 2);
        float result = widthFromLeftOnSecondScrollViewAfter / secondScrollRect.widthAfter;

        if (result < 0)
        {
            currentPointOnFirstViewBefore = 1 + currentPointOnFirstViewBefore;
            widthFromLeftBeforeOnSecondScrollView = currentPointOnFirstViewBefore * secondScrollRect.widthBefore;
            widthFromLeftOnSecondScrollViewAfter = widthFromLeftBeforeOnSecondScrollView + ((secondScrollRect.widthAfter - secondScrollRect.widthBefore) / 2);
            result = (widthFromLeftOnSecondScrollViewAfter + viewPortTransform.rect.width) / secondScrollRect.widthAfter;
        }
        secondScrollRect.scollRect.horizontalNormalizedPosition = result;

        if (print) { Debug.Log(widthFromLeftBeforeOnSecondScrollView); }
    }
    */
}
