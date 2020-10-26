using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsonist : MonoBehaviour
{
    [System.Serializable]
    private struct Floor
    {
        public Stage Stageobj;
        public List<BurningRoom> NoBurningRooms;
        public int ArsonisNamber;
    }

    [SerializeField]
    private Floor[] _floors;
    [SerializeField]
    private bool[] _percentage = new bool[100];
    [SerializeField]
    private float _timeBeforeArson;
    [SerializeField]
    [Range(1, 100)]
    private int _percentageOfFireInTheUpperRoom;
    private int _numberFloor = int.MinValue;
    private void Awake()
    {
        PercentSetting();
        for (int i = 0; i < _floors.Length; i++)
        {
            _floors[i].Stageobj.Initialization(this, i);
        }
    }
    void Start()
    {
        StartCoroutine(RandomArsonis());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _numberFloor++;
        }
    }
    private IEnumerator RandomArsonis()
    {

        while (true)
        {
            if (LevelManager.IsStartGame&&_numberFloor>=0)
            {
                int floorNumberVariation;
                if (_percentage[Random.Range(0, _percentage.Length)] && (_numberFloor + 1) < _floors.Length)
                {
                    floorNumberVariation = _numberFloor + 1;
                }
                else
                {
                    floorNumberVariation = _numberFloor;
                }

                int numberRoom = 0;

                if (_floors[floorNumberVariation].NoBurningRooms.Count > 0)
                {
                    numberRoom = Random.Range(0, _floors[floorNumberVariation].NoBurningRooms.Count);
                }
                else if (CheckFloor())
                {
                    yield return new WaitForSeconds(_timeBeforeArson);
                    continue;
                }
                else
                {
                    continue;
                }

                _floors[floorNumberVariation].NoBurningRooms[numberRoom].ActivationFair();
                _floors[floorNumberVariation].NoBurningRooms.Remove(_floors[floorNumberVariation].NoBurningRooms[numberRoom]);
                yield return new WaitForSeconds(_timeBeforeArson);
            }
            else
            {
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
        }
    }
    [ContextMenu("SearchBurningRoom")]
    private void SearchBurningRoom()
    {
        for (int i = 0; i < _floors.Length; i++)
        {
            _floors[i].NoBurningRooms = new List<BurningRoom>();
            for (int j = 0; j < _floors[i].Stageobj.transform.childCount; j++)
            {
                BurningRoom room = _floors[i].Stageobj.transform.GetChild(j).GetComponent<BurningRoom>();
                if (room != null)
                    _floors[i].NoBurningRooms.Add(room);
            }
        }
    }
    private bool CheckFloor()
    {
        int namber = _numberFloor;
        if ((_numberFloor + 1) < _floors.Length)
        {
            namber++;
        }
        for (int i = _numberFloor; i < namber; i++)
        {
            if ((_floors[namber].NoBurningRooms.Count > 0))
            {
                return false;
            }
        }
        return true;
    }
    private void PercentSetting()
    {
        List<int> nambers = new List<int>();
        for (int i = 0; i < 100; i++)
        {
            nambers.Add(i);
        }
        int quantity = 0;
        while (quantity < _percentageOfFireInTheUpperRoom)
        {
            int i = nambers[Random.Range(0, nambers.Count)];
            _percentage[i] = true;
            nambers.Remove(i);
            quantity++;
        }
    }
    public void NextNumber(int numberFloor)
    {
        _numberFloor = numberFloor;
    }
}
