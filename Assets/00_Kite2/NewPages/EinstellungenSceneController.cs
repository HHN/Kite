using UnityEngine;
using UnityEngine.UI;

public class EinstellungenSceneController : SceneController
{
    [SerializeField] private Button barrierefreiheitButton;
    [SerializeField] private Button impressumButton;
    [SerializeField] private Button nutzungsbedingungenButton;
    [SerializeField] private Button datenschutzButton;

    public void Start()
    {
        barrierefreiheitButton.onClick.AddListener(delegate { OnBarrierefreiheitButton(); });
        impressumButton.onClick.AddListener(delegate { OnImpressumButton(); });
        nutzungsbedingungenButton.onClick.AddListener(delegate { OnNutzungsbedingungenButton(); });
        datenschutzButton.onClick.AddListener(delegate { OnDatenschutzButton(); });
    }

    public void OnBarrierefreiheitButton()
    {
        SceneLoader.LoadBarrierefreiheitScene();
    }

    public void OnImpressumButton()
    {
        SceneLoader.LoadImpressumScene();
    }

    public void OnNutzungsbedingungenButton()
    {
        SceneLoader.LoadNutzungsbedingungenScene();
    }

    public void OnDatenschutzButton()
    {
        SceneLoader.LoadDatenschutzScene();
    }
}
