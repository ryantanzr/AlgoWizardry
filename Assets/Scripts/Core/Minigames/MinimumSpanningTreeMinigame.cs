using System;
using System.Collections.Generic;
using Algowizardry.Core.GraphTheory;
using Algowizardry.Minigames;
using UnityProgressBar;
using TMPro;
using Algowizardry.Utility;
using System.Diagnostics;

/**********************************************************
 * Author: Ryan Tan
 * A minigame script that emulates Kruskal's or Prim's 
 * algorithm to find a minimum spanning tree of a graph. It
 * reads a graph and provides callbacks to check if the
 * graph is a minimum spanning tree and to reset the graph
 **********************************************************/

namespace Algowizardry.Core.Minigames
{

    public class MinimumSpanningTreeMinigame : Minigame {

        public Graph graph;
        public int costThreshold;
        private UnionFind unionFind;
        private int accumulatedCost;

        public TextMeshProUGUI headerText;
        public TextMeshProUGUI subtitleText;
        public ProgressBar progressBar;

        public MinimumSpanningTreeMinigame(FeaturedTopic topic) : base(topic)
        {
            
        }

        // Awake will load the dialogue for Kruskal and Prim minigames
        public void Awake() 
        {
            DialogueCache.CacheDialogue(FeaturedTopic.Kruskal, JSONParser.ParseDialogue("Assets/Resources/Dialogue/KruskalDialogue.json"));
            DialogueCache.CacheDialogue(FeaturedTopic.Prim, JSONParser.ParseDialogue("Assets/Resources/Dialogue/PrimsDialogue.json")); 

            LoadNewRound(topic);
        }

        // Load a new round of the game with a new topic (Prim or Kruskal)
        private void LoadNewRound(FeaturedTopic topic)
        {
            
            unionFind = new UnionFind(graph.vertices);

            //Generate a new graph
            GraphGenerator.GenerateGraph(ref graph, ref accumulatedCost, ref costThreshold, ref unionFind);

            UnionFind temporary = unionFind.DeepCopy();

            //Get the minimum spanning tree details
            costThreshold = GraphGenerator.DetermineMSTFromGraph(graph.vertices, graph.edges, ref temporary);

            // Set the callbacks for the edges
            foreach (Edge edge in graph.edges)
            {
                edge.OnEdgeDisabled += () => Split(edge);
                edge.text.text = edge.cost.ToString();
            }; 

            try {

                dialogueContainer = DialogueCache.dialogues[topic];
            
            } catch (KeyNotFoundException e) {
            
                UnityEngine.Debug.Log("Dialogue for " + topic + " not found" + e.ToString());
            
            }

            // Set the header text and subtitle text
            headerText.text = (this.topic == FeaturedTopic.Kruskal) ? "Kruskal's Algorithm" : "Prim's Algorithm";
            subtitleText.text = "Current cost: " + accumulatedCost + " / " + costThreshold;

            // Set the progress bar
            progressBar.MaxValue = costThreshold;
            progressBar.Value = costThreshold;

        }

        // Reset the graph to its original state and the 
        // UnionFind data structure. Used when the player
        // is stuck and wants to reset the game
        public override void Reset()
        {
            foreach (Node vertex in graph.vertices)
            {
                vertex.SetIsVisited(false);
                unionFind.MakeSet(vertex);
            }

            foreach (Edge edge in graph.edges)
            {
                edge.isActive = false;
            }

            accumulatedCost = 0;
        }


        // Activate an edge and check if the graph is a minimum spanning tree
        public void Union(Edge selectedEdge) {

            // Check if the edge can be added to the minimum spanning tree
            if (unionFind.Union(selectedEdge.startVertex.ID, selectedEdge.endVertex.ID)) {

                // Add the cost of the edge to the accumulated cost
                accumulatedCost += selectedEdge.cost;

                // Activate the edge
                selectedEdge.isActive = true;

                // Check if the graph is a minimum spanning tree
                if (CheckGraphState() && topic == FeaturedTopic.Kruskal) {

                    // Set the game as completed
                    completedGame = true;

                    OnCompletion();

                }
            }
            else 
            {
                // Do something
            }
        }

        // Deactivate an edge and check if the graph is a minimum spanning tree
        public void Split(Edge selectedEdge) {

            UnityEngine.Debug.Log("Splitting edge");
            // Subtract the cost of the edge from the accumulated cost
            accumulatedCost -= selectedEdge.cost;

            // Deactivate the edge
            selectedEdge.isActive = false;

            // Update the UI
            subtitleText.text = "Current cost: " + accumulatedCost + " / " + costThreshold;
            progressBar.Value = accumulatedCost;

                
            // Check if the graph is a minimum spanning tree
            if (CheckGraphState()) {

                // Set the game as completed
                completedGame = true;

                OnCompletion();

            }
        }
        
        public override bool OnCompletion() {
            return true;
        }

        // Check if the graph is a minimum spanning tree
        public bool CheckGraphState() {
            
            UnionFind temporary = new UnionFind(graph.vertices);
            int costCounter = 0;

            foreach (Edge edge in graph.edges) {
                if (edge.isActive) {
                    temporary.Union(edge.startVertex.ID, edge.endVertex.ID);
                    costCounter += edge.cost;
                }
            }

            return temporary.isSpanningTree && costCounter == costThreshold;  

        }
    }
}