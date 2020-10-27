using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuUi, _gameUI, _winUi, _lostUI;
    void Start()
    {
        if (!LevelManager.IsStartGame)
        {
            _menuUi.SetActive(true);
        }
    }

    void Update()
    {
        if (LevelManager.IsStartGame&&!_gameUI.activeSelf)
        {
            _gameUI.SetActive(true);
        }
        if (LevelManager.IsWinGame && !_winUi.activeSelf)
        {
            _gameUI.SetActive(false);
            _winUi.SetActive(true);
        }
    }
}
