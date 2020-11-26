using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager CanvasManagerMain;

    [SerializeField]
    private GameObject _menuUi, _gameUI, _winUi, _lostUI;
    [SerializeField]
    private Image[] _stars;
    [SerializeField]
    private Image _progresBar;

    private int _namberArreySrars = 0;
    private float _fillStars,_fillProgres,_numberProgres,_receivedProgres, _numberStars, _receivedStars;
    private void Awake()
    {
        CanvasManagerMain = this;
    }
    void Start()
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].fillAmount = 0;
        }

        if (!LevelManager.IsStartGame)
        {
            _menuUi.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        if (_numberStars < _receivedStars)
        {
            _numberStars += 0.05f;

            _stars[_namberArreySrars].fillAmount += 0.05f;
            if (_stars[_namberArreySrars].fillAmount >= 1 && _namberArreySrars < _stars.Length - 1)
            {
                _namberArreySrars++;
            }
        }

        if (_numberProgres < _receivedProgres)
        {
            _numberProgres += 0.01f;

            _progresBar.fillAmount += 0.01f;
        }
    }
    void Update()
    {
        if (LevelManager.IsStartGame && !_gameUI.activeSelf)
        {
            _gameUI.SetActive(true);
        }
        if (LevelManager.IsWinGame && !_winUi.activeSelf)
        {
            _gameUI.SetActive(false);
            _winUi.SetActive(true);
        }
        if (LevelManager.IsLoseGame && !_lostUI.activeSelf)
        {
            _gameUI.SetActive(false);
            _lostUI.SetActive(true);
        }
    }
    public void InitializationFill(int numberInhabitant,int namberFloor)
    {
        _fillStars = 3f / numberInhabitant;
        _fillProgres = 1f / namberFloor;
    }
    public void ResidentSaved()
    {
        _receivedStars += _fillStars;
    }
    public void ResidentFloor()
    {
        _receivedProgres += _fillProgres;
    }
}
