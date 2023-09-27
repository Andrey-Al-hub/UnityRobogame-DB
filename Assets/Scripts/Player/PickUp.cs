using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    public static bool isItemHold = false;
    [SerializeField]
    protected static string holdingItemTag;

    [SerializeField]
    private Transform grabPosition;
    [SerializeField]
    private GameObject[] holdItems;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKey(KeyCode.E) && !isItemHold)
            {
                if (collider.tag == "PickableRed")
                {
                    PickItem(holdItems[0], collider);
                }
                if (collider.tag == "PickableGreen")
                {
                    PickItem(holdItems[1], collider);
                }
                if (collider.tag == "PickableBlue")
                {
                    PickItem(holdItems[2], collider);
                }
            }
        }
    }
 
    void PickItem(GameObject itemType, Collider2D collider)
    {
        GameObject item = Instantiate(itemType, grabPosition.transform.position, Quaternion.identity);
        item.transform.SetParent(grabPosition.transform);
        holdingItemTag = item.tag;
        isItemHold = true;
        Destroy(collider.gameObject);
    }
}
