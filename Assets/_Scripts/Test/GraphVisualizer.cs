#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Test
{
    public class GraphVisualizer : MonoBehaviour
    {
        private Dictionary<string, List<string>> _graph = new Dictionary<string, List<string>>();
        public Vector3 nodeStartPosition = new Vector3(0, 0, 0);
        public float nodeSpacing = 2.0f;

        private readonly Dictionary<string, Vector3> _nodePositions = new Dictionary<string, Vector3>();

        public void SetGraph(Dictionary<string, List<string>> graph)
        {
            _graph = graph;
            CalculateNodePositions();
        }

        private void CalculateNodePositions()
        {
            int i = 0;
            foreach (var node in _graph.Keys)
            {
                _nodePositions[node] = nodeStartPosition + new Vector3(i * nodeSpacing, 0, 0);
                i++;
            }
        }

        private void OnDrawGizmos()
        {
            if (_graph == null || _graph.Count == 0) return;

            Gizmos.color = Color.blue;

            // Zeichne die Knoten
            foreach (var node in _nodePositions)
            {
                Gizmos.DrawSphere(node.Value, 0.2f);
                UnityEditor.Handles.Label(node.Value, node.Key);
            }

            // Zeichne die Verbindungen
            Gizmos.color = Color.red;
            foreach (var node in _graph)
            {
                if (!_nodePositions.ContainsKey(node.Key)) continue;

                Vector3 startPosition = _nodePositions[node.Key];
                foreach (var neighbor in node.Value)
                {
                    if (_nodePositions.ContainsKey(neighbor))
                    {
                        Vector3 endPosition = _nodePositions[neighbor];
                        Gizmos.DrawLine(startPosition, endPosition);
                    }
                }
            }
        }
    }
}
#endif