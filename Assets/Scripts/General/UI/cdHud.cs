using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cdHud : MonoBehaviour
{
    private PlayerInput[] _playerInput;

    [SerializeField] private string _playerName;

    private LapCounter _lapCounter;

    [SerializeField] TextMeshProUGUI lapCounterP0;
    [SerializeField] TextMeshProUGUI lapCounterP1;
    [SerializeField] TextMeshProUGUI lapCounterP2;
    [SerializeField] TextMeshProUGUI lapCounterP3;

    #region PLAYER UI IMAGES
    [SerializeField] Image image0;
    [SerializeField] Image image1;
    [SerializeField] Image image2;
    [SerializeField] Image image3;

    [SerializeField] Image cooldownImage0;
    [SerializeField] Image cooldownImage1;
    [SerializeField] Image cooldownImage2;
    [SerializeField] Image cooldownImage3;

    [SerializeField] Image playerIndicator0;

    [SerializeField] Image playerShapeSquare0;
    [SerializeField] Image playerShapeCircle0;
    [SerializeField] Image playerShapeTriangle0;
    [SerializeField] Image playerShapeStar0;

    [SerializeField] Image playerShapeSquare1;
    [SerializeField] Image playerShapeCircle1;
    [SerializeField] Image playerShapeTriangle1;
    [SerializeField] Image playerShapeStar1;

    [SerializeField] Image playerShapeSquare2;
    [SerializeField] Image playerShapeCircle2;
    [SerializeField] Image playerShapeTriangle2;
    [SerializeField] Image playerShapeStar2;

    [SerializeField] Image playerShapeSquare3;
    [SerializeField] Image playerShapeCircle3;
    [SerializeField] Image playerShapeTriangle3;
    [SerializeField] Image playerShapeStar3;

    [SerializeField] Image playerUI0;
    [SerializeField] Image playerUI1;
    [SerializeField] Image playerUI2;
    [SerializeField] Image playerUI3;

    [SerializeField] TextMeshProUGUI lapText0;
    [SerializeField] TextMeshProUGUI lapText1;
    [SerializeField] TextMeshProUGUI lapText2;
    [SerializeField] TextMeshProUGUI lapText3;
    #endregion

    private void Start()
    {
        StartCoroutine(Cooldown());

        #region PLAYER UI IMAGES

        image0.enabled = false;
        image1.enabled = false;
        image2.enabled = false;
        image3.enabled = false;

        cooldownImage0.enabled = false;
        cooldownImage1.enabled = false;
        cooldownImage2.enabled = false;
        cooldownImage3.enabled = false;

        playerIndicator0.enabled = false;

        playerShapeSquare0.enabled = false;
        playerShapeCircle0.enabled = false;
        playerShapeTriangle0.enabled = false;
        playerShapeStar0.enabled = false;

        playerShapeSquare1.enabled = false;
        playerShapeCircle1.enabled = false;
        playerShapeTriangle1.enabled = false;
        playerShapeStar1.enabled = false;

        playerShapeSquare2.enabled = false;
        playerShapeCircle2.enabled = false;
        playerShapeTriangle2.enabled = false;
        playerShapeStar2.enabled = false;

        playerShapeSquare3.enabled = false;
        playerShapeCircle3.enabled = false;
        playerShapeTriangle3.enabled = false;
        playerShapeStar3.enabled = false;

        playerUI0.enabled = false;
        playerUI1.enabled = false;
        playerUI2.enabled = false;
        playerUI3.enabled = false;

        lapText0.enabled = false;
        lapText1.enabled = false;
        lapText2.enabled = false;
        lapText3.enabled = false;

        #endregion
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(_playerInput.Length);

        if (_playerInput.Length >= 1)
        {
            image0.enabled = true;
            cooldownImage0.enabled = true;
            playerIndicator0.enabled = true;
        }
        if (_playerInput.Length >= 2)
        {
            image1.enabled = true;
            cooldownImage1.enabled = true;
        }
        if (_playerInput.Length >= 3)
        {
            image2.enabled = true;
            cooldownImage2.enabled = true;
        }
        if (_playerInput.Length >= 4)
        {
            image3.enabled = true;
            cooldownImage3.enabled = true;
        }

        foreach (PlayerInput player in _playerInput)
        {
            playerIndicator0.color = Color.green;

            if (player.playerIndex == 0)
            {
                Shift_Shape _shiftShape = player.GetComponent<Shift_Shape>();

                P0ImageShape();
                P0Lapcounter();

                if (_shiftShape.BIsCooldownActive == true)
                {
                    image0.color = Color.red;

                    image0.fillAmount += Time.deltaTime * (1f / 15f);
                }
                else if (_shiftShape.BIsCountdownActive == true)
                {
                    image0.color = Color.yellow;

                    image0.fillAmount -= Time.deltaTime * (1f / 5f);
                }
                else if (_shiftShape.CooldownTimer <= 0)
                {
                    image0.color = Color.yellow;
                }
            }
            if (player.playerIndex == 1)
            {
                Shift_Shape _shiftShape = player.GetComponent<Shift_Shape>();

                P1ImageShape();
                P1Lapcounter();

                if (_shiftShape.BIsCooldownActive == true)
                {
                    image1.color = Color.red;

                    image1.fillAmount += Time.deltaTime * (1f / 15f);
                }
                else if (_shiftShape.BIsCountdownActive == true)
                {
                    image1.color = Color.yellow;

                    image1.fillAmount -= Time.deltaTime * (1f / 5f);
                }
                else if (_shiftShape.CooldownTimer <= 0)
                {
                    image1.color = Color.yellow;
                }
            }
            if (player.playerIndex == 2)
            {
                Shift_Shape _shiftShape = player.GetComponent<Shift_Shape>();

                P2ImageShape();
                P2Lapcounter();

                if (_shiftShape.BIsCooldownActive == true)
                {
                    image2.color = Color.red;

                    image2.fillAmount += Time.deltaTime * (1f / 15f);
                }
                else if (_shiftShape.BIsCountdownActive == true)
                {
                    image2.color = Color.yellow;

                    image2.fillAmount -= Time.deltaTime * (1f / 5f);
                }
                else if (_shiftShape.CooldownTimer <= 0)
                {
                    image2.color = Color.yellow;
                }
            }
            if (player.playerIndex == 3)
            {
                Shift_Shape _shiftShape = player.GetComponent<Shift_Shape>();

                P3ImageShape();
                P3Lapcounter();

                if (_shiftShape.BIsCooldownActive == true)
                {
                    image3.color = Color.red;

                    image3.fillAmount += Time.deltaTime * (1f / 15f);
                }
                else if (_shiftShape.BIsCountdownActive == true)
                {
                    image3.color = Color.yellow;

                    image3.fillAmount -= Time.deltaTime * (1f / 5f);
                }
                else if (_shiftShape.CooldownTimer <= 0)
                {
                    image3.color = Color.yellow;
                }
            }
        }
    }

    private void P0ImageShape()
    {
        foreach (PlayerInput player in _playerInput)
        {
            if (player.playerIndex == 0)
            {
                Shift_Shape _shiftShape = player.GetComponent<Shift_Shape>();

                playerShapeSquare0.color = Color.green;
                playerShapeCircle0.color = Color.green;
                playerShapeTriangle0.color = Color.green;

                playerUI0.enabled = true;
                lapText0.enabled = true;

                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Square)
                {
                    playerShapeSquare0.enabled = true;
                    playerShapeCircle0.enabled = false;
                    playerShapeTriangle0.enabled = false;
                    playerShapeStar0.enabled = false;
                }
                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Circle)
                {
                    playerShapeSquare0.enabled = false;
                    playerShapeCircle0.enabled = true;
                    playerShapeTriangle0.enabled = false;
                    playerShapeStar0.enabled = false;
                }
                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Triangle)
                {
                    playerShapeSquare0.enabled = false;
                    playerShapeCircle0.enabled = false;
                    playerShapeTriangle0.enabled = true;
                    playerShapeStar0.enabled = false;
                }
                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Star && _shiftShape.BIsStarActive == true)
                {
                    playerShapeSquare0.enabled = false;
                    playerShapeCircle0.enabled = false;
                    playerShapeTriangle0.enabled = false;
                    playerShapeStar0.enabled = true;
                }
            }
        }
    }
    private void P1ImageShape()
    {
        foreach (PlayerInput player in _playerInput)
        {
            playerShapeSquare1.color = Color.red;
            playerShapeCircle1.color = Color.red;
            playerShapeTriangle1.color = Color.red;

            if (player.playerIndex == 1)
            {
                Shift_Shape _shiftShape = player.GetComponent<Shift_Shape>();

                playerUI1.enabled = true;
                lapText1.enabled = true;

                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Square)
                {
                    playerShapeSquare1.enabled = true;
                    playerShapeCircle1.enabled = false;
                    playerShapeTriangle1.enabled = false;
                    playerShapeStar1.enabled = false;
                }
                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Circle)
                {
                    playerShapeSquare1.enabled = false;
                    playerShapeCircle1.enabled = true;
                    playerShapeTriangle1.enabled = false;
                    playerShapeStar1.enabled = false;
                }
                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Triangle)
                {
                    playerShapeSquare1.enabled = false;
                    playerShapeCircle1.enabled = false;
                    playerShapeTriangle1.enabled = true;
                    playerShapeStar1.enabled = false;
                }
                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Star && _shiftShape.BIsStarActive == true)
                {
                    playerShapeSquare1.enabled = false;
                    playerShapeCircle1.enabled = false;
                    playerShapeTriangle1.enabled = false;
                    playerShapeStar1.enabled = true;
                }
            }
        }
    }
    private void P2ImageShape()
    {
        foreach (PlayerInput player in _playerInput)
        {
            playerShapeSquare2.color = Color.blue;
            playerShapeCircle2.color = Color.blue;
            playerShapeTriangle2.color = Color.blue;

            if (player.playerIndex == 2)
            {
                Shift_Shape _shiftShape = player.GetComponent<Shift_Shape>();

                playerUI2.enabled = true;
                lapText2.enabled = true;

                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Square)
                {
                    playerShapeSquare2.enabled = true;
                    playerShapeCircle2.enabled = false;
                    playerShapeTriangle2.enabled = false;
                    playerShapeStar2.enabled = false;
                }
                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Circle)
                {
                    playerShapeSquare2.enabled = false;
                    playerShapeCircle2.enabled = true;
                    playerShapeTriangle2.enabled = false;
                    playerShapeStar2.enabled = false;
                }
                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Triangle)
                {
                    playerShapeSquare2.enabled = false;
                    playerShapeCircle2.enabled = false;
                    playerShapeTriangle2.enabled = true;
                    playerShapeStar2.enabled = false;
                }
                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Star && _shiftShape.BIsStarActive == true)
                {
                    playerShapeSquare2.enabled = false;
                    playerShapeCircle2.enabled = false;
                    playerShapeTriangle2.enabled = false;
                    playerShapeStar2.enabled = true;
                }
            }
        }
    }
    private void P3ImageShape()
    {
        foreach (PlayerInput player in _playerInput)
        {
            playerShapeSquare3.color = Color.yellow;
            playerShapeCircle3.color = Color.yellow;
            playerShapeTriangle3.color = Color.yellow;

            if (player.playerIndex == 3)
            {
                Shift_Shape _shiftShape = player.GetComponent<Shift_Shape>();

                playerUI3.enabled = true;
                lapText3.enabled = true;

                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Square)
                {
                    playerShapeSquare3.enabled = true;
                    playerShapeCircle3.enabled = false;
                    playerShapeTriangle3.enabled = false;
                    playerShapeStar3.enabled = false;
                }
                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Circle)
                {
                    playerShapeSquare3.enabled = false;
                    playerShapeCircle3.enabled = true;
                    playerShapeTriangle3.enabled = false;
                    playerShapeStar3.enabled = false;
                }
                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Triangle)
                {
                    playerShapeSquare3.enabled = false;
                    playerShapeCircle3.enabled = false;
                    playerShapeTriangle3.enabled = true;
                    playerShapeStar3.enabled = false;
                }
                if (_shiftShape.currentShapeState.currentShapeState == Shape_Enum.ShapeState.Star && _shiftShape.BIsStarActive == true)
                {
                    playerShapeSquare3.enabled = false;
                    playerShapeCircle3.enabled = false;
                    playerShapeTriangle3.enabled = false;
                    playerShapeStar3.enabled = true;
                }
            }
        }
    }

    private void P0Lapcounter()
    {
        if (_lapCounter == null)
        {
            _lapCounter = FindObjectOfType<LapCounter>();
        }
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Lobby_Scene"))
        {
            int p0Laps = LapCounter.lapCompletedPlayer1;
            int lapsToWin = _lapCounter.lapsToWin + 1;

            lapCounterP0.text = "Laps: " + p0Laps + "/" + lapsToWin;
        }
    }
    private void P1Lapcounter()
    {
        if (_lapCounter == null)
        {
            _lapCounter = FindObjectOfType<LapCounter>();
        }
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Lobby_Scene"))
        {
            int p1Laps = LapCounter.lapCompletedPlayer2;
            int lapsToWin = _lapCounter.lapsToWin + 1;

            lapCounterP1.text = "Laps: " + p1Laps + "/" + lapsToWin;
        }
    }
    private void P2Lapcounter()
    {
        if (_lapCounter == null)
        {
            _lapCounter = FindObjectOfType<LapCounter>();
        }
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Lobby_Scene"))
        {
            int p2Laps = LapCounter.lapCompletedPlayer3;
            int lapsToWin = _lapCounter.lapsToWin + 1;

            lapCounterP2.text = "Laps: " + p2Laps + "/" + lapsToWin;
        }

    }
    private void P3Lapcounter()
    {
        if (_lapCounter == null)
        {
            _lapCounter = FindObjectOfType<LapCounter>();
        }
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Lobby_Scene"))
        {
            int p3Laps = LapCounter.lapCompletedPlayer4;
            int lapsToWin = _lapCounter.lapsToWin + 1;

            lapCounterP3.text = "Laps: " + p3Laps + "/" + lapsToWin;
        }
    }

    private IEnumerator Cooldown()
    {
        while (true)
        {
            _playerInput = GameObject.FindObjectsOfType<PlayerInput>();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
