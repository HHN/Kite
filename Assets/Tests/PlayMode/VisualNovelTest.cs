using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.Managers;
using Assets._Scripts.Novel;
using Assets._Scripts.Novel.VisualNovelFormatter;
using Assets._Scripts.SceneManagement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class VisualNovelTest
    {
        [UnityTest]
        public IEnumerator TestNovels()
        {
            SceneLoader.LoadMainMenuScene();

            yield return WaitForNovels();

            List<NovelTester> tests = NovelTester.TestNovels(KiteNovelManager.Instance().GetAllKiteNovels());

            foreach (NovelTester test in tests)
            {
                while (test.IsTestOver() == false)
                {
                    yield return null;
                }

                Assert.IsTrue(test.IsTestSuccessful());
            }
        }

        [UnityTest]
        public IEnumerator ConvertNovelsFromTweeToJson()
        {
            yield return RunConverter(nr => nr.ConvertNovelsFromTweeToJSON());
        }

        [UnityTest]
        public IEnumerator ConvertNovelsFromTweeToJsonAndSelectiveOverrideOldNovels()
        {
            yield return RunConverter(nr => nr.ConvertNovelsFromTweeToJSONAndSelectiveOverrideOldNovels());
        }

        private IEnumerator LoadScene(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (asyncLoad != null && !asyncLoad.isDone)
            {
                yield return null;
            }
        }

        private IEnumerator WaitForNovels(float timeout = 5f)
        {
            float startTime = Time.time;
            
            List<VisualNovel> novels = KiteNovelManager.Instance().GetAllKiteNovels();
            while (novels == null || novels.Count == 0)
            {
                if (Time.time - startTime > timeout)
                {
                    Assert.Fail("Time out! Loading novels did not work.");
                }

                yield return null;
            }
        }

        private IEnumerator RunConverter(System.Action<NovelReader> convertMethod)
        {
            yield return LoadScene(SceneNames.MainMenuScene);

            GameObject converter = GameObject.Find("TweeToJsonConverter");
            Assert.NotNull(converter);

            NovelReader novelReader = converter.GetComponent<NovelReader>();
            convertMethod(novelReader);

            while (!novelReader.IsFinished())
            {
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator TestKeyWords()
        {
            Debug.Log("TEST!");

            // Erstelle ein neues GameObject und f�ge das KeywordTester-Skript hinzu.
            GameObject testerObj = new GameObject("KeywordTesterObject");
            KeywordTester tester = testerObj.AddComponent<KeywordTester>();
            // Optional: Passe den Dateipfad an, falls n�tig.
            tester.filePath = "_novels_twee/Eltern/visual_novel_event_list.txt";

            // Warte eine gewisse Zeit, damit die Coroutine im KeywordTester laufen kann.
            // Dies ist ein einfaches Beispiel; ggf. musst du hier an deine Testlogik anpassen.
            yield return new WaitForSeconds(2f);

            // Hier k�nntest du weitere Checks einbauen, z.B. anhand interner Tester-Daten.
            Debug.Log("Keyword testing completed.");

            // Zerst�re das Tester-Objekt, um saubere Verh�ltnisse zu schaffen.
            Object.Destroy(testerObj);

            yield return null;
        }
    }
}