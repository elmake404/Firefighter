using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
#region Static
    public static bool IsStartGame,IsBurning,IsWinGame;
    public static Vector3 BottomLeftLimit, TopRightLimit;
#endregion
    private void Awake()
    {
        IsWinGame = false;
        IsBurning = false;
        IsStartGame = false;
    }
    private void Start()
    {

    }

    void Update()
    {
        //Debug.Log();
    }
}
