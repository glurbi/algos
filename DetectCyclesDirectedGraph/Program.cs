using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DetectCyclesDirectedGraph
{
    class Program
    {
        class DirectedGraph
        {
            private Dictionary<int, List<int>> adjacencies = new Dictionary<int, List<int>>();

            public DirectedGraph(ICollection<(int, int)> edges)
            {
                foreach (var (u, v) in edges)
                {
                    if (!adjacencies.TryGetValue(u, out var listU))
                    {
                        listU = new List<int>();
                        adjacencies[u] = listU;
                    }

                    listU.Add(v);
                }
            }
            public bool HasCycle()
            {
                foreach (var u in adjacencies.Keys)
                {
                    var visited = new HashSet<int> { };

                    if (HasCycle(visited, u))
                        return true;
                }

                return false;

                bool HasCycle(HashSet<int> visited, int u)
                {
                    if (visited.Contains(u))
                        return true;

                    visited.Add(u);

                    foreach (var v in adjacencies.TryGetValue(u, out var l) ? l : Enumerable.Empty<int>())
                    {
                        if (HasCycle(visited, v))
                            return true;
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
                        (0, 1),
                        (1, 2),
                        (0, 2),
                        (2, 0),
                        (2, 3),
                        (3, 3),
                    };
            Debug.Assert(new DirectedGraph(edges).HasCycle()); 

            edges = new List<(int, int)>()
                    {
                        (0, 1),
                        (1, 2),
                        (0, 2),
                        (2, 3),
                    };
            Debug.Assert(!new DirectedGraph(edges).HasCycle());

            Console.ReadKey();
        }

    }
}

