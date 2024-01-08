using UnityEngine;

public abstract class PipelineJob : MonoBehaviour, OnSuccessHandler, OnErrorHandler
{
    [SerializeField] private GPT_Pipeline pipeline;
    [SerializeField] private PipelineJobState state;
    [SerializeField] private string prompt;
    [SerializeField] private GameObject gptServercallPrefab;
    [SerializeField] private SceneController controller;

    public PipelineJob()
    {
        state = PipelineJobState.READY_TO_START;
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartJob()
    {
        state = PipelineJobState.JOB_RUNNING;
        GetCompletionServerCall call = Instantiate(gptServercallPrefab).GetComponent<GetCompletionServerCall>();
        call.sceneController = controller;
        call.onSuccessHandler = this;
        call.onErrorHandler = this;
        call.prompt = prompt;
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);
    }

    public void OnSuccess(Response response)
    {
        state = HandleCompletion();
        pipeline.OnJobFinished(this);
    }

    public abstract void InitializePrompt();

    public abstract PipelineJobState HandleCompletion();

    public PipelineJobState GetState()
    {
        return state;
    }

    public void OnError(Response response)
    {
        state = PipelineJobState.FAILED;
        pipeline.OnJobFinished(this);
    }
}
