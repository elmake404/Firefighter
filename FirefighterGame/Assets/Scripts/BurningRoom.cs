using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningRoom : MonoBehaviour
{
    [SerializeField]
    private GameObject _window;
    [SerializeField]
    private ParticleSystem _FbxSmoke, _fbxFire;

    public void ActivationFair()
    {
        _window.SetActive(false);
        _fbxFire.Play();
        _FbxSmoke.Play();
    }
    public void PutOut()
    {
        _fbxFire.Stop();
        _FbxSmoke.Stop();

    }
}
