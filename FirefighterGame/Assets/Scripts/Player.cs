using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _aim, _posFair;
    [SerializeField]
    private Water _water;
    [SerializeField]
    private float _forseWater;
    void Start()
    {
        StartCoroutine(TurnOnTheWater()) ;
    }

    void FixedUpdate()
    {
        transform.LookAt(_aim.position);
    }
    private IEnumerator TurnOnTheWater()
    {
        while (true)
        {
            Water water = Instantiate(_water,_posFair.position,_water.transform.rotation);
            water.AddForseWater(_forseWater, transform.forward);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
