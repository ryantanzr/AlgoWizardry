using System.Collections.Generic;
using System.IO;
using UnityEngine;

/**********************************************************
 * Author: Ryan Tan
 * A utility script that parses JSON files and converts
 * them into C# objects using Unity's built-in JsonUtility
 **********************************************************/

namespace Algowizardry.Utility { 

    [System.Serializable]
    public class DialogueLine
    {
        public string text;
    }

    [System.Serializable]
    public class Dialogue
    {
        public string id;
        public List<DialogueLine> lines;
    }

    [System.Serializable]
    public class DialogueContainer
    {
        public List<Dialogue> dialogues;
    }

    [System.Serializable]
    public class Wrapper<T>
    {
        public T[] Dialogues;
    }

    public static class DialogueParser
    {
        private static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Dialogues;
        }
        public static DialogueContainer ParseDialogue(string filePath)
        {
            
            var jsonString = File.ReadAllText(filePath);
            var dialogues = JsonUtility.FromJson<DialogueContainer>(jsonString);
            return dialogues;
        }
    }
}