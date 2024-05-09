using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**********************************************************
 * Author: Ryan Tan
 * Graph is a class that is used to represent a graph
 * which is defined as a collection of connected vertices
 * and edges.
 *********************************************************/
public class Graph : MonoBehaviour
{
    public List<Vertex> vertices;
    public List<Edge> edges;

    private bool isConnected;
    private bool isAcyclic;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitializeGraph()
    {
        
    }

    public void SetIsConnected(bool connected)
    {
        isConnected = connected;
    }

    public void SetIsAcyclic(bool acyclic)
    {
        isAcyclic = acyclic;
    }

}
