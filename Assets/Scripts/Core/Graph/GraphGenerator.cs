using System.Collections.Generic;

/**********************************************************
 * Author: Ryan Tan
 * A graph generator that generates a random graph with a
 * specified number of vertices and edges. The graph is
 * guaranteed to be connected and have no circuits
 * with the help of a UnionFind data structure
 **********************************************************/

namespace Algowizardry.Core.GraphTheory
{
    public static class GraphGenerator
    {
        private static UnionFind unionFindHelper;

        public static Graph GenerateGraph(int numVertices, int numEdges, ref int accumulatedCost)
        {
            Graph g = new Graph
            {
                vertices = new List<Node>(),
                edges = new List<Edge>()
            };

            if (unionFindHelper == null) {
                unionFindHelper = new UnionFind(g.vertices);
            } else {
                unionFindHelper.Initialize(g.vertices);
            }

            // Add vertices
            for (int i = 0; i < numVertices; i++)
            {
                Node n = new Node(i);
                g.vertices.Add(n);
            }

            // Add random edges with random costs >= 1
            // Ensure that the graph is connected and
            // no circuits are formed
            for (int i = 0; i < numEdges; i++)
            {
                Node start = g.vertices[UnityEngine.Random.Range(0, numVertices)];
                Node end = g.vertices[UnityEngine.Random.Range(0, numVertices)];


                int cost = UnityEngine.Random.Range(1, 10);
                accumulatedCost += cost;

                // Ensure that the edge is not a self-loop
                if (start != end)
                {
                    // Ensure that the edge does not form a circuit
                    if (unionFindHelper.Union(start.ID, end.ID))
                    {
                        g.edges.Add(new Edge(start, end, cost, false));
                    }
                }
            }

            return g;
        }
        
    }
}