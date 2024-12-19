using _00_Kite2.Common;
using _00_Kite2.Common.Managers;
using _00_Kite2.Common.SceneManagement;
using _00_Kite2.Server_Communication;
using _00_Kite2.Server_Communication.Server_Calls;
using UnityEngine;

namespace _00_Kite2.UserFeedback
{
    public class PromptsAndCompletionsExplorerSceneController : SceneController, IOnSuccessHandler
    {
        [SerializeField] private GameObject getDataServerCallPrefab;
        [SerializeField] private GameObject dataObjectPrefab;
        [SerializeField] private GameObject container;
        [SerializeField] private GameObject noDataObjectsHint;

        private void Start()
        {
            BackStackManager.Instance().Push(SceneNames.PROMPTS_AND_COMPLETIONS_EXPLORER_SCENE);

            GetDataServerCall call = Instantiate(getDataServerCallPrefab).GetComponent<GetDataServerCall>();
            call.sceneController = this;
            call.OnSuccessHandler = this;
            call.SendRequest();
        }

        public void OnSuccess(Response response)
        {
            if (response?.GetDataObjects() == null || response?.GetDataObjects().Count == 0)
            {
                noDataObjectsHint.SetActive(true);
                return;
            }
            noDataObjectsHint.SetActive(false);

            foreach (DataObject dataObject in response.GetDataObjects())
            {
                DataObjectGuiElement dataObjectGuiElement =
                    Instantiate(dataObjectPrefab, container.transform)
                        .GetComponent<DataObjectGuiElement>();

                dataObjectGuiElement.InitializeDataObject(dataObject);
            }
        }
    }
}
