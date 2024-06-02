using Algowizardry.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using Algowizardry.Core;

/**********************************************************
 * Author: Ryan Tan
 * An Edge is a class that is used to represent a connection
 * between two vertices in a graph. It has a weight that
 * represents the cost of traversal and 2 vertices
 *********************************************************/


namespace Algowizardry.Core.GraphTheory {
    
    [RequireComponent(typeof(LineRenderer))]
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

        public void Start()
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();

            Vector3 startVertexPos = startVertex.transform.position;
            Vector3 endVertexPos = endVertex.transform.position;
           
            lineRenderer.SetPosition(0, startVertexPos);
            lineRenderer.SetPosition(1, endVertexPos);

            //Generate the mesh in world space
            Mesh mesh = new Mesh
            {
                vertices = new Vector3[]
                {
                    startVertexPos,
                    endVertexPos
                }
            };

            lineRenderer.BakeMesh(mesh);
            
            meshCollider.sharedMesh = mesh;

        }

        public void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Touch detected");
                Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(rayFromCamera, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        ToggleEdge();
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse click detected");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Hit detected" + hit.collider.gameObject.name);
                    if (hit.collider.gameObject == gameObject)
                    {
                        ToggleEdge();
                    }
                }
            }
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