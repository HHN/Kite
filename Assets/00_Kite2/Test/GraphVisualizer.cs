using UnityEngine;
using System.Collections.Generic;

public class GraphVisualizer : MonoBehaviour
{
    public Dictionary<string, List<string>> Graph = new Dictionary<string, List<string>>();
    public Vector3 nodeStartPosition = new Vector3(0, 0, 0);
    public float nodeSpacing = 2.0f;

    private Dictionary<string, Vector3> nodePositions = new Dictionary<string, Vector3>();

    public void SetGraph(Dictionary<string, List<string>> graph)
    {
        Graph = graph;
        CalculateNodePositions();
    }

    private void CalculateNodePositions()
    {
        int i = 0;
        foreach (var node in Graph.Keys)
        {
            nodePositions[node] = nodeStartPosition + new Vector3(i * nodeSpacing, 0, 0);
            i++;
        }
    }

    private void OnDrawGizmos()
    {
        if (Graph == null || Graph.Count == 0) return;

        Gizmos.color = Color.blue;

        // Zeichne die Knoten
        foreach (var node in nodePositions)
        {
            Gizmos.DrawSphere(node.Value, 0.2f);
            UnityEditor.Handles.Label(node.Value, node.Key);
        }

        // Zeichne die Verbindungen
        Gizmos.color = Color.red;
        foreach (var node in Graph)
        {
            if (!nodePositions.ContainsKey(node.Key)) continue;

            Vector3 startPosition = nodePositions[node.Key];
            foreach (var neighbor in node.Value)
            {
                if (nodePositions.ContainsKey(neighbor))
                {
                    Vector3 endPosition = nodePositions[neighbor];
                    Gizmos.DrawLine(startPosition, endPosition);
                }
            }
        }
    }
}