using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : PickUp
{
    //[SerializeField] Transform projectilePrefab;
    //[SerializeField] Transform spawnPoint;
    //[SerializeField] LineRenderer lineRenderer;

    //[SerializeField] float launchForce = 1.5f;
    //[SerializeField] float trajectoryTimeStep = 0.05f;
    //[SerializeField] float trajectoryStepCount = 15;

    //Vector2 velocity, startMousePos, currentMousePos;
    public GameObject throwingItem;
    public float launchForce;
    public Transform shotPoint;

    [SerializeField]
    private GameObject[] throwingItems;

    void Update()
    {
        if (Time.timeScale != 0)
        {
            Vector2 gunPosition = transform.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - gunPosition;
            transform.right = direction;

            if (Input.GetMouseButtonUp(0) && PickUp.isItemHold)
            {
                if (PickUp.holdingItemTag == "HoldRed")
                {
                    Shoot(throwingItems[0]);
                }
                if (PickUp.holdingItemTag == "HoldGreen")
                {
                    Shoot(throwingItems[1]);
                }
                if (PickUp.holdingItemTag == "HoldBlue")
                {
                    Shoot(throwingItems[2]);
                }
                PickUp.isItemHold = false;
            }
        }
    }

    void Shoot(GameObject throwingItem)
    {
        Destroy(GameObject.FindWithTag(PickUp.holdingItemTag));
        this.throwingItem = throwingItem;
        GameObject newThrown = Instantiate(throwingItem, shotPoint.position, shotPoint.rotation);
        newThrown.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }
}
