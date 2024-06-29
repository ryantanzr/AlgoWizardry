using UnityEngine;
using Algowizardry.Utility;
using System;

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

    [RequireComponent(typeof(DialogueContainer))]
    public abstract class Minigame : MonoBehaviour
    {
        
        [SerializeField]
        public FeaturedTopic topic;
        protected DialogueContainer dialogueContainer;

        protected delegate void MinigameEventHandler();
        protected event MinigameEventHandler OnCompletion;

        public Minigame(FeaturedTopic topic)
        {
            this.topic = topic;
        }

        public abstract void Reset();
        public abstract void LoadNewRound();
        public void Completion() 
        {
            OnCompletion?.Invoke();
            OnCompletion = null;
        }

     
    }

}