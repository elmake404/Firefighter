using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimControl : MonoBehaviour
{
    private Camera _cam;
    private Ray _ray;
    private Vector3 _posAim;
    private float _distensFromCamra;
    void Start()
    {
        _cam = Camera.main;
        _posAim = transform.position;
        _distensFromCamra = _cam.transform.position.z - transform.position.z;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _ray = _cam.ScreenPointToRay(Input.mousePosition);
            _posAim = (_cam.transform.position - ((_ray.direction) *
                            ((_distensFromCamra) / _ray.direction.z)));
        }
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,_posAim,0.5f);
    }
}
