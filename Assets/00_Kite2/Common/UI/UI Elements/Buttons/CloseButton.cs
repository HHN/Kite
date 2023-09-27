using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public Button button;
    public GameObject gameObjectToHide;

    private void Start()
    {
        button.onClick.AddListener(delegate { OnClick(); });
    }

    public void OnClick()
    {
        if (gameObjectToHide == null)
        {
            return;
        }
        gameObjectToHide.SetActive(false);
    }
}
