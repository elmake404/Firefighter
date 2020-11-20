using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Static
    public static int NamberStage;
    public static bool IsStartGame,IsWinGame;

    public static Vector3 BottomLeftLimit, TopRightLimit;
#endregion
    private void Awake()
    {
        NamberStage = 0;
        IsWinGame = false;
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
