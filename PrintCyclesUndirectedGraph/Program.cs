using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PrintCycleUndirectedGraph
{
    class Program
    {
        class UndirectedGraph
        {
            private Dictionary<int, List<int>> adjacencies = new Dictionary<int, List<int>>();

            public UndirectedGraph(ICollection<(int, int)> edges)
            {
                foreach (var (u, v) in edges)
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

            public HashSet<SortedSet<int>> GetCycles()
            {
                var cycles = new HashSet<SortedSet<int>>();

                foreach (var u in adjacencies.Keys)
                {
                    var visited = new HashSet<int>();

                    SearchCycles(visited, u);
                }

                return cycles;

                void SearchCycles(HashSet<int> visited, int u)
                {
                    if (visited.Add(u))
                    {
                        foreach (var v in adjacencies[u])
                        {
                            if (v != u)
                            {
                                SearchCycles(visited, v);
                            }
                        }
                        visited.Remove(u);
                    }
                    else
                    {
                        Console.WriteLine("[" + string.Join(',', visited) + "]");
                    }
                }
            }

            public void PrintCycles()
            {
                var cycles = GetCycles();
                foreach (var cycle in cycles)
                {
                    Console.WriteLine("[" + string.Join(',', cycle) + "]");
                }
            }
        }


        static void Main(string[] args)
        {
            List<(int, int)> edges;

            edges = new List<(int, int)>()
            {
                (1, 2),
                (2, 3),
                (3, 4),
                (4, 6),
                (4, 7),
                (5, 6),
                (3, 5),
                (7, 8),
                (6, 10),
                (5, 9),
                (10, 11),
                (11, 12),
                (11, 13),
                (12, 13),
            };
            new UndirectedGraph(edges).PrintCycles();

            Console.ReadKey();
        }
    }
}
