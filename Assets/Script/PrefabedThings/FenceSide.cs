using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceSide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (gameObject.name == "l")
                gameObject.transform.parent.GetComponent<Fence>().L = true;
            else
                gameObject.transform.parent.GetComponent<Fence>().R = true;
    }
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name != "BlockPointer")
        {
            if (gameObject.name == "l")
                gameObject.transform.parent.GetComponent<Fence>().L = true;
            else
                gameObject.transform.parent.GetComponent<Fence>().R = true;
        }
    }*/
    private void OnTriggerExit2D(Collider2D collision)
    {
            if (gameObject.name == "l")
                gameObject.transform.parent.GetComponent<Fence>().L = false;
            else
                gameObject.transform.parent.GetComponent<Fence>().R = false;
    }
}
