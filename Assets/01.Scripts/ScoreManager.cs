using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Title("�����")]
    [ReadOnly] [SerializeField] private int earnedMoney = 0;
    [ReadOnly] [SerializeField] private float runDistance = 0f;

    #region ������Ƽ
    public int EarnedMoney => earnedMoney;
    public float RunDistance => runDistance;
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddMoney(int value)
    {
        earnedMoney += value;
    }

    public void AddRunDistance(float value)
    {
        runDistance += value;
    }
}
