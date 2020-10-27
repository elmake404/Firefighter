using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimControl : MonoBehaviour
{
    private Camera _cam;
    private Ray _ray;
    private Vector3 _posAim, _oldAimPos, _startMosePos;
    private float _distensFromCamra;
    void Start()
    {
        _cam = Camera.main;
        _posAim = transform.position;
        _distensFromCamra = _cam.transform.position.z - transform.position.z;

        LevelManager.BottomLeftLimit = _cam.ScreenToWorldPoint(new Vector3(0, 0, _cam.transform.position.z - transform.position.z));
        LevelManager.TopRightLimit = _cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _cam.transform.position.z - transform.position.z));
    }

    void Update()
    {
        if (LevelManager.IsStartGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ray = _cam.ScreenPointToRay(Input.mousePosition);
                _oldAimPos = transform.position;
                _startMosePos = (_cam.transform.position - ((_ray.direction) *
                                 ((_distensFromCamra) / _ray.direction.z)));
            }
            else if (Input.GetMouseButton(0))
            {
                _ray = _cam.ScreenPointToRay(Input.mousePosition);
                _posAim = FrameCheck(_oldAimPos + ((_cam.transform.position - ((_ray.direction) *
                                ((_distensFromCamra) / _ray.direction.z))) - _startMosePos));
            }
        }
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _posAim, 0.5f);
    }
    private Vector3 FrameCheck(Vector3 vector)
    {
        if (vector.y> LevelManager.TopRightLimit.y)
        {
            vector.y = LevelManager.TopRightLimit.y;
        }
        if (vector.x < LevelManager.TopRightLimit.x)
        {
            vector.x = LevelManager.TopRightLimit.x;
        }
        if (vector.y< LevelManager.BottomLeftLimit.y)
        {
            vector.y = LevelManager.BottomLeftLimit.y;
        }
        if (vector.x > LevelManager.BottomLeftLimit.x)
        {
            vector.x = LevelManager.BottomLeftLimit.x;
        }
        return vector;
    }
}
