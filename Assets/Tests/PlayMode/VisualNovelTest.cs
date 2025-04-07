using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.Managers;
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
            float startTime = Time.time;
            float timeout = 5f;

            SceneLoader.LoadMainMenuScene();

            while (KiteNovelManager.Instance().GetAllKiteNovels() == null || KiteNovelManager.Instance().GetAllKiteNovels().Count == 0)
            {
                if (Time.time - startTime > timeout)
                {
                    Assert.Fail("Time out! Loading novels did not work.");
                }
                yield return null;
            }
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
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneNames.MainMenuScene);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            GameObject converter = GameObject.Find("TweeToJsonConverter");

            Assert.NotNull(converter);

            NovelReader novelReader = converter.GetComponent<NovelReader>();
            novelReader.ConvertNovelsFromTweeToJSON();

            while (novelReader.IsFinished() == false)
            {
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator ConvertNovelsFromTweeToJsonAndSelectiveOverrideOldNovels()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneNames.MainMenuScene);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            GameObject converter = GameObject.Find("TweeToJsonConverter");

            Assert.NotNull(converter);

            NovelReader novelReader = converter.GetComponent<NovelReader>();
            novelReader.ConvertNovelsFromTweeToJSONAndSelectiveOverrideOldNovels();

            while (novelReader.IsFinished() == false)
            {
                yield return null;
            }
        }
    }
}
