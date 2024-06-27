using System.Collections.Generic;
using Algowizardry.Core.GraphTheory;
using Algowizardry.Minigames;
using Algowizardry.Utility;
using UnityProgressBar;
using TMPro;
using UnityEngine;

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
        private int currentDialogueIndex;
        private List<DialogueLine> currentDialogue;

        public TextMeshProUGUI headerText;
        public TextMeshProUGUI subtitleText;
        public TextMeshProUGUI dialoguePanel;
        public ProgressBar progressBar;
        public GameObject completionPanel;

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
        private override void LoadNewRound(FeaturedTopic topic)
        {
            
            unionFind = new UnionFind(graph.vertices);

            //Generate a new graph
            GraphGenerator.GenerateGraph(ref graph, ref accumulatedCost, ref costThreshold, ref unionFind);
            UnionFind temporary = unionFind.DeepCopy();

            //Get the minimum spanning tree details
            costThreshold = GraphGenerator.DetermineMSTFromGraph(graph.vertices, graph.edges, ref temporary);

           
            InitializeCallbacks();
            InitializeUserInterface();
        }

        public override void LoadNewRound()
        {
            ResetCallbacks();
            LoadNewRound(Random.Range(0, 2) == 0 ? FeaturedTopic.Kruskal : FeaturedTopic.Prim);
            Reset();
        }

        private void InitializeUserInterface()
        {

            try {

                dialogueContainer = DialogueCache.dialogues[topic];
                // If it is a new player, display the dialogue
                {
                    currentDialogue = dialogueContainer.dialogues[Utility.Constants.TUTORIAL_DIALOGUE].lines;
                    dialoguePanel.text = currentDialogue[0].text;
                }
            
            } catch (KeyNotFoundException e) {
            
                UnityEngine.Debug.Log("Dialogue for " + topic + " not found" + e.ToString());
            
            }

            // Set the header text and subtitle text
            headerText.text = (this.topic == FeaturedTopic.Kruskal) ? "Kruskal's Algorithm" : "Prim's Algorithm";
            subtitleText.text = "Current cost: " + accumulatedCost + " / " + costThreshold;

            // Set the progress bar
            progressBar.MaxValue = accumulatedCost;
            progressBar.Value = accumulatedCost;

            
        }

        private void InitializeCallbacks()
        {
            // Set the callbacks for the edges
            foreach (Edge edge in graph.edges)
            {
                edge.ToggleEdge(topic == FeaturedTopic.Prim);
                edge.OnEdgeDisabled += () => ToggleEdge(edge, false);
                edge.OnEdgeEnabled += () => ToggleEdge(edge, true);
                edge.text.text = edge.cost.ToString();
            };

            //Set the callbacks for the PlayerProgressStore
            OnCompletion += () => PlayerProgressStore.Instance.MinigameCompleted(this);
        }

        private void ResetCallbacks()
        {
            foreach (Edge edge in graph.edges)
            {
                edge.ClearCallbacks();
            }
        }

        // Reset the graph to its original state and the 
        // UnionFind data structure. Used when the player
        // is stuck and wants to reset the game
        public override void Reset()
        {

            accumulatedCost = 0;

            switch(topic) {
                case FeaturedTopic.Kruskal:
                    
                    foreach (Edge edge in graph.edges)
                    {
                        edge.ToggleEdge(false);
                    }
                    break;
                case FeaturedTopic.Prim:
                    
                    foreach (Edge edge in graph.edges)
                    {
                        edge.ToggleEdge(true);
                    }
                    break;
            }
        }

        // Deactivate an edge and check if the graph is a minimum spanning tree
        public void ToggleEdge(Edge selectedEdge, bool activate) 
        {
            if (activate) 
            {
                // Increase the cost of the edge from the accumulated cost
                accumulatedCost += selectedEdge.cost;
            }
            else 
            {
                // Subtract the cost of the edge from the accumulated cost
                accumulatedCost -= selectedEdge.cost;
            }
           

            // Update the UI
            subtitleText.text = "Current cost: " + accumulatedCost + " / " + costThreshold;
            progressBar.Value = accumulatedCost;
                
            // Check if the graph is a minimum spanning tree
            if (CheckGraphState(selectedEdge)) 
            {

                // Set the game as completed
                Completion();
                completionPanel.SetActive(true);

            }
        }

        public void TraverseDialogue(bool next)
        {
            if (next && currentDialogueIndex < currentDialogue.Count - 1)
            {
                currentDialogueIndex++;
            }
            else if (!next && currentDialogueIndex > 0)
            {
                currentDialogueIndex--;
            }

            dialoguePanel.text = currentDialogue[currentDialogueIndex].text;

        }

        // Check if the graph is a minimum spanning tree
        public bool CheckGraphState(Edge selectedEdge) 
        {
            
            UnionFind temporary = new UnionFind(graph.vertices);
            int costCounter = 0;

            foreach (Edge edge in graph.edges) 
            {
                if (edge.isActive) 
                {
                    edge.SetColor(!temporary.Union(edge.startVertex.ID, edge.endVertex.ID) && edge == selectedEdge, (float)accumulatedCost / (float)progressBar.MaxValue);
                    costCounter += edge.cost;
                }
            }

            return temporary.isSpanningTree && costCounter == costThreshold;  

        }
    }
}