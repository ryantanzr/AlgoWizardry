using UnityEngine;
using UnityEngine.UIElements;

/**********************************************************
 * Author: Ryan Tan
 * SceneLoader is a class that is used to load scenes with
 * the UIDocument component.
 *********************************************************/

[RequireComponent(typeof(UIDocument))]
public class SceneLoader : MonoBehaviour
{   
    public UIDocument uiDocument;
    
    private Button kruskalMinigameButton;
    private Button DFSMinigameButton;

    private EventCallback<ClickEvent> clickEventKruskal;
    private EventCallback<ClickEvent> clickEventDFS;

    // Awake is called when the object is initialized
    // This is used to add event listeners to the buttons
    public void Awake()
    {
        var root = uiDocument.rootVisualElement;
        
        // Get the buttons from the UIDocument
        kruskalMinigameButton = root.Q<Button>("KruskalMinigameButton");
        DFSMinigameButton = root.Q<Button>("DFSMinigameButton");

        // Create the event listeners for the buttons
        clickEventKruskal = evt => LoadScene("GraphMinigame");
        clickEventDFS = evt => LoadScene("TraversalMinigame");

        // Add the event listeners to the buttons
        kruskalMinigameButton.RegisterCallback<ClickEvent>(clickEventKruskal);
        DFSMinigameButton.RegisterCallback<ClickEvent>(clickEventDFS);   
    }

    //OnDisable is called when the object is disabled, This is
    //used to remove the event listeners when the object is disabled
    //to prevent memory leaks
    public void OnDisable()
    {
        // Get the buttons from the UIDocument
        var root = uiDocument.rootVisualElement;

        // Remove the event listeners from the buttons
        kruskalMinigameButton.UnregisterCallback<ClickEvent>(clickEventKruskal);
        DFSMinigameButton.UnregisterCallback<ClickEvent>(clickEventDFS);
    }

    // LoadScene is a function that takes a string parameter
    // and loads the scene with its name
    public void LoadScene(string sceneName)
    {
        try {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        } catch (System.Exception e) {
            Debug.LogError( $"Scene {sceneName} is not found, error: {e}");
        }
    }
}
