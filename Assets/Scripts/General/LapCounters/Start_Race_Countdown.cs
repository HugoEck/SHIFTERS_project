using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Start_Race_Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countDownText;

    private bool _bHasStartedRace = false;
    private void Update()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Lobby_Scene") && !_bHasStartedRace)
        {
            _bHasStartedRace = true;
            StartCoroutine(StartCountDown());
            FindObjectOfType<AudioManager>().Play("COUNTDOWN");
        }
    }
    private IEnumerator StartCountDown()
    {
        
        _countDownText.text = "3";
        
        yield return new WaitForSeconds(1f);

        _countDownText.text = "2";

        yield return new WaitForSeconds(1f);

        _countDownText.text = "1";

        yield return new WaitForSeconds(1f);

        _countDownText.text = "GO!";

        yield return new WaitForSeconds(1f);

        _countDownText.text = "";
        FindObjectOfType<AudioManager>().Play("SHIFTERS_MUSIC");
    }
}
