using UnityEngine;

public class PromptsAndCompletionsExplorerSceneController : SceneController, OnSuccessHandler
{
    [SerializeField] private GameObject getDataServerCallPrefab;
    [SerializeField] private GameObject dataObjectPrefab;
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject noDataObjectsHint;

    void Start()
    {
        BackStackManager.Instance().Push(SceneNames.PROMPTS_AND_COMPLETIONS_EXPLORER_SCENE);

        GetDataServerCall call = Instantiate(getDataServerCallPrefab).GetComponent<GetDataServerCall>();
        call.sceneController = this;
        call.onSuccessHandler = this;
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
