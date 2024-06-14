using UnityEngine;
using TMPro;
using System;

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
        public TextMeshPro text;
        public Color errorColor;
        public Color normalColor;
        public ParticleSystem particleSystem;
        public bool isActive;
        [Range(-10,10)] public int cost;

        private bool isDirected;
        private int ID;
        private LineRenderer lineRenderer;
        public delegate void EdgeEventHandler();
        public event EdgeEventHandler OnEdgeEnabled;
        public event EdgeEventHandler OnEdgeDisabled;

        public Edge(Node start, Node end, int cost, bool directed) {
            startVertex = start;
            endVertex = end;
            this.cost = cost;
            isDirected = directed;
            isActive = false;
        }

        internal void Initialize(Node start, Node end, int cost, bool v) {
            startVertex = start;
            endVertex = end;
            this.cost = cost;
            isDirected = v;

            lineRenderer = GetComponent<LineRenderer>();
            MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();

            Vector3 startVertexPos = startVertex.transform.position;
            Vector3 endVertexPos = endVertex.transform.position;
            Vector3 midPoint = (endVertexPos + startVertexPos) / 2 + new Vector3(0, 1, 0);

            Vector3 rotation = endVertexPos - startVertexPos;

            text.transform.position = midPoint;
            text.text = cost.ToString();

            particleSystem.transform.position = startVertexPos;
            particleSystem.transform.rotation = Quaternion.LookRotation(rotation, Vector3.up);
            
            var mainModule = particleSystem.main;
            mainModule.startLifetime = new ParticleSystem.MinMaxCurve(0, Vector3.Distance(startVertexPos, endVertexPos) / mainModule.startSpeed.constant);
            
            lineRenderer.SetPosition(0, startVertexPos);
            lineRenderer.SetPosition(1, endVertexPos);
            lineRenderer.material.color = normalColor;

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

        public void Update() {
            TapCheck();
            MouseInputCheck();
        }

        // Check if the edge has been clicked
        public void MouseInputCheck() {

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
                        ToggleEdge(!isActive);
                    }
                }
            }
        }

        // Check if the edge has been tapped
        public void TapCheck() {
            
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Touch detected");
                Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(rayFromCamera, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        ToggleEdge(!isActive);
                    }
                }
            }
        }

        public void ToggleEdge(bool activate)
        {
            isActive = activate;

            if (isActive)
            {
                lineRenderer.enabled = true;
                OnEdgeEnabled?.Invoke();
            }
            else
            {
                lineRenderer.enabled = false;
                OnEdgeDisabled?.Invoke();
            }

        }

        public void SetColor(bool isError) {
            lineRenderer.material.color = isError ? errorColor : normalColor;
        }

    }
}