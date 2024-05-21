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

        public override void Reset()
        {
            throw new System.NotImplementedException();
        }

        public override bool OnCompletion()
        {
            throw new System.NotImplementedException();
        }
    }

}