using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchButtonsController : MonoBehaviour
{
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI indexInfo;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image image;
    [SerializeField] private long index;

    void Start()
    {
        leftButton.onClick.AddListener(delegate { OnLeftButton(); });
        rightButton.onClick.AddListener(delegate { OnRightButton(); });
        SetIndex(0);
    }

    public void Select(long index)
    {
        if (index < 0 || index >= sprites.Length)
        {
            return;
        }
        image.sprite = sprites[index];
        this.index = index;

        if (index < 9)
        {
            this.indexInfo.text = "0" + (index + 1) + " / " + (sprites.Length);
        }
        else
        {
            this.indexInfo.text = (index + 1) + " / " + (sprites.Length);
        }
    }

    public void OnLeftButton()
    {
        if (index > 0)
        {
            Select(index - 1);
        }
        else
        {
            Select(sprites.Length - 1);
        }
    }

    public void OnRightButton()
    {
        if (index < (sprites.Length - 1))
        {
            Select(index + 1);
        }
        else
        {
            Select(0);
        }
    }

    public long GetIndex()
    {
        return index;
    }

    public void SetIndex(long index)
    {
        Select(index);
    }
}
