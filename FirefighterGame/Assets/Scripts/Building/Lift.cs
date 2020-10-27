using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField]
    private Vector3  _topRightLimit;
    [SerializeField]
    private float _speedNotBurning, _speedBurning;
    private float _speed;
    void Start()
    {
        _speed = _speedBurning;
    }

    void FixedUpdate()
    {
        if (LevelManager.IsStartGame)
        {
            if (LevelManager.IsBurning)
                _speed = Mathf.Lerp(_speed,_speedBurning,0.1f);
            else
                _speed = Mathf.Lerp(_speed, _speedNotBurning, 0.1f);

            transform.position = Vector3.MoveTowards(transform.position, _topRightLimit, _speed);
        }
    }
    [ContextMenu("RecordsTopRightLimit")]
    private void RecordsTopRightLimit()
    {
        _topRightLimit = transform.position;
    }

}
