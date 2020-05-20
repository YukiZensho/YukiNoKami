using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Thing t;
    public Sprite[] saplings,treez,leavesLess,leavesFull;
    public GameObject leaves,updater;
    public Collider2D sapling, log;
    public float nMax,chMax;
    public float start,actime,deltat,chtime,chstart;
    public bool fruited,grown,next;
    void Start()
    {
        if(start==0)
        start = Time.time;
        updater = GameObject.Find("Main Camera");
    }
    public void Destroying()
    {


        // gameObject.GetComponent<Thing>().Inv.Gettr(new Vector2(gameObject.GetComponent<Thing>().TheThing, 1));
        if (grown)
        {
            gameObject.GetComponent<Thing>().Inv.Gettr(new Vector2(gameObject.GetComponent<Thing>().TheThing, 1));
            gameObject.GetComponent<Thing>().Inv.Gettr(new Vector2(((gameObject.GetComponent<Thing>().TheThing - 13) * 8 + 17), 1));
            gameObject.GetComponent<Thing>().Inv.Gettr(new Vector2(((gameObject.GetComponent<Thing>().TheThing - 13) * 8 + 23), 2));
            if (fruited)
                gameObject.GetComponent<Thing>().Inv.Gettr(new Vector2((gameObject.GetComponent<Thing>().TheThing + 11 + 7 * (gameObject.GetComponent<Thing>().TheThing - 13)), 1));
        }
    }
    public void Fruit()
    {
        if(fruited)
        {
            gameObject.GetComponent<Thing>().Inv.Gettr(new Vector2((gameObject.GetComponent<Thing>().TheThing +11 +7*(gameObject.GetComponent<Thing>().TheThing-13)), 1));
            chstart = Time.time;
            fruited = false;
        }
    }
    public void growth()
    {
        {
            if (t.TheThing > 11 && t.TheThing < 17)
            {
                actime = Time.time;
                deltat = actime - start;
                if (deltat < nMax)
                {
                    // n++;
                }
                else if (chstart == 0)
                {
                    grown = true;
                    chstart = Time.time;
                }
                if(chstart !=0)
                    grown = true;
                if (grown)
                {
                    chtime = Time.time - chstart;
                    if (chtime >= chMax)
                    {
                        fruited = true;
                    }
                }
            }
        }
    }
    private void Update()
    {
        next = false;
        if (Mathf.Abs(updater.transform.position.x - gameObject.transform.position.x) < 25 && Mathf.Abs(updater.transform.position.y - gameObject.transform.position.y) < 25)
        {
            next = true;
            growth();
            sapling.enabled = !grown;
            log.enabled = grown;
            leaves.SetActive(grown);
            if (grown)
            {
                GetComponent<Thing>().BN = 30;
                gameObject.GetComponent<SpriteRenderer>().sprite = treez[t.TheThing - 13];
                if(fruited)
                    leaves.GetComponent<SpriteRenderer>().sprite = leavesFull[t.TheThing - 13];
                else
                    leaves.GetComponent<SpriteRenderer>().sprite = leavesLess[t.TheThing - 13];

            }
        }
    }
}
