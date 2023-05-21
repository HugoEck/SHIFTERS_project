using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject _menuCanvasGO;
    [SerializeField] private GameObject _settingsCanvasGO;

    private bool _bIsPaused;

    private void Start()
    {
        _menuCanvasGO.SetActive(false);
        _settingsCanvasGO.SetActive(false);
    }
    private void Update()
    {
        if (MenuInputManager.instance.MenuOpenCloseInput)
        {
            if (!_bIsPaused)
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
        _bIsPaused = true;
        Time.timeScale = 0;

        OpenMainMenu();
    }
    public void UnPause()
    {
        _bIsPaused = false;
        Time.timeScale = 1f;

        CloseAllMenus();

    }
    #endregion

    #region Activations/Deactivations
    private void OpenMainMenu()
    {
        _menuCanvasGO.SetActive(true);
        _settingsCanvasGO.SetActive(false);

        //EventSystem.current.SetSelectedGameObject(menuFirst);
    }
    private void CloseAllMenus()
    {
        _menuCanvasGO.SetActive(false);
        _settingsCanvasGO.SetActive(false);

        //EventSystem.current.SetSelectedGameObject(null);
    }

    private void OpenSettingsMenuHandle()
    {
        _settingsCanvasGO.SetActive(true);
        _menuCanvasGO.SetActive(false);

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
