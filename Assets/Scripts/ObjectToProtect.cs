using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ObjectToProtect : MonoBehaviour
{
    public GameManager gameManager;
    int HP = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Destroy(collision.gameObject);
            --HP;
            if (Score.SCORE > 100) Score.SCORE -= 100;
            else if (Score.SCORE > 0 && Score.SCORE <= 100) Score.SCORE = 0;
            if (HP == 0)
            {
                gameManager.GameOver();
            }
        }
    }
}
