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

            Vector3 startPos = startVertex.transform.position;
            Vector3 endPos = endVertex.transform.position;
            Vector3 midPoint = (endPos + startPos) / 2 + new Vector3(0, 1, 0);
            Vector3 rot = endPos - startPos;

            text.transform.position = midPoint;
            text.text = cost.ToString();

            InitializeParticleSystem(startPos, endPos, midPoint, rot);
            InitializeLineRenderer(startPos, endPos);            
        }

        private void InitializeParticleSystem(Vector3 startPos, Vector3 endPos, Vector3 pos, Vector3 rot)
        {
            particleSystem.transform.position = startPos;
            particleSystem.transform.rotation = Quaternion.LookRotation(rot, Vector3.up);

            var mainModule = particleSystem.main;
            mainModule.startLifetime = new ParticleSystem.MinMaxCurve(0, Vector3.Distance(startPos, endPos) / mainModule.startSpeed.constant);
        }

        private void InitializeLineRenderer(Vector3 startPos, Vector3 endPos)
        {
            lineRenderer = GetComponent<LineRenderer>();

            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);
            lineRenderer.material.color = normalColor;

            MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();

            //Generate the mesh in world space
            Mesh mesh = new Mesh
            {
                vertices = new Vector3[]
                {
                    startPos,
                    endPos
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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
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

        public void SetColor(bool isError, float lerpWeight)
        {
            lineRenderer.material.color = isError ? errorColor : normalColor;
            var mainModule = particleSystem.main;
            Debug.Log(lerpWeight);
            mainModule.startColor = Color.Lerp(Color.green, Color.red, lerpWeight);
        }

    }
}