using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DetectCycleUndirectedGraph
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
                    var visited = new HashSet<int> { u };

                    return HasCycle(visited, u, -1);
                }

                return false;

                bool HasCycle(HashSet<int> visited, int u, int previous)
                {
                    foreach (var v in adjacencies[u])
                    {
                        if (visited.Add(v))
                        {
                            if (HasCycle(visited, v, u))
                            {
                                return true;
                            }
                        }
                        else if (v != previous)
                        {
                            return true;
                        }
                    }
                    visited.Remove(u);

                    return false;
                }
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
