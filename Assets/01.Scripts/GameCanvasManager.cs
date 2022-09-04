using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class GameCanvasManager : MonoBehaviour
{
    public static GameCanvasManager instance;

    [Title("Panels")]
    [SerializeField]
    private GameObject gameStartPanel;
    [SerializeField]
    private GameObject gameHUDPanel;

    [Title("UI")]
    [SerializeField]
    private Text playerHPText;   

    private void Awake()
    {
        instance = this;
    }

    public void UpdatePlayerHPText(float value)
    {
        var newValue = Mathf.Floor(value * 10f) / 10f;
        playerHPText.text = newValue.ToString();
    }

    public void OnGameStart()
    {
        gameStartPanel.SetActive(false);
        gameHUDPanel.SetActive(true);
    }
}
