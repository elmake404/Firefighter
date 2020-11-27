using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurningRoom : MonoBehaviour
{
    [SerializeField]
    private Image _burningBar;
    [SerializeField]
    private Inhabitant _inhabitant;
    [SerializeField]
    private Stage _mainStage;
    [SerializeField]
    private MeshRenderer _window;
    [SerializeField]
    private Collider _colliderMain;
    [SerializeField]
    private ParticleSystem _FbxSmoke, _fbxFire, _fbxPressurisedSteam;

    [SerializeField]
    private int _firePower = 20;
    private float _decayRate,_fillAmount;
    private bool _isBurn = false;


    [HideInInspector]
    public bool IsThereIsAResident;
    private void Awake()
    {
        _decayRate = 1f / _firePower;
        _burningBar.gameObject.SetActive(false);
        _fbxPressurisedSteam.Stop();
        _fbxFire.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4 && _isBurn)
        {
            _fbxFire.transform.localScale -= new Vector3(_decayRate, _decayRate, _decayRate);
            _burningBar.fillAmount -= _decayRate;
            _firePower--;

            if (_firePower <= 0)
            {
                _inhabitant.Saved();
                PutOut();
            }
        }
    }
    public void ActivationFair()
    {
        _burningBar.gameObject.SetActive(true);
        _fbxFire.gameObject.SetActive(true);

        _isBurn = true;
        _colliderMain.isTrigger = true;
        _window.enabled = false;
        _fbxFire.Play();
        _FbxSmoke.Play();
        _fbxPressurisedSteam.Stop();

        if (IsThereIsAResident)
        {
            Inhabitant inhabitant = Instantiate(_inhabitant, transform.position, Quaternion.identity);
            inhabitant.transform.SetParent(transform);
            _inhabitant = inhabitant;
        }
    }
    public void PutOut()
    {
        _fbxFire.gameObject.SetActive(false);

        _mainStage.ArsonisNamber--;
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
