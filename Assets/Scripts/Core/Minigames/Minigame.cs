using UnityEngine;

/**********************************************************
* Author: Ryan Tan
 * A minigame script that encapsulates the basic structure
 * of a minigame. It provides callbacks to reset the game
 * and to check if the game has been completed
 **********************************************************/

public abstract class Minigame : MonoBehaviour {
    
    protected bool completedGame = false;
    protected DialogueContainer dialogueContainer;

    public abstract void Reset();
}