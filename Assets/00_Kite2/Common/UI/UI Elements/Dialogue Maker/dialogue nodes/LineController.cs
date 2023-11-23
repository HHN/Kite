using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform[] points;

    [SerializeField] private int fromNodeId;
    [SerializeField] private int toNodeId;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetUpLine(Transform[] points, int from, int to)
    {
        lineRenderer.positionCount = points.Length;
        this.points = points;
        fromNodeId = from;
        toNodeId = to;
    }

    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(points[i].position.x, points[i].position.y, 5));
        }
    }
}
