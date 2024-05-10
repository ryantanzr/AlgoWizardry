using UnityEngine;

/**********************************************************
 * Author: Ryan Tan
 * An Edge is a class that is used to represent a connection
 * between two vertices in a graph. It has a weight that
 * represents the cost of traversal and 2 vertices
 *********************************************************/

namespace Algowizardry.Core.GraphTheory {
    public class Edge : MonoBehaviour
    {
        public Node startVertex;
        public Node endVertex;

        [Range(-10,10)]
        public int cost;

        public bool isActive;

        private bool isDirected;
        private int ID;

        public Edge(Node start, Node end, int cost, bool directed)
        {
            startVertex = start;
            endVertex = end;
            this.cost = cost;
            isDirected = directed;
            isActive = false;
        }

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
}