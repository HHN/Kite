using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Player;
using Assets._Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Controller.SceneControllers
{
    public class BookmarkedNovelsSceneController : SceneController
    {
        [SerializeField] private RectTransform visualNovelHolder;
        [SerializeField] private Button bankkreditNovel;
        [SerializeField] private Button investorNovel;
        [SerializeField] private Button bankkontoNovel;
        [SerializeField] private Button foerderAntragNovel;
        [SerializeField] private Button elternNovel;
        [SerializeField] private Button notarinNovel;
        [SerializeField] private Button presseNovel;
        [SerializeField] private Button bueroNovel;
        [SerializeField] private Button gruenderZuschussNovel;
        [SerializeField] private Button honorarNovel;
        [SerializeField] private Button lebenspartnerNovel;
        [SerializeField] private Button introNovel;

        [SerializeField] private GameObject selectNovelSoundPrefab;

        private Dictionary<VisualNovelNames, Button> _novelButtons;

        public void Start()
        {
            _novelButtons = new Dictionary<VisualNovelNames, Button>
            {
                { VisualNovelNames.BANK_KREDIT_NOVEL, bankkreditNovel },
                { VisualNovelNames.INVESTOR_NOVEL, investorNovel },
                { VisualNovelNames.ELTERN_NOVEL, elternNovel },
                { VisualNovelNames.NOTARIAT_NOVEL, notarinNovel },
                { VisualNovelNames.PRESSE_NOVEL, presseNovel },
                { VisualNovelNames.VERMIETER_NOVEL, bueroNovel },
                { VisualNovelNames.HONORAR_NOVEL, honorarNovel },
                { VisualNovelNames.EINSTIEGS_NOVEL, introNovel }
            };

            foreach (var novelButton in _novelButtons)
            {
                novelButton.Value.onClick.AddListener(() => OnNovelButton(novelButton.Key));
                novelButton.Value.gameObject.SetActive(false);
            }

            List<long> favoriteIds = FavoritesManager.Instance().GetFavoritesIds();
            int index = 0;

            foreach (long id in favoriteIds)
            {
                GameObject novel = GetNovelById(id);

                if (novel == null)
                {
                    continue;
                }

                Vector3 localPosition = GetLocalPositionByIndex(index);
                novel.transform.localPosition = localPosition;
                novel.gameObject.SetActive(true);

                index++;
            }


            SetVisualNovelHolderHeight(index);
        }

        private void SetVisualNovelHolderHeight(int index)
        {
            float height = index switch
            {
                12 => 1300,
                >= 10 => 1200,
                9 => 1050,
                >= 7 => 900,
                6 => 750,
                >= 4 => 600,
                3 => 450,
                _ => 300
            };

            visualNovelHolder.GetComponent<LayoutElement>().preferredHeight = height;
        }

        private static Vector3 GetLocalPositionByIndex(int index)
        {
            return index switch
            {
                0 => new Vector3(-250f, 0f, 0f),
                1 => new Vector3(250f, 0f, 0f),
                2 => new Vector3(0f, -144.5f, 0f),
                3 => new Vector3(-250f, -290f, 0f),
                4 => new Vector3(250f, -290f, 0f),
                5 => new Vector3(0f, -433.5f, 0f),
                6 => new Vector3(-250f, -578f, 0f),
                7 => new Vector3(250f, -578f, 0f),
                8 => new Vector3(0f, -722.5f, 0f),
                9 => new Vector3(-250f, -868f, 0f),
                10 => new Vector3(250f, -868f, 0f),
                11 => new Vector3(0f, -1011.5f, 0f),
                _ => Vector3.zero
            };
        }

        private GameObject GetNovelById(long id)
        {
            VisualNovelNames visualNovelName = VisualNovelNamesHelper.ValueOf((int)id);
            return _novelButtons.TryGetValue(visualNovelName, out var button) ? button.gameObject : null;
        }

        private void OnNovelButton(VisualNovelNames visualNovelName)
        {
            NovelColorManager.Instance().SetColor(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
            VisualNovel visualNovelToDisplay = null;
            List<VisualNovel> allNovels = KiteNovelManager.Instance().GetAllKiteNovels();

            foreach (VisualNovel novel in allNovels)
            {
                if (novel.id == VisualNovelNamesHelper.ToInt(visualNovelName))
                {
                    visualNovelToDisplay = novel;
                    break;
                }
            }

            if (visualNovelToDisplay == null)
            {
                DisplayErrorMessage("Die gewünschte Novel konnte nicht geladen werden.");
                return;
            }

            PlayManager.Instance().SetVisualNovelToPlay(visualNovelToDisplay);
            PlayManager.Instance().SetForegroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetForegroundColorOfNovel(visualNovelName));
            PlayManager.Instance().SetBackgroundColorOfVisualNovelToPlay(FoundersBubbleMetaInformation.GetBackgroundColorOfNovel(visualNovelName));
            PlayManager.Instance().SetDisplayNameOfNovelToPlay(FoundersBubbleMetaInformation.GetDisplayNameOfNovelToPlay(visualNovelName));
            GameObject buttonSound = Instantiate(selectNovelSoundPrefab);
            DontDestroyOnLoad(buttonSound);

            if (ShowPlayInstructionManager.Instance().ShowInstruction())
            {
                SceneLoader.LoadPlayInstructionScene();
            }
            else
            {
                SceneLoader.LoadPlayNovelScene();
            }
        }
    }
}