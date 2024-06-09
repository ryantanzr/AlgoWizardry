using System.Collections.Generic;
using Algowizardry.Utility;
/**********************************************************
 * Author: Ryan Tan
 * A Union-Find data structure keeps track of a set of
 * partitioned elements. It checked the connectedness of
 * a graph in near constant time, detects cycles in a graph
 * and is scalable. In this implementation, we use it to
 * determine if a graph is a Spanning Tree and uses
 * a size heuristic
 *********************************************************/

namespace Algowizardry.Core.GraphTheory {
    public class UnionFind : IDeepCopyable<UnionFind>{
        public int[] parents {get; private set;}
        public int[] sizes {get; private set;}
        public bool isSpanningTree { get; private set; }

        public UnionFind(List<Node> vertices)
        {
            Initialize(vertices);
        }

        // Initialize the UnionFind data structure
        // by creating a set for each vertex
        // and setting the parent of each vertex to itself
        public void Initialize(List<Node> vertices)
        {
            if (vertices == null) 
            {
                return;
            }
            isSpanningTree = false;
            parents = new int[vertices.Count];
            sizes = new int[vertices.Count];

            foreach (Node vertex in vertices)
            {
                MakeSet(vertex);
            }
        }

        public void MakeSet(Node vertex)
        {
            // Set the parent of the vertex to itself
            parents[vertex.ID] = vertex.ID;
            // Set the rank of the vertex to 0
            sizes[vertex.ID] = 1;
        }

        // Recursive find operation with path compression which
        // finds the root/representative element of the set
        public int Find(int vertexID)
        {
            // If the vertex is not the root of the set
            if (vertexID != parents[vertexID])
            {
                // Recursively find the root of the set
                parents[vertexID] = Find(parents[vertexID]);
            }

            // Return the root of the set
            return parents[vertexID];
        }

        // Union operation that merges two sets together
        // and updates the size heuristic of the partitions
        // the smaller set is merged into the larger set
        public bool Union(int sourceID, int targetID)
        {
            // Find the roots of the sets that v1 and v2 belong to
            int sourceRoot = Find(sourceID);
            int targetRoot = Find(targetID);

            if (sourceRoot == targetRoot)
            {
                return Constants.OPERATION_FAILURE;
            }

            // If the size of the source is less than the size of target
            if (sizes[sourceRoot] < sizes[targetRoot])
            {
                // Set the parent of source to target
                parents[sourceRoot] = targetRoot;
                sizes[targetRoot] += sizes[sourceRoot];
            }
            // If the size of the source is greater than the size of target
            else if (sizes[sourceRoot] > sizes[targetRoot])
            {
                // Set the parent of target to source
                parents[targetRoot] = sourceRoot;
                sizes[sourceRoot] += sizes[targetRoot];
            }
            // If the size of the roots are equal
            else
            {
                // Set the parent of source to target
                parents[sourceRoot] = targetRoot;
                // Increment the size of target
                sizes[targetRoot] += sizes[sourceRoot];
            }

            if (sizes[sourceRoot] == sizes.Length || sizes[targetRoot] == sizes.Length)
            {
                isSpanningTree = true;
            } 
            else
            {
                isSpanningTree = false;
            }

            return Constants.OPERATION_SUCCESS;
        }

        // Split operation that removes the edge between two vertices
        // and updates the size heuristic of the partitions
        // targetID will split from sourceID
        public bool Split(int sourceID, int targetID)
        {
            // Find the roots of the sets that v1 and v2 belong to
            int sourceRoot = Find(sourceID);
            int targetRoot = Find(targetID);

            // If vertices are in the same partition, operate
            if (sourceRoot == targetRoot && sizes[targetID] < sizes[sourceRoot])
            {
                
                // Set the parent of source to source
                parents[targetID] = targetID;
                // Decrement the rank of source
                sizes[sourceRoot] -= sizes[targetID];

                return Constants.OPERATION_SUCCESS;
            }

            return Constants.OPERATION_FAILURE;
        }

        public UnionFind DeepCopy()
        {
            return new UnionFind(null)
            {
                parents = (int[])parents.Clone(),
                sizes = (int[])sizes.Clone(),
                isSpanningTree = isSpanningTree
            };
        }
    }
}