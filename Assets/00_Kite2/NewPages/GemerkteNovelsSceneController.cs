using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemerkteNovelsSceneController : SceneController
{
    [SerializeField] private Button bankkreditNovel;
    [SerializeField] private Button bekannteTreffenNovel;
    [SerializeField] private Button bankkontoNovel;
    [SerializeField] private Button foerderantragNovel;
    [SerializeField] private Button elternNovel;
    [SerializeField] private Button notarinNovel;
    [SerializeField] private Button presseNovel;
    [SerializeField] private Button bueroNovel;
    [SerializeField] private Button gruenderzuschussNovel;
    [SerializeField] private Button honorarNovel;
    [SerializeField] private Button lebenspartnerNovel;
    [SerializeField] private Button introNovel;

    public void Start()
    {
        bankkreditNovel.onClick.AddListener(delegate { OnBankkreditNovelButton(); });
        bekannteTreffenNovel.onClick.AddListener(delegate { OnBekannteTreffenNovelButton(); });
        bankkontoNovel.onClick.AddListener(delegate { OnBankKontoNovelButton(); });
        foerderantragNovel.onClick.AddListener(delegate { OnFoerderantragNovelButton(); });
        elternNovel.onClick.AddListener(delegate { OnElternNovelButton(); });
        notarinNovel.onClick.AddListener(delegate { OnNotariatNovelButton(); });
        presseNovel.onClick.AddListener(delegate { OnPresseNovelButton(); });
        bueroNovel.onClick.AddListener(delegate { OnBueroNovelButton(); });
        gruenderzuschussNovel.onClick.AddListener(delegate { OnGruenderzuschussNovelButton(); });
        honorarNovel.onClick.AddListener(delegate { OnHonorarNovelButton(); });
        lebenspartnerNovel.onClick.AddListener(delegate { OnLebenspartnerNovelButton(); });
        introNovel.onClick.AddListener(delegate { OnIntroNovelButton(); });
    }

    public void OnBankkreditNovelButton()
    {
        Debug.Log("OnBankkreditNovelButton");
    }

    public void OnBekannteTreffenNovelButton()
    {
        Debug.Log("OnBekannteTreffenNovelButton");
    }

    public void OnBankKontoNovelButton()
    {
        Debug.Log("OnBankKontoNovelButton");
    }

    public void OnFoerderantragNovelButton()
    {
        Debug.Log("OnFoerderantragNovelButton");
    }

    public void OnElternNovelButton()
    {
        Debug.Log("OnElternNovelButton");
    }

    public void OnNotariatNovelButton()
    {
        Debug.Log("OnNotariatNovelButton");
    }

    public void OnPresseNovelButton()
    {
        Debug.Log("OnPresseNovelButton");
    }

    public void OnBueroNovelButton()
    {
        Debug.Log("OnBueroNovelButton");
    }

    public void OnGruenderzuschussNovelButton()
    {
        Debug.Log("OnGruenderzuschussNovelButton");
    }

    public void OnHonorarNovelButton()
    {
        Debug.Log("OnHonorarNovelButton");
    }

    public void OnLebenspartnerNovelButton()
    {
        Debug.Log("OnLebenspartnerNovelButton");
    }

    public void OnIntroNovelButton()
    {
        Debug.Log("OnIntroNovelButton");
    }
}
