using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HasCycle
{
    class Program
    {
        class UndirectedGraph
        {
            private Dictionary<int, List<int>> adjacencies = new Dictionary<int, List<int>>();

            public UndirectedGraph(ICollection<(int,int)> edges)
            {
                foreach (var (u,v) in edges)
                {
                    AddEdge(u, v);
                    AddEdge(v, u);
                }
            }

            public void AddEdge(int u, int v)
            {
                if (!adjacencies.TryGetValue(u, out var listU))
                {
                    listU = new List<int>();
                    adjacencies[u] = listU;
                }

                listU.Add(v);
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
            List<(int, int)> edges;

            edges = new List<(int, int)>()
            {
                (1, 0),
                (1, 2),
                (2, 0),
                (0, 3),
                (3, 4),
            };

            Debug.Assert(new UndirectedGraph(edges).HasCycle());

            edges = new List<(int, int)>()
            {
                (1, 0),
                (1, 2),
                (0, 3),
                (3, 4),
            };

            Debug.Assert(!new UndirectedGraph(edges).HasCycle());

            Console.ReadKey();
        }
    }
}
