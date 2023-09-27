using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyBehaviour : MonoBehaviour
{
    int HP = 3;
    [SerializeField] private float moveSpeed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ThrownRed")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag != "Platform")
        {
            HP--;
        }
        if (HP == 0)
        {
            Destroy(gameObject);
        }
    }
}
