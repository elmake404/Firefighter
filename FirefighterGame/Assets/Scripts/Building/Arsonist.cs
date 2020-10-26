using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsonist : MonoBehaviour
{
    [System.Serializable]
    private struct Floor
    {
        public Transform Stage;
        public List<BurningRoom> BurningRooms;
        public int ArsonisNamber;
    }

    [SerializeField]
    private Floor[] _floors;
    void Start()
    {

        RandomArsonis();
    }

    void Update()
    {
        
    }
    private void RandomArsonis()
    {
        for (int i = 0; i < _floors.Length; i++)
        {
            List<BurningRoom> ArsonisRooms = new List<BurningRoom>();

            while (ArsonisRooms.Count < _floors[i].ArsonisNamber)
            {
                if (_floors[i].ArsonisNamber > _floors[i].BurningRooms.Count)
                {
                    Debug.Log("request exceeds the number of rooms, maximum number of rooms " + _floors[i].BurningRooms.Count + " namber "+i);
                    break;
                }

                int namberRoom = Random.Range(0, _floors[i].BurningRooms.Count);
                if (!ArsonisRooms.Contains(_floors[i].BurningRooms[namberRoom]))
                {
                    ArsonisRooms.Add(_floors[i].BurningRooms[namberRoom]);
                    _floors[i].BurningRooms[namberRoom].ActivationFair();
                }
            }

        }
    }
    [ContextMenu("SearchBurningRoom")]
    private void SearchBurningRoom()
    {
        for (int i = 0; i < _floors.Length; i++)
        {
            _floors[i].BurningRooms = new List<BurningRoom>();
            for (int j = 0; j < _floors[i].Stage.childCount; j++)
            {
                BurningRoom room = _floors[i].Stage.GetChild(j).GetComponent<BurningRoom>();
                if(room!=null)
                _floors[i].BurningRooms.Add(room);
            }
        }
    }
}
