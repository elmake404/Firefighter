using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningRoom : MonoBehaviour
{
    [SerializeField]
    private Stage _mainStage;
    [SerializeField]
    private MeshRenderer _window;
    [SerializeField]
    private Collider _colliderMain;
    [SerializeField]
    private ParticleSystem _FbxSmoke, _fbxFire, _fbxPressurisedSteam;
    private bool _isBurn = false;
    private void Awake()
    {
        _fbxPressurisedSteam.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4 && _isBurn)
        {
            _fbxFire.transform.localScale = Vector3.Lerp(_fbxFire.transform.localScale, Vector3.zero, 0.1f);
            if (_fbxFire.transform.localScale.x <= 0.1)
            {
                PutOut();
            }
        }
    }
    public void ActivationFair()
    {
        _isBurn = true;
        _colliderMain.isTrigger = true;
        _window.enabled = false;
        _fbxFire.Play();
        _FbxSmoke.Play();
        _fbxPressurisedSteam.Stop();
    }
    public void PutOut()
    {
        _mainStage.NumberBurningRooms--;
        _colliderMain.enabled = false;
        _fbxFire.Stop();
        _FbxSmoke.Stop();
        _fbxPressurisedSteam.Play();
        StartCoroutine(StopSteem());
    }
    private IEnumerator StopSteem()
    {
        yield return new WaitForSeconds(5);
        _fbxPressurisedSteam.Stop();
    }
    [ContextMenu("SearchComponent")]
    private void SearchComponent()
    {
        _colliderMain = GetComponent<Collider>();
        _window = GetComponent<MeshRenderer>();
        _mainStage = GetComponentInParent<Stage>();
    }
}
