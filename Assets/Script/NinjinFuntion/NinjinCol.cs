using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjinCol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("LoL");
        if (other.gameObject.GetComponent<Thing>() as Thing != null)
        {
            if (gameObject.transform.position.y > other.transform.position.y)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
            else
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;

        }
        if (other.tag == "Leaves")
        {
            GameObject g = other.transform.GetComponentInParent<Tree>().gameObject;
            if (other.GetComponent<Tree>() != null)
            {
                if(other.GetComponent<Tree>().fruited)
                    {
                    other.GetComponent<Tree>().chtime = Time.time;
                    gameObject.GetComponent<Inventory>().Gettr(new Vector2((other.GetComponent<Thing>().TheThing-13)*7+11,1));
                }
            }
        }
    }
}
