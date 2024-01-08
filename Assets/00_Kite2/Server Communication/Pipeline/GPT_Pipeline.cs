using System.Collections.Generic;
using UnityEngine;

public abstract class GPT_Pipeline : MonoBehaviour
{
    [SerializeField] private Dictionary<string, string> memory;
    [SerializeField] private Queue<PipelineJob> jobs;
    [SerializeField] private PipelineState state;

    public GPT_Pipeline() 
    { 
        memory = new Dictionary<string, string>();
        jobs = new Queue<PipelineJob>();
        state = PipelineState.READY_TO_START;
        InitializePipelineJobs();
        DontDestroyOnLoad(this.gameObject);
    }

    public abstract void InitializePipelineJobs();

    public void Start()
    {
        state = PipelineState.RUNNING;

        if (jobs.Count == 0)
        {
            state = PipelineState.COMPLETED;
            OnSuccess();
            Destroy(this.gameObject);
            return;
        }
        PipelineJob job = jobs.Dequeue();
        job.StartJob();
    }

    public void OnJobFinished(PipelineJob lastJob)
    {
        if (lastJob.GetState() == PipelineJobState.FAILED)
        {
            state = PipelineState.FAILED;
            OnFailure();
            Destroy(lastJob.gameObject);
            Destroy(this.gameObject);
            return;
        }
        if (jobs.Count == 0)
        {
            state = PipelineState.COMPLETED;
            OnSuccess();
            Destroy(lastJob.gameObject);
            Destroy(this.gameObject);
            return;
        }
        OnProgress();
        PipelineJob nextJob = jobs.Dequeue();
        nextJob.StartJob();
        Destroy(lastJob.gameObject);

    }

    public abstract void OnSuccess();

    public abstract void OnFailure();

    public abstract void OnProgress();

    public PipelineState GetState()
    {
        return state;
    }
}
