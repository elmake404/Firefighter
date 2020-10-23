using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsonist : MonoBehaviour
{
    [SerializeField]
    private BurningRoom[] _burningRooms;
    [SerializeField]
    private int _ArsonisNamber;
    void Start()
    {
        if (_ArsonisNamber>_burningRooms.Length)
        {
            Debug.LogError("request exceeds the number of rooms, maximum number of rooms " + _burningRooms.Length);
        }

        RandomArsonis();
    }

    void Update()
    {
        
    }
    private void RandomArsonis()
    {
        List <BurningRoom> ArsonisRooms = new List<BurningRoom>();
        while (ArsonisRooms.Count<_ArsonisNamber)
        {
            int namberRoom = Random.Range(0, _burningRooms.Length);
            if (!ArsonisRooms.Contains(_burningRooms[namberRoom]))
            {
                ArsonisRooms.Add(_burningRooms[namberRoom]);
                _burningRooms[namberRoom].ActivationFair();
            }
        }
    }
    [ContextMenu("SearchBurningRoom")]
    private void SearchBurningRoom()
    {
        _burningRooms = FindObjectsOfType<BurningRoom>();
    }
}
