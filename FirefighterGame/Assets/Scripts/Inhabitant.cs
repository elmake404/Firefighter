using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inhabitant : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer _meshRenderer;
    [SerializeField]
    private Color _newColor;

    [SerializeField]
    private float _speed;
    private bool _isAlive, _isSaved;
    void Start()
    {
        Material material = new Material(Shader.Find("Standard"));
        material.color = _meshRenderer.material.color;
        material.mainTexture = _meshRenderer.material.mainTexture;
        _meshRenderer.material = material;
        _isAlive = true;
        _isSaved = false;
    }

    void FixedUpdate()
    {
        if (!_isSaved && _isAlive)
        {
            _meshRenderer.material.color = Vector4.MoveTowards(_meshRenderer.material.color, _newColor, _speed);
            if (_meshRenderer.material.color == new Color(1, 0, 0))
            {
                Debug.Log("red");
                LevelManager.MaximumNumberOfDeadInhabitants--;
                //Debug.Log(LevelManager.MaximumNumberOfDeadInhabitants);
                _isAlive = false;
            }
        }
    }
    public void Saved()
    {
        if (_isAlive)
        {
            CanvasManager.CanvasManagerMain.ResidentSaved();
            _meshRenderer.material.color = new Color(1, 1, 1);
            _isSaved = true;
        }
    }
}
