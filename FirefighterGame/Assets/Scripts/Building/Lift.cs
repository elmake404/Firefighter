using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    private Vector3  _target;
    [SerializeField]
    private List<Transform> _targetLest = new List<Transform>();
    [SerializeField]
    private float _speedNotBurning= 0.05f, _speedBurning= 0.005f;
    private float _speed;
    void Start()
    {
        _speed = _speedNotBurning;
    }

    void FixedUpdate()
    {
        if (LevelManager.IsStartGame)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed);
        }
    }
    public void NewTargetMoving()
    {
        _target = transform.position;
        _target.y += (Camera.main.transform.position.y - _targetLest[LevelManager.NamberStage].position.y);
    }
    public void AddTargets(Transform target)
    {
        _targetLest.Add(target);
    }
}
