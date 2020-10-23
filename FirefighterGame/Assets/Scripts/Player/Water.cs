using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rbMain;

    [SerializeField]
    private float _timeLife;
    private void Start()
    {
        Destroy(gameObject, _timeLife);
    }
    public void AddForseWater(float Forse,Vector3 direction)
    {
        _rbMain.AddForce(direction*Forse,ForceMode.Acceleration);
    }
}
