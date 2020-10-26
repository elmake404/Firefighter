using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private Arsonist _arsonist;
    private int _numberFloor;
    
    public void Initialization(Arsonist arsonist,int number)
    {
        _numberFloor = number;
        _arsonist = arsonist;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            _arsonist.NextNumber(_numberFloor);
        }
    }
}
