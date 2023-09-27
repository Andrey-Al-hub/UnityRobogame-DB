using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemThrown : MonoBehaviour
{
    [SerializeField]
    private GameObject changeOnDropped;

    private bool touchedEnemy = false;
    public static int killedEnemies = 0;
    Rigidbody2D rb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            touchedEnemy = true;
        }
        if (collision.collider.name == "Ground")
        {
            if (!touchedEnemy)
            {
                GameObject dropped = Instantiate(changeOnDropped, transform.position, transform.rotation);
                dropped.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            }
            else
            {
                if (killedEnemies == 1)
                {
                    Score.SCORE += 50;
                }
                if (killedEnemies == 2)
                {
                    Score.SCORE += 200;
                }
                if (killedEnemies >= 3)
                {
                    Score.SCORE += 200 * killedEnemies;
                }
            }
            killedEnemies = 0;
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
