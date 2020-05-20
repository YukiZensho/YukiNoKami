using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour
{
    public GameObject par;
    public Inventory Inv;
    public Collider2D col;
    public int BS /*Broke State*/ , BN /*BrokeNeeded*/,TheThing;
    private void Start()
    {
        if (gameObject.GetComponent<Tree>() as Tree != null)
        {
            par = GameObject.Find("Plants");
        }
        else
        {
            par = GameObject.Find("BlockLocation");
        }
        transform.SetParent(par.transform);
        transform.position = new Vector3((int)transform.position.x,(int)transform.position.y);
        Inv = GameObject.Find("Ninjin").GetComponent<Inventory>();
        StartCoroutine(Starter());
    }
    public void Hit()
    {
        if (gameObject.GetComponent<Tree>() as Tree || gameObject.GetComponentInParent<Tree>() as Tree)
        {
            if ((int)(Inv.Item[Inv.InvSelected].x) >= 77 && (int)(Inv.Item[Inv.InvSelected].x) <= 84 || (int)(Inv.Item[Inv.InvSelected].x) == 93 || (int)(Inv.Item[Inv.InvSelected].x) == 94)
            {
                if (Inv.InvThing == 77 || Inv.InvThing == 81)
                    BS += 1;
                if (Inv.InvThing == 78 || Inv.InvThing == 82)
                    BS += 2;
                if (Inv.InvThing == 80 || Inv.InvThing == 84)
                    BS += 3;
                if (Inv.InvThing == 79 || Inv.InvThing == 83||Inv.InvThing == 93 || Inv.InvThing == 94)
                    BS += 4;
            }
        }
        BS+=1;
        Debug.Log(BS+"/"+BN);
    }
    public IEnumerator Starter()
    {
        while(TheThing == -1)
        yield return new WaitForSecondsRealtime(0.016f);
        gameObject.GetComponent<SpriteRenderer>().sprite = Inv.items[TheThing];
    }
    public void Destroying()
    {
        Inv.Gettr(new Vector2(TheThing, 1));
        if (gameObject.GetComponent<Tree>() as Tree)
            gameObject.GetComponent<Tree>().Destroying();
        for(int i=0;i<gameObject.transform.childCount;i++)
        Destroy(gameObject.transform.GetChild(i).gameObject);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (Mathf.Abs(Inv.gameObject.transform.position.x - gameObject.transform.position.x) < 3 && Mathf.Abs(Inv.gameObject.transform.position.y - gameObject.transform.position.y) < 3&&col.enabled)
            if (Inv.gameObject.transform.position.y > gameObject.transform.position.y)
                GetComponent<SpriteRenderer>().sortingOrder = 12;
            else
                GetComponent<SpriteRenderer>().sortingOrder = 8;
        if (Mathf.Abs((Inv.gameObject.transform.position.x/32) - (gameObject.transform.position.x/32)) > 2 || Mathf.Abs((Inv.gameObject.transform.position.y/32) - (gameObject.transform.position.y/32)) > 2)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
                Destroy(gameObject.transform.GetChild(i).gameObject);
            Destroy(gameObject);
        }
    }
}
