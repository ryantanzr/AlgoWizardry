using UnityEngine;
using Algowizardry.Utility;

/**********************************************************
* Author: Ryan Tan
 * A minigame script that encapsulates the basic structure
 * of a minigame. It provides callbacks to reset the game
 * and to check if the game has been completed
 **********************************************************/

namespace Algowizardry.Core.Minigames {

    public enum FeaturedTopic : byte {
        Kruskal = 0,
        Prim = 1,
        DFS = 2,
        BFS = 3,
        Djikstra = 4,
        AStar = 5
    }

    public abstract class Minigame : MonoBehaviour {
        
        protected bool completedGame = false;
        
        [SerializeField]
        protected FeaturedTopic topic;
        protected DialogueContainer dialogueContainer;

        public Minigame(FeaturedTopic topic) {

            this.topic = topic;
        }

        public abstract void Reset();

        public abstract bool OnCompletion();
    }

}