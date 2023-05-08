using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReadyButton : MonoBehaviour
{
    // Create a static variable to count the number of ready players
    private static int readyPlayerCount = 0;
    // Create a boolean variable to track if the player is ready
    private bool isReady = false;

    // Get a reference to the text object of the button
    private Text buttonText;

    void Start()
    {
        // Get a reference to the text component of the button
        buttonText = GetComponentInChildren<Text>();
    }

    // This function is called when the button is clicked
    public void OnReadyButtonClick()
    {
        // Toggle the isReady variable
        isReady = !isReady;

        // Change the text on the button to indicate if the player is ready or not
        buttonText.text = isReady ? "Not Ready" : "Ready";

        // Update the ready player count
        readyPlayerCount += isReady ? 1 : -1;

        // Check if all players are ready
        if (readyPlayerCount == 4)
        {
            // Enable the "Start Game" button
            GameObject startButton = GameObject.Find("StartButton");
            startButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            // Disable the "Start Game" button
            GameObject startButton = GameObject.Find("StartButton");
            startButton.GetComponent<Button>().interactable = false;
        }
    }
    // This function is called when the button is clicked
    public void OnStartButtonClick()
    {
        // Check if all players are ready
        if (ReadyButton.readyPlayerCount == 4)
        {
            // Load the game scene
            SceneManager.LoadScene("GameScene");
        }
    }
}
