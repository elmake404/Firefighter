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

    //[SerializeField]
    //private bool[] _percentage = new bool[100];
    [SerializeField]
    private float _timeBeforeArson;
    [SerializeField]
    private int _arsonisNamber, _stupidInhabitants;
    [SerializeField]
    [Range(1, 100)]
    private int _percentageOfFireInTheUpperRoom = 20;
    private void Awake()
    {
        ArsonistMain = this;
        //PercentSetting();
    }
    private void Start()
    {
        InitializationFloor();
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
    //private void PercentSetting()
    //{
    //    List<int> nambers = new List<int>();
    //    for (int i = 0; i < 100; i++)
    //    {
    //        nambers.Add(i);
    //    }
    //    int quantity = 0;
    //    while (quantity < _percentageOfFireInTheUpperRoom)
    //    {
    //        int i = nambers[Random.Range(0, nambers.Count)];
    //        _percentage[i] = true;
    //        nambers.Remove(i);
    //        quantity++;
    //    }
    //}
    private BurningRoom RandomRoom(int namberFloor)
    {
        int count = _floors[namberFloor].NoBurningRooms.Count;
        BurningRoom room = _floors[namberFloor].NoBurningRooms[Random.Range(0, count)];
        _floors[namberFloor].NoBurningRooms.Remove(room);
        return room;
    }
    private void InitializationFloor()
    {
        int arsonisNumber = _arsonisNamber>= _floors.Length? _arsonisNamber: _floors.Length;

        int stupidInhabitants = _stupidInhabitants;
        int MaxNumber = 0;
        int MaxNamberStupid = 0;

        for (int i = 0; i < _floors.Length; i++)
        {
            _floors[i].ArsonisNamber = 1;
            _arsonDictionary[i] = new List<BurningRoom>();
            _arsonDictionary[i].Add(RandomRoom(i));
            MaxNumber += _floors[i].NoBurningRooms.Count;
            MaxNamberStupid ++;
            arsonisNumber--;
        }

        arsonisNumber = arsonisNumber <= MaxNumber ? arsonisNumber : MaxNumber;

        MaxNamberStupid += arsonisNumber;

        stupidInhabitants = stupidInhabitants <= MaxNamberStupid ? stupidInhabitants : MaxNamberStupid;
        //
        CanvasManager.CanvasManagerMain.InitializationStars(stupidInhabitants);

        while (arsonisNumber > 0)
        {
            int i = Random.Range(0, _floors.Length);
            if (_floors[i].NoBurningRooms.Count > 0)
            {
                _arsonDictionary[i].Add(RandomRoom(i));
                _floors[i].ArsonisNamber++;
                arsonisNumber--;
            }
        }
        List<BurningRoom> selectedInhabitants = new List<BurningRoom>();

        while (stupidInhabitants > 0)
        {
            int i = Random.Range(0, _arsonDictionary.Count);
            int j = Random.Range(0, _arsonDictionary[i].Count);

            if (!selectedInhabitants.Contains(_arsonDictionary[i][j]))
            {
                _arsonDictionary[i][j].IsThereIsAResident = true;
                selectedInhabitants.Add(_arsonDictionary[i][j]);
                //Debug.Log(i + " " + j);
                stupidInhabitants--;
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
