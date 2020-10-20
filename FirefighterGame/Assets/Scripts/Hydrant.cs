﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydrant : MonoBehaviour
{
    //private Camera _cam;
    //private Ray _ray;
    private Vector3 /*_posHydrant,*/ _startPosHydrant,_startPosAim;
    [SerializeField]
    private Transform _aim, _posFair;
    [SerializeField]
    private Water _water;

    [SerializeField]
    private float _forseWater, _factorDistance;
    //private float _distensFromCamra;
    void Start()
    {
        //_cam = Camera.main;
        //_posHydrant = transform.position;
        _startPosAim = _aim.transform.position;
        _startPosHydrant = transform.position;
        //_distensFromCamra = _cam.transform.position.z - transform.position.z;

        StartCoroutine(TurnOnTheWater());
    }
    //void Update()
    //{
    //    //if (Input.GetMouseButton(0))
    //    //{
    //    //    _ray = _cam.ScreenPointToRay(Input.mousePosition);
    //    //    _posHydrant =/* _startPosHydrant -*/ (_cam.transform.position - ((_ray.direction) *
    //    //                    ((_distensFromCamra) / _ray.direction.z)));
    //    //    Debug.Log(_posHydrant.x);
    //    //}
    //}

    void FixedUpdate()
    {
        float posX = (_startPosAim.x - _aim.transform.position.x)* _factorDistance;
        Vector3 pos = new Vector3(_startPosHydrant.x + posX, _startPosHydrant.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, pos, 0.5f);
        transform.LookAt(_aim.position);
    }
    private IEnumerator TurnOnTheWater()
    {
        while (true)
        {
            Water water = Instantiate(_water, _posFair.position, _water.transform.rotation);
            water.AddForseWater(_forseWater, transform.forward);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
