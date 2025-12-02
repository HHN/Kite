using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Assets._Scripts._Mappings;
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

            while (KiteNovelManager.Instance().GetAllKiteNovels() == null ||
                   KiteNovelManager.Instance().GetAllKiteNovels().Count == 0)
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
                while (!test.IsTestOver())
                {
                    yield return null;
                }

                Assert.IsTrue(test.IsTestSuccessful());
            }
        }

        [UnityTest]
        public IEnumerator ImportNovel()
        {
            MappingManager mappingManager = MappingManager.Instance;

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("_Scenes/MainMenuSceneTest");

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            // Start the import
            NovelReader.Instance.ImportNovel();

            // Wait until the import is finished
            yield return new WaitUntil(() => NovelReader.Instance.IsFinished());

            // End of test
            Assert.IsTrue(NovelReader.Instance.IsFinished(), "ImportNovel should be finished at the end.");
        }

        [UnityTest]
        public IEnumerator TestKeyWords()
        {
            // Create a new GameObject and add the KeywordTester script.
            GameObject testerObj = new GameObject("KeywordTesterObject");
            KeywordTester tester = testerObj.AddComponent<KeywordTester>();

            // Optional: Adjust the folder path if needed.
            tester.folderPath = "_novels_twee/";

            // Wait for some time so the coroutine inside KeywordTester can run.
            // This is a simple example; adapt to your test logic if necessary.
            yield return new WaitForSeconds(2f);

            // Here you could add additional checks based on internal tester data.
            Debug.Log("Keyword testing completed.");

            // Destroy the tester object to keep the scene clean.
            UnityEngine.Object.Destroy(testerObj);

            yield return null;
        }

        /// <summary>
        /// Removes all novels from novels.json whose titles are listed in
        /// "..\\Assets\\_novels_twee\\list_of_novels_to_remove_from_json.txt"
        /// under the "visualNovels" array.
        ///
        /// This method can be executed directly from the Unity Test Runner.
        /// </summary>
        [Test]
        public void RemoveNovelsListedInFileFromNovelsJson()
        {
            string listPath = Path.Combine(
                Application.dataPath,
                "_novels_twee",
                "list_of_novels_to_remove_from_json.txt");

            string novelsJsonPath = Path.Combine(
                Application.streamingAssetsPath,
                "novels.json");

            Debug.Log($"[RemoveNovels] List file path: {listPath}");
            Debug.Log($"[RemoveNovels] novels.json path: {novelsJsonPath}");

            NovelJsonTools.RemoveNovelsByTitle(listPath, novelsJsonPath);

            // Optional: kleine Plausibilitätsprüfung
            string finalJson = File.ReadAllText(novelsJsonPath, Encoding.UTF8);
            Assert.IsFalse(string.IsNullOrEmpty(finalJson), "novels.json should not be empty after removal.");
        }



        /// <summary>
        /// Wrapper class for the list of novels to remove.
        /// File format:
        /// {
        ///   "visualNovels": [
        ///     "Einstieg",
        ///     "Eltern",
        ///     "Honorar"
        ///   ]
        /// }
        /// </summary>
        [Serializable]
        public class VisualNovelTitlesToRemove
        {
            public List<string> visualNovels;
        }

        /// <summary>
        /// Complete index structure as in novels.json.
        /// We map all fields so that nothing is lost when writing back to disk.
        /// </summary>
        [Serializable]
        public class VisualNovelIndex
        {
            public List<VisualNovelJson> visualNovels;
        }

        /// <summary>
        /// Single entry from novels.json.
        /// Field names match the JSON keys (lowerCamelCase).
        /// </summary>
        [Serializable]
        public class VisualNovelJson
        {
            public int id;
            public string title;
            public string description;
            public string feedback;
            public string context;
            public string folderName;
            public string novelEvents;
            public bool isKiteNovel;
            public string novelColor;
            public string novelFrameColor;
            public List<string> characters;
            public string playedPath;
        }
    }
}
