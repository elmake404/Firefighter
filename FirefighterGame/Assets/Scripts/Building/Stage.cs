using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private Arsonist _arsonist;
    private int _numberFloor;
    private bool _isActive;

    [HideInInspector]
    public int ArsonisNamber, NumberBurningRooms;
    public bool IsLast;

    private void FixedUpdate()
    {
        if (_isActive&&!LevelManager.IsBurning)
        {
            LevelManager.IsBurning = true;
        }
        if (ArsonisNamber<=0&&NumberBurningRooms<=0)
        {
            LevelManager.IsBurning = false;
            enabled = false;
            if (IsLast)
            {
                LevelManager.IsWinGame = true;
                LevelManager.IsStartGame = false;
            }
        }
    }
    public void Initialization(Arsonist arsonist,int number, int arsonisNamber)
    {
        _numberFloor = number;
        _arsonist = arsonist;
        ArsonisNamber = arsonisNamber;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            _isActive = true;
            _arsonist.NextNumber(_numberFloor);
        }
    }
}
