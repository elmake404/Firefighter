using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurningRoom : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPos;
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
    private int _firePowerConst;
    private float _decayRate, _fillburning, _reignitionTime, _delay;
    private bool _isBurn = false;


    [HideInInspector]
    public bool IsThereIsAResident;
    private void Awake()
    {
        _firePowerConst = _firePower;
        _fillburning = 1;
        _delay = 0.1f;
        _decayRate = 1f / _firePower;
        _burningBar.gameObject.SetActive(false);
        _fbxPressurisedSteam.Stop();
        _fbxFire.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (_firePower > 0)
        {
            if (_reignitionTime>0)
            {
                _reignitionTime -= 0.1f;
            }
            else if (_firePower < _firePowerConst)
            {
                if (_delay<=0)
                {
                    _delay = 0.1f;
                    _fillburning += _decayRate;
                    _fbxFire.transform.localScale += new Vector3(_decayRate, _decayRate, _decayRate);
                    _firePower++;
                }
                else
                {
                    _delay -= Time.fixedDeltaTime;
                }
            }
        }
        _burningBar.fillAmount = Mathf.Lerp(_burningBar.fillAmount,_fillburning, 0.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4 && _isBurn)
        {
            _reignitionTime = 0.5f;
            _fbxFire.transform.localScale -= new Vector3(_decayRate, _decayRate, _decayRate);
            _fillburning -= _decayRate;
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
        _FbxSmoke.Stop();

        if (IsThereIsAResident)
        {
            Inhabitant inhabitant = Instantiate(_inhabitant, _spawnPos.position, Quaternion.identity);
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
        _FbxSmoke.Play();
        StartCoroutine(StopSteem());
    }
    private IEnumerator StopSteem()
    {
        yield return new WaitForSeconds(5);
        _FbxSmoke.Stop();
    }
    [ContextMenu("SearchComponent")]
    private void SearchComponent()
    {
        _colliderMain = GetComponent<Collider>();
        _window = GetComponent<MeshRenderer>();
        _mainStage = GetComponentInParent<Stage>();
    }
}
