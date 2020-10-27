using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydrant : MonoBehaviour
{
    private Vector3 _startPosHydrant, _startPosAim;
    [SerializeField]
    private Transform _aim, _posFair;
    [SerializeField]
    private ParticleSystem _FbxWater;
    [SerializeField]
    private Water _water;

    [SerializeField]
    private float _forseWater, _factorDistance, _radiusFireplug;
    void Start()
    {
        _startPosAim = _aim.transform.position;
        _startPosHydrant = transform.position;
        StartCoroutine(TurnOnTheWater());
    }
    void FixedUpdate()
    {
        if (LevelManager.IsStartGame&&!_FbxWater.isPlaying)
        {
            _FbxWater.Play();
        }
        else if (LevelManager.IsWinGame && _FbxWater.isPlaying)
        {
            _FbxWater.Stop();

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
        }
    }

}
