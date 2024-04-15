using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

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

            Assert.IsTrue(test.IsTestSuccessfull());
        }
    }

    [UnityTest]
    public IEnumerator ConvertNovelsFromTweeToJson()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneNames.MAIN_MENU_SCENE);

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
}
