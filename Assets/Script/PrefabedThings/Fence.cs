using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    public Sprite[] FenceLooks;

    public int nande;
    public bool L, R;
    void Update()
    {
        nande = ((gameObject.GetComponent<Thing>().TheThing - 22) / 8);
        if (L&&R)
        {
            GetComponent<SpriteRenderer>().sprite = FenceLooks[nande + 12];
        }
        else if(L)
        {
            GetComponent<SpriteRenderer>().sprite = FenceLooks[nande + 4];
        }
        else if(R)
        {
            GetComponent<SpriteRenderer>().sprite = FenceLooks[nande + 8];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = FenceLooks[nande];
        }
    }
}
