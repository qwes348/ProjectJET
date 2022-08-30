using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]private Enums.GameState gameState;

    #region 프로퍼티
    public Enums.GameState GameState => gameState;
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        SetGameState(Enums.GameState.BeforePlaying);
    }

    public void SetGameState(Enums.GameState newState)
    {
        switch(newState)
        {
            case Enums.GameState.BeforePlaying:
                break;
            case Enums.GameState.Playing:
                break;
            case Enums.GameState.End:
                break;
        }
    }
}
