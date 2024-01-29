using System.Collections.Generic;
using UnityEngine;

public abstract class GPT_Pipeline : MonoBehaviour
{
    [SerializeField] protected Dictionary<string, string> memory;
    [SerializeField] protected Queue<PipelineJob> jobs;
    [SerializeField] protected PipelineState state;
    [SerializeField] protected SceneController controller;
    [SerializeField] protected int numberOfJobs;
    [SerializeField] protected int numberOfCompletedJobs;

    public GPT_Pipeline() 
    { 
        memory = new Dictionary<string, string>();
        jobs = new Queue<PipelineJob>();
        state = PipelineState.READY_TO_START;
        InitializePipelineJobs();
    }

    public abstract void InitializePipelineJobs();

    public void StartPipeline()
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
            return;
        }
        if (jobs.Count == 0)
        {
            state = PipelineState.COMPLETED;
            OnSuccess();
            return;
        }
        OnProgress();
        PipelineJob nextJob = jobs.Dequeue();
        nextJob.StartJob();

    }

    public abstract void OnSuccess();

    public abstract void OnFailure();

    public abstract void OnProgress();

    public PipelineState GetState()
    {
        return state;
    }

    public Dictionary<string, string> GetMemory()
    {
        return memory;
    }
}
