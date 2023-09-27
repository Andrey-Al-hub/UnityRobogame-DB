using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private float moveSpeed;
    private float moveHorizontal;
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        if (transform.localScale.x < 0)
        {
            moveSpeed *= -1;
        }
        moveHorizontal = moveSpeed;
    }

    void FixedUpdate()
    {
        rb2D.AddForce(new Vector2(moveHorizontal, 0), ForceMode2D.Impulse);
    }

    private void OnDestroy()
    {
        ItemThrown.killedEnemies += 1;
    }
}
