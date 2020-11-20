using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsonist : MonoBehaviour
{
    public static Arsonist ArsonistMain;
    [System.Serializable]
    private struct Floor
    {
        public Stage Stageobj;
        public List<BurningRoom> NoBurningRooms;
        [HideInInspector]
        public int ArsonisNamber;
    }
    private Dictionary<int, List<BurningRoom>> _arsonDictionary = new Dictionary<int, List<BurningRoom>>();
    [SerializeField]
    private Floor[] _floors;
    [SerializeField]
    private Lift _lift;

    [SerializeField]
    private bool[] _percentage = new bool[100];
    [SerializeField]
    private float _timeBeforeArson;
    [SerializeField]
    private int _arsonisNamber;
    [SerializeField]
    [Range(1, 100)]
    private int _percentageOfFireInTheUpperRoom = 20;
    private void Awake()
    {
        ArsonistMain = this;
        PercentSetting();
        InitializationFloor();
    }
    private void Start()
    {
        //ActivationLift();
    }

    private IEnumerator ActivationArsonis()
    {
        yield return new WaitForSeconds(_timeBeforeArson);

        for (int i = 0; i < _arsonDictionary[LevelManager.NamberStage].Count; i++)
        {
            _arsonDictionary[LevelManager.NamberStage][i].ActivationFair();
            yield return new WaitForSeconds(_timeBeforeArson);
        }
    }
    [ContextMenu("SearchBurningRoom")]
    private void SearchBurningRoom()
    {
        for (int i = 0; i < _floors.Length; i++)
        {
            _floors[i].NoBurningRooms = new List<BurningRoom>();
            _lift.AddTargets(_floors[i].Stageobj.CenterFloor);

            for (int j = 0; j < _floors[i].Stageobj.transform.childCount; j++)
            {
                BurningRoom room = _floors[i].Stageobj.transform.GetChild(j).GetComponent<BurningRoom>();
                if (room != null)
                    _floors[i].NoBurningRooms.Add(room);
            }
        }
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
    private BurningRoom RandomRoom(int namberFloor)
    {
        int count = _floors[namberFloor].NoBurningRooms.Count;
        BurningRoom room = _floors[namberFloor].NoBurningRooms[Random.Range(0, count)];
        _floors[namberFloor].NoBurningRooms.Remove(room);
        return room;
    }
    private void InitializationFloor()
    {
        int number = _arsonisNamber;
        int MaxNumber = 0;

        for (int i = 0; i < _floors.Length; i++)
        {
            _floors[i].ArsonisNamber = 1;
            _arsonDictionary[i] = new List<BurningRoom>();
            _arsonDictionary[i].Add(RandomRoom(i));
            MaxNumber += _floors[i].NoBurningRooms.Count;
            number--;
        }

        number = number <= MaxNumber ? number : MaxNumber;

        while (number > 0)
        {
            int i = Random.Range(0, _floors.Length);
            if (_floors[i].NoBurningRooms.Count > 0)
            {
                _arsonDictionary[i].Add(RandomRoom(i));
                _floors[i].ArsonisNamber++;
                number--;
            }
        }

        for (int i = 0; i < _floors.Length; i++)
        {
            _floors[i].Stageobj.Initialization(this, i, _floors[i].ArsonisNamber);
        }
        _floors[_floors.Length - 1].Stageobj.IsLast = true;
    }
    public void ActivationLift()
    {
        _lift.NewTargetMoving();
        StartCoroutine(ActivationArsonis());
    }
}
