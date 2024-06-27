using UnityEngine;
using Algowizardry.Core.GraphTheory;

/**********************************************************
 * Author: Ryan Tan
 * A minigame script that emulates Pathfinding algorithms
 * to find an optimal path between two points. It reads
 * a graph and provides callbacks
 **********************************************************/

namespace Algowizardry.Core.Minigames {

    public class PathfindingMinigame : Minigame
    {
        public Graph graph;

        public PathfindingMinigame(FeaturedTopic topic) : base(topic)
        {
            
        }

        public void Awake()
        {
            DialogueCache.CacheDialogue(FeaturedTopic.DFS, JSONParser.ParseDialogue("Assets/Resources/Dialogue/DFS.json"));
            DialogueCache.CacheDialogue(FeaturedTopic.BFS, JSONParser.ParseDialogue("Assets/Resources/Dialogue/PrimsDialogue.json")); 
        }

         // Load a new round of the game with a new topic (Prim or Kruskal)
        private override void LoadNewRound(FeaturedTopic topic)
        {
            
        }

        public override void LoadNewRound()
        {
            
        }

        public override void Reset()
        {
            throw new System.NotImplementedException();
        }

        public bool CheckGameState() 
        {

        }
    }

}