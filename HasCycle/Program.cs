using System;
using System.Collections.Generic;

namespace HasCycle
{
    class Program
    {
        class UndirectedGraph
        {
            private Dictionary<int, List<int>> adjacencies = new Dictionary<int, List<int>>();

            public void AddEdge(int u, int v)
            {
                if (!adjacencies.TryGetValue(u, out var listU))
                {
                    listU = new List<int>();
                    adjacencies[u] = listU;
                }

                listU.Add(v);

                if (!adjacencies.TryGetValue(v, out var listV))
                {
                    listV = new List<int>();
                    adjacencies[v] = listV;
                }

                listV.Add(u);
            }
            public bool HasCycle()
            {
                foreach (var u in adjacencies.Keys)
                {
                    var visitedEdges = new HashSet<(int, int)>();
                    var visitedNodes = new HashSet<int>();

                    return HasCycle(visitedEdges, visitedNodes, u);
                }

                return false;

                bool HasCycle(HashSet<(int, int)> visitedEdges, HashSet<int> visitedNodes, int u)
                {
                    if (!visitedNodes.Add(u))
                        return true;

                    foreach (var v in adjacencies[u])
                    {
                        var edge = MakeEdge(u, v);
                        if (visitedEdges.Add(edge))
                        {
                            if (HasCycle(visitedEdges, visitedNodes, v))
                            {
                                return true;
                            }
                            else
                            {
                                visitedNodes.Remove(v);
                                visitedEdges.Remove(edge);
                            }
                        }
                    }

                    return false;
                }

                (int a, int b) MakeEdge(int u, int v) => u < v ? (u, v) : (v, u);
            }
        }


        static void Main(string[] args)
        {
            var graph = new UndirectedGraph();
            graph.AddEdge(1, 0);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);
            graph.AddEdge(0, 3);
            graph.AddEdge(3, 4);

            Console.WriteLine(graph.HasCycle());
        }
    }
}
