using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]private Enums.GameState gameState;

    private PlayerController player;

    #region ���Ӽ���

    [Title("���Ӽ���")]
    [FoldoutGroup("�÷��̾� ����")]
    public float startHealth;

    #endregion

    #region ������Ƽ
    public Enums.GameState GameState => gameState;
    public PlayerController Player => player;
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

    public void SetPlayer(PlayerController newPlayer)
    {
        player = newPlayer;
    }

    public void SetGameState(Enums.GameState newState)
    {
        switch(newState)
        {
            case Enums.GameState.BeforePlaying:
                break;
            case Enums.GameState.Playing:
                if (player != null)
                    player.Init();

                GameCanvasManager.instance.OnGameStart();
                break;
            case Enums.GameState.End:
                break;
        }

        gameState = newState;
    }

    public void SetGameState(int newState)
    {
        SetGameState((Enums.GameState)newState);
    }
}
