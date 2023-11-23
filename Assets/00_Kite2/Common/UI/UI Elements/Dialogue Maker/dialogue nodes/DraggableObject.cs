using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isDragging = false;
    private Vector3 offset;
    private NovelMakerSceneController sceneController;
    [SerializeField] GameObject ObjectToDrag;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Image image;

    void Start()
    {
        sceneController = GameObject.Find("Controller").GetComponent<NovelMakerSceneController>();
    }

    void Update()
    {
        if (isDragging)
        {
            ObjectToDrag.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    public void SetSizeOfBoxCollider()
    {
        RectTransform rectTransform = image.rectTransform;
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        Vector3 sizeAsVector3 = new Vector3(width, height, 0);
        boxCollider.size = sizeAsVector3;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        offset = ObjectToDrag.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        sceneController.DeactivateScrollView();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        sceneController.ActivateScrollView();
    }
}
