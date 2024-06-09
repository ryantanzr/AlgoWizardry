using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

        // Generate a random graph with a specified number 
        // of vertices and edges The graph is guaranteed
        // to be connected but may have circuits
        public static void GenerateGraph(ref Graph graph, ref int accumulatedCost, ref int mstThreshold, ref UnionFind unionFind)
        {

            int numVertices = graph.vertices.Count;
            int numEdges = graph.edges.Count;

           
            unionFind.Initialize(graph.vertices);
            

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
                    if (!unionFind.isSpanningTree && unionFind.Union(start.ID, end.ID))
                    {
                        accumulatedCost += cost;
                        graph.edges[i].Initialize(start, end, cost, false);
                        i++;
                    }
                    else if (unionFind.isSpanningTree) {

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
        }

        // Generate a minimum spanning tree from the graph
        // using Kruskal's algorithm
        public static int DetermineMSTFromGraph(List<Node> nodes, List<Edge> edges, ref UnionFind helper) {

            int MSTCost = 0;

            // Sort the edges in ascending order
            edges.Sort((x, y) => x.cost.CompareTo(y.cost));

            // Initialize the UnionFind data structure
            helper.Initialize(nodes);

            // Add the edges to the minimum spanning tree
            foreach (Edge edge in edges)
            {
                if (helper.Union(edge.startVertex.ID, edge.endVertex.ID))
                {
                    edge.isActive = true;
                    MSTCost += edge.cost;
                }
            }

            return MSTCost;

        }
        
    }
}