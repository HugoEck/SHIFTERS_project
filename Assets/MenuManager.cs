using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject menuCanvasGO;
    [SerializeField] private GameObject settingsCanvasGO;

    //[Header("First Selected Options")]
    //[SerializeField] private GameObject menuFirst;
    //[SerializeField] private GameObject settingsMenuFirst;

    private bool isPaused;

    private void Start()
    {
        menuCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
    }
    private void Update()
    {
        if (MenuInputManager.instance.MenuOpenCloseInput)
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }
    #region Pause/Unpause
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;

        OpenMainMenu();
    }
    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1f;

        CloseAllMenus();

    }
    #endregion

    #region Activations/Deactivations
    private void OpenMainMenu()
    {
        menuCanvasGO.SetActive(true);
        settingsCanvasGO.SetActive(false);

        //EventSystem.current.SetSelectedGameObject(menuFirst);
    }
    private void CloseAllMenus()
    {
        menuCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);

        //EventSystem.current.SetSelectedGameObject(null);
    }

    private void OpenSettingsMenuHandle()
    {
        settingsCanvasGO.SetActive(true);
        menuCanvasGO.SetActive(false);

        //EventSystem.current.SetSelectedGameObject(settingsMenuFirst);
    }

    #endregion

    #region Menu Button Actions

    public void OnsettingsPress()
    {
        //OpenSettingsMenuHandle();
    }

    public void OnResumePress()
    {
       UnPause();
    }

    #endregion

    #region Settings Menu Actions

    public void OnSettingsBackPress()
    {
        OpenMainMenu();
    }

    #endregion
}
