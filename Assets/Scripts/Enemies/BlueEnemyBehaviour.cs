using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyBehaviour : MonoBehaviour
{
    int HP = 3;
    [SerializeField] private float moveSpeed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ThrownBlue")
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
