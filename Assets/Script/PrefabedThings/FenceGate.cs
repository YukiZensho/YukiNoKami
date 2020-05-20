using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceGate : MonoBehaviour
{
    public Sprite[] LooksOn,LooksOff;
    public Collider2D col;
    public bool Open;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == GameObject.Find("Ninjin"))
        {
            Open = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if
          (collision.gameObject == GameObject.Find("Ninjin"))
        {
            Open = false;
        }
    }
    private void Update()
    {
        if(Open)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = LooksOn[gameObject.GetComponent<Thing>().TheThing - 53];
            col.enabled = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = LooksOff[gameObject.GetComponent<Thing>().TheThing - 53];
            col.enabled = true;
        }
    }
}
