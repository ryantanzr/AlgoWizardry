using System.Collections.Generic;
using System.IO;
using UnityEngine;

/**********************************************************
 * Author: Ryan Tan
 * A utility script that parses JSON files and converts
 * them into C# objects using Unity's built-in JsonUtility
 **********************************************************/

public class DialogueLine
{
    public string Text { get; set; }
}

public class Dialogue
{
    public string Id { get; set; }
    public List<DialogueLine> Lines { get; set; }
}

public class DialogueContainer
{
    public List<Dialogue> Dialogues { get; set; }
}

public static class DialogueParser
{
    public static DialogueContainer ParseDialogue(string filePath)
    {
        var jsonString = File.ReadAllText(filePath);
        var dialogues = JsonUtility.FromJson<DialogueContainer>(jsonString);
        return dialogues;
    }
}