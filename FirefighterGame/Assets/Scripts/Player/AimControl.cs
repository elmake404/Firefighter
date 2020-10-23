using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimControl : MonoBehaviour
{
    private Camera _cam;
    private Ray _ray;
    private Vector3 _posAim, _oldAimPos, _startMosePos;
    [SerializeField]
    private Vector3 _bottomLeftLimit, _topRightLimit;
    private float _distensFromCamra;
    void Start()
    {
        _cam = Camera.main;
        _posAim = transform.position;
        _distensFromCamra = _cam.transform.position.z - transform.position.z;
        //RecordsLimits();
    }

    void Update()
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
                            ((_distensFromCamra) / _ray.direction.z)))-_startMosePos));
        }
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _posAim, 0.5f);
    }
    //private void RecordsLimits()
    //{
    //    Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0));
    //    _topRightLimit = (_cam.transform.position - ((_ray.direction) *
    //                         ((_distensFromCamra) / _ray.direction.z)));

    //    ray = _cam.ScreenPointToRay(Vector3.zero);
    //    _bottomLeftLimit = (_cam.transform.position - ((_ray.direction) *
    //                         ((_distensFromCamra) / _ray.direction.z)));
    //}
    private Vector3 FrameCheck(Vector3 vector)
    {
        if (vector.y>_topRightLimit.y)
        {
            vector.y = _topRightLimit.y;
        }
        if (vector.x < _topRightLimit.x)
        {
            vector.x = _topRightLimit.x;
        }
        if (vector.y<_bottomLeftLimit.y)
        {
            vector.y = _bottomLeftLimit.y;
        }
        if (vector.x >_bottomLeftLimit.x)
        {
            vector.x = _bottomLeftLimit.x;
        }
        return vector;
    }
    [ContextMenu("RecordsBottomLeftLimit")]
    private void RecordsBottomLeftLimit()
    {
        _bottomLeftLimit = transform.position;
    }
    [ContextMenu("RecordsTopRightLimit")]
    private void RecordsTopRightLimit()
    {
        _topRightLimit = transform.position;
    }
}
