using UnityEngine;
using UnityEngine.UI;

public class FoundersWell2SceneController : SceneController
{
    [SerializeField] private Button ressourcenButton;
    [SerializeField] private Button barrierefreiheitButton;
    [SerializeField] private Button nutzungsbedingungenButton;
    [SerializeField] private Button datenschutzButton;
    [SerializeField] private Button impressumButton;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.FOUNDERS_WELL_2_SCENE);

        ressourcenButton.onClick.AddListener(delegate { OnRessourcenButton(); });
        barrierefreiheitButton.onClick.AddListener(delegate { OnBarrierefreiheitButton(); });
        nutzungsbedingungenButton.onClick.AddListener(delegate { OnNutzungsbedingungenButton(); });
        datenschutzButton.onClick.AddListener(delegate { OnDatenschutzButton(); });
        impressumButton.onClick.AddListener(delegate { OnImpressumButton(); });
    }

    public void OnRessourcenButton()
    {
        SceneLoader.LoadRessourcenScene();
    }

    public void OnBarrierefreiheitButton()
    {
        SceneLoader.LoadBarrierefreiheitScene();
    }

    public void OnNutzungsbedingungenButton()
    {
        SceneLoader.LoadNutzungsbedingungenScene();
    }

    public void OnDatenschutzButton()
    {
        SceneLoader.LoadDatenschutzScene();
    }

    public void OnImpressumButton()
    {
        SceneLoader.LoadImpressumScene();
    }
}
