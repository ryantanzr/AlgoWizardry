using System;
using System.Collections.Generic;
using Algowizardry.Utility;
using UnityEngine;

/**********************************************************
 * Author: Ryan Tan
 * A store that keeps track of the player's progress in
 * the minigames. It is an observer that listens to the
 * minigame and updates the player's progress accordingly
 * with actions that can be subscribed to by minigames
 * during initialization
 **********************************************************/

namespace Algowizardry.Core.Minigames {

    public class PlayerProgressStore : Singleton<PlayerProgressStore>
    {

        private Dictionary<FeaturedTopic, int> completedMinigames = new Dictionary<FeaturedTopic, int>();

        public void MinigameCompleted(Minigame minigame)
        {
            if (completedMinigames.ContainsKey(minigame.topic))
            {
                completedMinigames[minigame.topic]++;
            }
            else
            {
                completedMinigames.Add(minigame.topic, 1);
            }
        }

        public bool IsMinigameCompleted(FeaturedTopic topic) => completedMinigames.ContainsKey(topic);

    }
}