using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Transform CenterFloor;
    private Arsonist _arsonist;
    private int _numberFloor;
    private bool _isActive;

    //[HideInInspector]
    public int ArsonisNamber;
    public bool IsLast;

    private void FixedUpdate()
    {
        if (ArsonisNamber <= 0 )
        {
            enabled = false;
            if (IsLast)
            {
                LevelManager.IsWinGame = true;
                LevelManager.IsStartGame = false;
            }
            else
            {
                LevelManager.NamberStage++;
                _arsonist.ActivationLift();
                CanvasManager.CanvasManagerMain.ResidentFloor();
            }
        }
    }
    public void Initialization(Arsonist arsonist, int number, int arsonisNamber)
    {
        _numberFloor = number;
        _arsonist = arsonist;
        ArsonisNamber = arsonisNamber;
    }
}
