using System.Collections.Generic;
using Algowizardry.Core.Minigames;
using Algowizardry.Utility;

namespace Algowizardry.Minigames { 

    public static class DialogueCache {

        public static Dictionary<FeaturedTopic, DialogueContainer> dialogues = new Dictionary<FeaturedTopic, DialogueContainer>();

        public static void CacheDialogue(FeaturedTopic topic, DialogueContainer dialogue) {
            if(!dialogues.ContainsKey(topic)) {
                dialogues.Add(topic, dialogue);
            }
        }
    }
}