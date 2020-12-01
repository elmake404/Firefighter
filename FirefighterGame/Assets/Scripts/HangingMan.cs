using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingMan : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rightHend, _leftHend, _rightAttachmentPoint, _leftAttachmentPoint;
    [SerializeField]
    private Transform _pointPositionInhabitans;
    void Awake()
    {
        Hydrant.HydrantMain.RescueOfInhabitants
            (_rightHend,_leftHend, _rightAttachmentPoint, _leftAttachmentPoint, _pointPositionInhabitans,transform);
    }

    void Update()
    {
        
    }
}
