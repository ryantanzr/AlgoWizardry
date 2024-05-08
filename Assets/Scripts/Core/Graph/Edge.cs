using UnityEngine;

/**********************************************************
 * Author: Ryan Tan
 * An Edge is a class that is used to represent a connection
 * between two vertices in a graph. It has a weight that
 * represents the cost of traversal and 2 vertices
 *********************************************************/

public class Edge : MonoBehaviour
{
    public Vertex startVertex;
    public Vertex endVertex;

    [Range(-10,10)]
    public int cost;

    public bool isActive;

    private bool isDirected;
    private int ID;

    public void ToggleEdge()
    {
        isActive = !isActive;

        if (isActive)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

}
