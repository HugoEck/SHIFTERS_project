using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Score_Update : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _fillSpeed = 0.1f;
    [SerializeField] private float _fillDelay = 1f;
    [SerializeField] GameObject _won1Race;
    [SerializeField] private GameObject _won2Race;
    [SerializeField] private GameObject _won3Race;

    private bool _bIsFilling = false;
    private bool _bChangedScene = false;
    private bool _bHasFilled = false;

    private int _player1RoundsWon;
    private int _player2RoundsWon;
    private int _player3RoundsWon;
    private int _player4RoundsWon;

    private void Start()
    {
        _image.fillAmount = 0;

        _won1Race.SetActive(false);
        _won2Race.SetActive(false);
        _won3Race.SetActive(false);
    }

    private void Update()
    {
        _player1RoundsWon = LapCounter.player1WonARace;
        _player2RoundsWon = LapCounter.player2WonARace;
        _player3RoundsWon = LapCounter.player3WonARace;
        _player4RoundsWon = LapCounter.player4WonARace;

        if (LapCounter.bIsRaceFinished && !_bHasFilled)
        {
            if (LapCounter.bPlayer1WonRace)
            {
                Color colorGreen = Color.green;
                colorGreen.a = 0.4f;
                _image.color = colorGreen;

                if(_player1RoundsWon == 1)
                {
                    StartCoroutine(ShowScore(_won1Race));                    
                }
                else if(_player1RoundsWon == 2)
                {
                    _won1Race.SetActive(true);
                    StartCoroutine(ShowScore(_won2Race));
                }
                else if(_player1RoundsWon == 3)
                {
                    _won1Race.SetActive(true);
                    _won2Race.SetActive(true);
                    StartCoroutine(ShowScore(_won3Race));
                }
            }
            else if (LapCounter.bPlayer2WonRace)
            {
                Color colorRed = Color.red;
                colorRed.a = 0.4f;
                _image.color = colorRed;

                if (_player2RoundsWon == 1)
                {
                    StartCoroutine(ShowScore(_won1Race));
                }
                else if (_player2RoundsWon == 2)
                {
                    _won1Race.SetActive(true);
                    StartCoroutine(ShowScore(_won2Race));
                }
                else if (_player2RoundsWon == 3)
                {
                    _won1Race.SetActive(true);
                    _won2Race.SetActive(true);
                    StartCoroutine(ShowScore(_won3Race));
                }
            }
            else if (LapCounter.bPlayer3WonRace)
            {
                Color colorBlue = Color.blue;
                colorBlue.a = 0.4f;
                _image.color = colorBlue;

                if (_player3RoundsWon == 1)
                {
                    StartCoroutine(ShowScore(_won1Race));
                }
                else if (_player3RoundsWon == 2)
                {
                    _won1Race.SetActive(true);
                    StartCoroutine(ShowScore(_won2Race));
                }
                else if (_player3RoundsWon == 3)
                {
                    _won1Race.SetActive(true);
                    _won2Race.SetActive(true);
                    StartCoroutine(ShowScore(_won3Race));
                }
            }
            else if (LapCounter.bPlayer4WonRace)
            {
                Color colorYellow = Color.yellow;
                colorYellow.a = 0.4f;
                _image.color = colorYellow;

                if (_player4RoundsWon == 1)
                {
                    StartCoroutine(ShowScore(_won1Race));
                }
                else if (_player4RoundsWon == 2)
                {
                    _won1Race.SetActive(true);
                    StartCoroutine(ShowScore(_won2Race));
                }
                else if (_player4RoundsWon == 3)
                {
                    _won1Race.SetActive(true);
                    _won2Race.SetActive(true);
                    StartCoroutine(ShowScore(_won3Race));
                }
            }

            _image.fillAmount += _fillSpeed * Time.deltaTime;

            if (_image.fillAmount >= 1f)
            {
                _image.fillAmount = 1f;
                _bHasFilled = true;
                if (_bChangedScene)
                {
                    _bChangedScene = false;
                    _image.fillAmount = 0f;
                }
            }
            if(_bHasFilled)
            {
                _image.fillAmount = 1f;
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _bChangedScene = true;
        enabled = true; // Re-enable the script when a new scene is loaded
    }
    private IEnumerator ShowScore(GameObject gameObject)
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(true);

    }
}



