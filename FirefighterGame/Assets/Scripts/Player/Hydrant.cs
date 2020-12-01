using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydrant : MonoBehaviour
{
    public static Hydrant HydrantMain;

    private Vector3 _startPosHydrant, _startPosAim;
    [SerializeField]
    private Transform _aim, _posFair, _pointPositionInhabitans;
    [SerializeField]
    private Rigidbody _rightAttachmentPoint, _leftAttachmentPoint;
    [SerializeField]
    private ParticleSystem _FbxWater;
    [SerializeField]
    private Water _water;

    [SerializeField]
    private float _forseWater, _factorDistance, _radiusFireplug;
    private void Awake()
    {
        HydrantMain = this;
    }
    void Start()
    {
        _startPosAim = _aim.transform.position;
        _startPosHydrant = transform.position;
        StartCoroutine(TurnOnTheWater());
    }
    void FixedUpdate()
    {
        if (LevelManager.IsStartGame && !_FbxWater.isPlaying)
        {
            _FbxWater.Play();
        }

        float posX = (_startPosAim.x - _aim.transform.position.x) * _factorDistance;
        Vector3 pos = new Vector3(_startPosHydrant.x + posX, _startPosHydrant.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, pos, 0.5f);

        transform.LookAt(_aim.position);
    }
    private IEnumerator TurnOnTheWater()
    {
        while (true)
        {
            if (LevelManager.IsStartGame)
            {
                Water water = Instantiate(_water, _posFair.position, _water.transform.rotation);
                water.AddForseWater(_forseWater, transform.forward);
            }
            yield return new WaitForSeconds(0.1f);

            if (LevelManager.IsLoseGame || LevelManager.IsWinGame)
            {
                _FbxWater.Stop();

                break;
            }
        }
    }
    public void RescueOfInhabitants
        (Rigidbody connectRight,Rigidbody connectLeft,Rigidbody newRightAttachmentPoint,Rigidbody newLeftAttachmentPoint ,Transform newPointPositionInhabitans,Transform Inhabitant)
    {
        Inhabitant.position = _pointPositionInhabitans.position;
        Inhabitant.rotation = _pointPositionInhabitans.rotation;
        _rightAttachmentPoint.gameObject.AddComponent<FixedJoint>().connectedBody=connectRight;
        _leftAttachmentPoint.gameObject.AddComponent<FixedJoint>().connectedBody= connectLeft;

        _pointPositionInhabitans = newPointPositionInhabitans;
        _rightAttachmentPoint = newRightAttachmentPoint;
        _leftAttachmentPoint = newLeftAttachmentPoint;
    }
}
