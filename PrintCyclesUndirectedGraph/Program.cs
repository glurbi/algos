using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

            public HashSet<List<int>> GetCycles()
            {
                var cycles = new HashSet<List<int>>(new CycleComparer());

                foreach (var u in adjacencies.Keys)
                {
                    var visited = new HashSet<int> { u };

                    SearchCycles(visited, u, -1, u);
                }

                return cycles;

                void SearchCycles(HashSet<int> visited, int u, int previous, int start)
                {
                    foreach (var v in adjacencies[u])
                    {
                        if (visited.Add(v))
                        {
                            SearchCycles(visited, v, u, start);
                        }
                        else if (v != previous && v == start)
                        {
                            var cycle = new List<int>(visited);
                            cycle.Sort();
                            cycles.Add(cycle);
                        }
                    }
                    visited.Remove(u);
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

        class CycleComparer : IEqualityComparer<List<int>>
        {
            public bool Equals(List<int> x, List<int> y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(List<int> l)
            {
                return l.Aggregate(1, (agg, curr) => agg * 31 + curr);
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
