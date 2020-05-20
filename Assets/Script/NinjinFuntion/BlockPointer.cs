using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPointer : MonoBehaviour
{
    public CameraMovement CM;
    public Inventory Inv;
    public GameObject Ninjin,Corner,touched;
    public bool Touching,bp;
    private void Start()
    {
        Cursor.visible = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            Touching = true;
            Inv.BTouching = true;
            touched = collision.gameObject;
            if(collision.gameObject.GetComponent<Thing>() as Thing) 
            bp = collision.gameObject.GetComponent<Thing>().col.enabled;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            Touching = false;
            Inv.BTouching = false;
            touched = null;
            bp = false;
    }
    void Update()
    {
        if (Inv.Item[Inv.InvSelected].y > 0&& Inv.Item[Inv.InvSelected].x > -1)
            gameObject.GetComponent<SpriteRenderer>().sprite = Inv.items[(int)Inv.Item[Inv.InvSelected].x];
        else gameObject.GetComponent<SpriteRenderer>().sprite = Inv.items[11];
        if (Touching && !Inv.PapyrusOpen)
        {
            
            if(Input.GetMouseButtonDown(0))
            {
                Inv.SlotTouched = -1;
                //Debug.Log("a");
                if (touched.GetComponent<Thing>() as Thing)
                {
                    //Debug.Log("r");
                    touched.GetComponent<Thing>().Hit();
                    if (touched.GetComponent<Thing>().BS >= touched.GetComponent<Thing>().BN)
                    {
                        touched.GetComponent<Thing>().Destroying();
                    }
                }
                else if(touched.transform.parent.GetComponent<Thing>() as Thing)
                {

                    touched.transform.parent.GetComponent<Thing>().Hit();
                    if (touched.transform.parent.GetComponent<Thing>().BS >= touched.transform.parent.GetComponent<Thing>().BN)
                    {
                        Destroy(touched.transform.parent.gameObject);
                    }
                }
            }
            else if(Input.GetMouseButtonDown(1))
            {
                Inv.SlotTouched = -1;
                //Debug.Log("a");
                if (touched.GetComponent<Thing>() as Thing)
                {
                    if (touched.GetComponent<Thing>().TheThing == 137)
                        Ninjin.GetComponent<Inventory>().BenchStart();
                    else if ((touched.GetComponent<Thing>().TheThing == 22 || touched.GetComponent<Thing>().TheThing == 30 || touched.GetComponent<Thing>().TheThing == 38 || touched.GetComponent<Thing>().TheThing == 46)||(touched.GetComponent<Thing>().TheThing > 52 && touched.GetComponent<Thing>().TheThing < 61))
                        touched.GetComponent<FenceGate>().Open = !touched.GetComponent<FenceGate>().Open;
                }
                else if (touched.transform.parent.GetComponent<Thing>() as Thing)
                {
                    if(touched.transform.parent.GetComponent<Thing>().TheThing>11&&touched.transform.parent.GetComponent<Thing>().TheThing<17)
                    {
                        touched.transform.parent.GetComponent<Tree>().Fruit();
                    }
                }
            }
            else if(Input.GetKeyDown("x"))
            {
                Inv.SlotTouched = -1;
                if (touched.GetComponent<Thing>() as Thing)
                    touched.GetComponent<SpriteRenderer>().flipX = !touched.GetComponent<SpriteRenderer>().flipX;
            }
            else if(Input.GetKeyDown("z"))
            {
                Inv.SlotTouched = -1;
                if (touched.GetComponent<Thing>() as Thing)
                    touched.GetComponent<SpriteRenderer>().flipY = !touched.GetComponent<SpriteRenderer>().flipY;
            }
        }
        Vector3 mousePosition = Input.mousePosition,TargetPos;

        //Convert the mousePosition according to World position
        TargetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
        gameObject.transform.position = new Vector3((TargetPos.x > 0) ? (int)(TargetPos.x + 0.5f) : (TargetPos.x < 0) ? (int)(TargetPos.x - 0.5f) : (int)(TargetPos.x), (TargetPos.y > 0) ? (int)(TargetPos.y + 0.5f) : (TargetPos.y < 0) ? (int)(TargetPos.y - 0.5f) : (int)(TargetPos.y));
        
        /*
        float x, y;
        x = (Input.mousePosition.x) / 80;
        x += (Input.mousePosition.x> (Screen.width / 2) + 30) ? 0 : (Input.mousePosition.x < (Screen.width / 2) - 30) ? -1 : 0;
        y = (Input.mousePosition.y) / 80;
        y += (Input.mousePosition.y > (Screen.height / 2) + 30) ? 1 : (Input.mousePosition.y < (Screen.height / 2) - 30) ? -1 : 0;
        transform.position = new Vector3((Input.mousePosition.x < (Screen.width / 2) - 30 || (Input.mousePosition.x > (Screen.width / 2) + 30)) ? (int)((Corner.transform.position.x) + x) : (int)Ninjin.transform.position.x, (Input.mousePosition.y < (Screen.height / 2) - 30 || (Input.mousePosition.y > (Screen.height / 2) + 30)) ? (int)((Corner.transform.position.y) + y) : (int)Ninjin.transform.position.y);
    */
    }
}
