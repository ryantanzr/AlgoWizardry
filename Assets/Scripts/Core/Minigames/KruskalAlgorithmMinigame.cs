using System;
using System.Collections.Generic;
using UnityEngine;
using Algowizardry.Core.GraphTheory;
using Algowizardry.Utility;

/**********************************************************
 * Author: Ryan Tan
 * A minigame script that emulates Kruskal's algorithm to
 * find a minimum spanning tree of a graph. It reads a
 * graph and provides callbacks to check if the graph is
 * a minimum spanning tree and to reset the graph
 **********************************************************/

namespace Algowizardry.Core.Minigames{
    public class KruskalAlgorithmMinigame : Minigame {

        public Graph graph;
        public int costThreshold;
        private int accumulatedCost;
        private UnionFind unionFind;

        
        // Start is called before the first frame update
        void Start()
        {
            unionFind = new UnionFind(graph.vertices);
            dialogueContainer = DialogueParser.ParseDialogue("Assets/Scripts/Core/Minigames/Dialogues/KruskalAlgorithm.json");
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

        // Load the next graph and reset the UnionFind data structure
        // and the accumulated cost. Used when the player progresses
        public void LoadNextGraph(Graph newGraph, int newThreshold) {
            
            // Reset the graph, flags and UnionFind
            graph = newGraph;
            costThreshold = newThreshold;
            accumulatedCost = 0;
            completedGame = false;

            unionFind.Initialize(graph.vertices);

        }

        public void Union(Edge selectedEdge) {

            // Check if the edge can be added to the minimum spanning tree
            if (unionFind.Union(selectedEdge.startVertex.ID, selectedEdge.endVertex.ID)) {

                // Add the cost of the edge to the accumulated cost
                accumulatedCost += selectedEdge.cost;

                // Activate the edge
                selectedEdge.isActive = true;

                // Check if the graph is a minimum spanning tree
                if (CheckGraphState()) {

                    // Set the game as completed
                    completedGame = true;

                    // Do something

                }
            }
            else 
            {
                // Do something
            }
        }

        public void Split(Edge selectedEdge) {

            // Check if the edge can be removed from the minimum spanning tree
            if (unionFind.Split(selectedEdge.startVertex.ID, selectedEdge.endVertex.ID)) {

                // Subtract the cost of the edge from the accumulated cost
                accumulatedCost -= selectedEdge.cost;

                // Deactivate the edge
                selectedEdge.isActive = false;
            }

        }

        // Check if the graph is a minimum spanning tree
        public bool CheckGraphState() => unionFind.isSpanningTree && accumulatedCost <= costThreshold;

    }
}