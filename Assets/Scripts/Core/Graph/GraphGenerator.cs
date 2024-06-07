using System.Collections.Generic;
using System.Diagnostics;

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

        // Generate a random graph with a specified number 
        // of vertices and edges The graph is guaranteed
        // to be connected but may have circuits
        public static Graph GenerateGraph(ref Graph graph, ref int accumulatedCost, ref int mstThreshold)
        {

            int numVertices = graph.vertices.Count;
            int numEdges = graph.edges.Count;

            // Initialize the UnionFind data structure
            if (unionFindHelper == null) {
                unionFindHelper = new UnionFind(graph.vertices);
            } else {
                unionFindHelper.Initialize(graph.vertices);
            }

            // Add random edges with random costs >= 1
            // Ensure that the graph is connected and
            // no circuits are formed
            for (int i = 0; i < numEdges;)
            {
                Node start = graph.vertices[UnityEngine.Random.Range(0, numVertices)];
                Node end = graph.vertices[UnityEngine.Random.Range(0, numVertices)];
                int cost = UnityEngine.Random.Range(1, 10);

                // Ensure that the edge is not a self-loop
                if (start != end)
                {
                    UnityEngine.Debug.Log("Start: " + start.ID + " End: " + end.ID);
                    // Prioritize a connected graph first
                    // Ensure that the edge does not form a circuit
                    if (!unionFindHelper.isSpanningTree && unionFindHelper.Union(start.ID, end.ID))
                    {
                        graph.edges[i].Initialize(start, end, cost, false);
                        accumulatedCost += cost;
                        mstThreshold = accumulatedCost;
                        i++;
                    }
                    else if (unionFindHelper.isSpanningTree) {

                        //Check if the edge already exists
                        bool edgeExists = false;
                        foreach (Edge edge in graph.edges)
                        {
                            if ((edge.startVertex == start && edge.endVertex == end) ||
                                (edge.startVertex == end && edge.endVertex == start))
                            {
                                edgeExists = true;
                                break;
                            }
                        }

                        if (!edgeExists)
                        {
                            accumulatedCost += cost;
                            graph.edges[i].Initialize(start, end, cost, false);
                            i++;
                        }
                    }
                }
            }

            return graph;
        }
        
    }
}