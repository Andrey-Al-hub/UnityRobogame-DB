using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private float moveSpeed;
    private float moveHorizontal;

    [SerializeField] private bool FacingRight = true;
    

    //SpriteRenderer sr;
    // Jumping:
    /*
    private float jumpForce;
    private bool isJumping;
    private float moveVertical;
    */

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        //sr = GetComponent<SpriteRenderer>();
        // Jumping:
        /*
        jumpForce = 25f;
        isJumping = false;
        */

        // œŒƒÕﬂ“»≈
        //pickUp = gameObject.GetComponent<PickUp>();
        //pickUp.Direction = new Vector2(0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
            //sr.flipX = moveDirection < 0 ? true : false; 

            // ÔÓ‚ÓÓÚ
            if (moveHorizontal < 0 && FacingRight)
            {
                Flip();
            }
            else if (moveHorizontal > 0 && !FacingRight)
            {
                Flip();
            }
        }


        // Jumping:
        /*
        moveVertical = Input.GetAxisRaw("Vertical");
        */


    }

    void FixedUpdate()
    {
        if(moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal, 0), ForceMode2D.Impulse);
        }

        // Jumping:
        /*
        if (!isJumping && moveVertical > 0.1f)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
        */
    }

    private void Flip()
    {
        FacingRight = !FacingRight;
        UnityEngine.Transform ray = transform.GetChild(0);
        UnityEngine.Transform gun = transform.GetChild(1);
        transform.DetachChildren();
        ray.SetParent(transform);
        Vector3 mainScale = transform.localScale;
        mainScale.x *= -1;
        transform.localScale = mainScale;
        gun.SetParent(transform);
    }
    // Jumping:
    /*
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }
    */
}
