using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("SHIFTERS_MENY");
    }
    public void PlayGame() // Handles the event when clicking the "Play" button
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
    public void QuitGame() // Handles the event when clicking the "Quit" button
    {
        Application.Quit();
    }
}
