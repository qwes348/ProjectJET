using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int moneyAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            if (ScoreManager.instance == null)
                return;

            ScoreManager.instance.AddMoney(moneyAmount);

            gameObject.SetActive(false);
        }
    }
}
