using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{
    public List<Edge> edges;
    private bool isVisited;
    public int ID { get; private set; }

    public void Visit()
    {
        isVisited = true;
    }

    public void Unvisit()
    {
        isVisited = false;
    }
    
}
