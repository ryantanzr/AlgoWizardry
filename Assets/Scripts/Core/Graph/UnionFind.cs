using System.Collections.Generic;
using UnityEngine;

/**********************************************************
 * Author: Ryan Tan
 * A Union-Find data structure keeps track of a set of
 * partitioned elements. It checked the connectedness of
 * a graph in near constant time, detects cycles in a graph
 * and is scalable. In this implementation, we use it to
 * determine if a graph is a Spanning Tree and uses
 * a size heuristic
 *********************************************************/

 public class UnionFind {
    private int[] parents;
    private int[] sizes;
    public bool isSpanningTree { get; private set; }

    public UnionFind(List<Vertex> vertices)
    {
        Initialize(vertices);
    }

    // Initialize the UnionFind data structure
    // by creating a set for each vertex
    // and setting the parent of each vertex to itself
    public void Initialize(List<Vertex> vertices)
    {
        isSpanningTree = false;
        parents = new int[parents.Length];
        sizes = new int[sizes.Length];

        foreach (Vertex vertex in vertices)
        {
            MakeSet(vertex);
        }
    }

    public void MakeSet(Vertex vertex)
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

        // If the rank of the source is less than the rank of target
        if (sizes[sourceRoot] < sizes[targetRoot])
        {
            // Set the parent of source to target
            parents[sourceRoot] = targetRoot;
            sizes[targetRoot] += sizes[sourceRoot];
        }
        // If the rank of the source is greater than the rank of target
        else if (sizes[sourceRoot] > sizes[targetRoot])
        {
            // Set the parent of target to source
            parents[targetRoot] = sourceRoot;
            sizes[sourceRoot] += sizes[targetRoot];
        }
        // If the ranks of the roots are equal
        else
        {
            // Set the parent of source to target
            parents[sourceRoot] = targetRoot;
            // Increment the rank of target
            sizes[targetRoot] += sizes[sourceRoot];
        }

        if (sizes[targetRoot] == sizes.Length)
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
        if (sourceRoot == targetRoot && sizes[targetID] < sizes[sourceID])
        {
            // Set the parent of source to source
            parents[targetRoot] = targetRoot;
            // Decrement the rank of source
            sizes[sourceRoot] -= sizes[targetRoot];

            return Constants.OPERATION_SUCCESS;
        }

        return Constants.OPERATION_FAILURE;
    }

 }