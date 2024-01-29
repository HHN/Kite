using UnityEngine;

public abstract class PipelineJob : MonoBehaviour, OnSuccessHandler, OnErrorHandler
{
    [SerializeField] protected GPT_Pipeline pipeline;
    [SerializeField] protected PipelineJobState state;
    [SerializeField] protected string prompt;
    [SerializeField] protected GameObject gptServercallPrefab;
    [SerializeField] protected SceneController controller;
    [SerializeField] protected string jobName;
    [SerializeField] protected bool performJob;

    public PipelineJob()
    {
        state = PipelineJobState.READY_TO_START;
    }

    public void StartJob()
    {
        if (!performJob)
        {
            Debug.Log("Skip Job: " + jobName);
            state = PipelineJobState.SKIPPED;
            pipeline.OnJobFinished(this);
            return;
        }
        InitializePrompt();
        Debug.Log("Prompt: " + prompt);
        state = PipelineJobState.JOB_RUNNING;
        GetCompletionServerCall call = Instantiate(gptServercallPrefab).GetComponent<GetCompletionServerCall>();
        call.sceneController = controller;
        call.onSuccessHandler = this;
        call.onErrorHandler = this;
        call.prompt = prompt;
        call.SendRequest();
        DontDestroyOnLoad(call.gameObject);
    }

    public void TryAgain()
    {
        Debug.Log("Job failed, try again");
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
        state = HandleCompletion(response.completion);
        Debug.Log("Completion: " + response.completion);

        if (state == PipelineJobState.FAILED)
        {
            TryAgain();
        }
        else
        {
            pipeline.OnJobFinished(this);
        }
    }

    public abstract void InitializePrompt();

    public abstract PipelineJobState HandleCompletion(string completion);

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
