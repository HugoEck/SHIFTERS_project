using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset_Values : MonoBehaviour
{
    private void Start()
    {      
        // Call the method to check the current scene index
        CheckCurrentSceneIndex();        

    }

    private void CheckCurrentSceneIndex()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 1)
        {
            // Reset the sceneCounter to 0
            LapCounter.sceneCounter = 0;            
            PlayerPrefs.SetInt("SceneCounter", LapCounter.sceneCounter);
            

            // Save the valyue of player scores into playerPrefs
            LapCounter.player1WonARace = 0;
            PlayerPrefs.SetInt("Player1WonARace", LapCounter.player1WonARace);

            LapCounter.player2WonARace = 0;
            PlayerPrefs.SetInt("Player2WonARace", LapCounter.player2WonARace);

            LapCounter.player3WonARace = 0;
            PlayerPrefs.SetInt("Player3WonARace", LapCounter.player3WonARace);

            LapCounter.player4WonARace = 0;
            PlayerPrefs.SetInt("Player4WonARace", LapCounter.player4WonARace);

            // Reset the values of who won the grand prix
            LapCounter.bPlayer1HasWonGrandPrix = false;
            int intValue = LapCounter.bPlayer1HasWonGrandPrix ? 1 : 0;
            PlayerPrefs.SetInt("Player1WonGrandPrix", intValue);

            LapCounter.bPlayer2HasWonGrandPrix = false;
            int intValue2 = LapCounter.bPlayer2HasWonGrandPrix ? 1 : 0;
            PlayerPrefs.SetInt("Player2WonGrandPrix", intValue2);

            LapCounter.bPlayer3HasWonGrandPrix = false;
            int intValue3 = LapCounter.bPlayer3HasWonGrandPrix ? 1 : 0;
            PlayerPrefs.SetInt("Player3WonGrandPrix", intValue3);

            LapCounter.bPlayer4HasWonGrandPrix = false;
            int intValue4 = LapCounter.bPlayer4HasWonGrandPrix ? 1 : 0;
            PlayerPrefs.SetInt("Player4WonGrandPrix", intValue4);

            PlayerPrefs.Save();
        }
        else
        {
            // Load the sceneCounter value from PlayerPrefs
            LapCounter.sceneCounter = PlayerPrefs.GetInt("SceneCounter");

            LapCounter.player1WonARace = PlayerPrefs.GetInt("Player1WonARace");
            LapCounter.player2WonARace = PlayerPrefs.GetInt("Player2WonARace");
            LapCounter.player3WonARace = PlayerPrefs.GetInt("Player3WonARace");
            LapCounter.player4WonARace = PlayerPrefs.GetInt("Player4WonARace");


        }
    }

    private void OnEnable()
    {
        // Register for the play mode state change event
        UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnDisable()
    {
        // Unregister the play mode state change event
        UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange state)
    {
        if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
        {
            // Reset the sceneCounter when exiting play mode
            LapCounter.sceneCounter = 0;

            // Save the updated sceneCounter value to PlayerPrefs (the value will persist throughout all the scenes)
            PlayerPrefs.SetInt("SceneCounter", LapCounter.sceneCounter);

            // Save the valyue of player scores into playerPrefs
            LapCounter.player1WonARace = 0;
            PlayerPrefs.SetInt("Player1WonARace", LapCounter.player1WonARace);

            LapCounter.player2WonARace = 0;
            PlayerPrefs.SetInt("Player2WonARace", LapCounter.player2WonARace);

            LapCounter.player3WonARace = 0;
            PlayerPrefs.SetInt("Player3WonARace", LapCounter.player3WonARace);

            LapCounter.player4WonARace = 0;
            PlayerPrefs.SetInt("Player4WonARace", LapCounter.player4WonARace);

            // Reset the values of who won the grand prix
            LapCounter.bPlayer1HasWonGrandPrix = false;
            int intValue = LapCounter.bPlayer1HasWonGrandPrix ? 1 : 0;
            PlayerPrefs.SetInt("Player1WonGrandPrix", intValue);

            LapCounter.bPlayer2HasWonGrandPrix = false;
            int intValue2 = LapCounter.bPlayer2HasWonGrandPrix ? 1 : 0;
            PlayerPrefs.SetInt("Player2WonGrandPrix", intValue2);

            LapCounter.bPlayer3HasWonGrandPrix = false;
            int intValue3 = LapCounter.bPlayer3HasWonGrandPrix ? 1 : 0;
            PlayerPrefs.SetInt("Player3WonGrandPrix", intValue3);

            LapCounter.bPlayer4HasWonGrandPrix = false;
            int intValue4 = LapCounter.bPlayer4HasWonGrandPrix ? 1 : 0;
            PlayerPrefs.SetInt("Player4WonGrandPrix", intValue4);

            PlayerPrefs.Save();
        }
    }

    private void OnApplicationQuit()
    {
        // Reset the sceneCounter when quitting the game
        LapCounter.sceneCounter = 0;

        // Save the updated sceneCounter value to PlayerPrefs (the value will persist throughout all the scenes)
        PlayerPrefs.SetInt("SceneCounter", LapCounter.sceneCounter);
        
        // Save the valyue of player scores into playerPrefs
        LapCounter.player1WonARace = 0;
        PlayerPrefs.SetInt("Player1WonARace", LapCounter.player1WonARace);

        LapCounter.player2WonARace = 0;
        PlayerPrefs.SetInt("Player2WonARace", LapCounter.player2WonARace);

        LapCounter.player3WonARace = 0;
        PlayerPrefs.SetInt("Player3WonARace", LapCounter.player3WonARace);

        LapCounter.player4WonARace = 0;
        PlayerPrefs.SetInt("Player4WonARace", LapCounter.player4WonARace);

        // Reset the values of who won the grand prix
        LapCounter.bPlayer1HasWonGrandPrix = false;
        int intValue = LapCounter.bPlayer1HasWonGrandPrix ? 1 : 0;
        PlayerPrefs.SetInt("Player1WonGrandPrix", intValue);

        LapCounter.bPlayer2HasWonGrandPrix = false;
        int intValue2 = LapCounter.bPlayer2HasWonGrandPrix ? 1 : 0;
        PlayerPrefs.SetInt("Player2WonGrandPrix", intValue2);

        LapCounter.bPlayer3HasWonGrandPrix = false;
        int intValue3 = LapCounter.bPlayer3HasWonGrandPrix ? 1 : 0;
        PlayerPrefs.SetInt("Player3WonGrandPrix", intValue3);

        LapCounter.bPlayer4HasWonGrandPrix = false;
        int intValue4 = LapCounter.bPlayer4HasWonGrandPrix ? 1 : 0;
        PlayerPrefs.SetInt("Player4WonGrandPrix", intValue4);

        PlayerPrefs.Save();
    }
}


