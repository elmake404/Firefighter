using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Static
    public static int NamberStage, MaximumNumberOfDeadInhabitants;
    public static bool IsStartGame, IsWinGame, IsLoseGame;

    public static Vector3 BottomLeftLimit, TopRightLimit;
    #endregion
    private void Awake()
    {
        NamberStage = 0;
        IsWinGame = false;
        IsStartGame = false;
        IsLoseGame = false;
    }
    private void Start()
    {

    }

    void FixedUpdate()
    {
        if (MaximumNumberOfDeadInhabitants <= 0 && !IsLoseGame)
        {
            IsLoseGame = true;
            IsStartGame = false;
        }
    }
}
