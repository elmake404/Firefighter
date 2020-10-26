using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField]
    private Vector3  _topRightLimit;
    [SerializeField]
    private float _speed;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (LevelManager.IsStartGame)
        {
            transform.position = Vector3.MoveTowards(transform.position, _topRightLimit, _speed);
        }
    }
    [ContextMenu("RecordsTopRightLimit")]
    private void RecordsTopRightLimit()
    {
        _topRightLimit = transform.position;
    }

}
