using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftLines : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    public Inventory inv;
    public int Dc /* 'Diagonal' count */, Dp /* 'Diagonal position */;
    public bool l;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //        Debug.Log("log");
        l = true;
    } 
    public void OnPointerExit(PointerEventData eventData)
    {
        l = false;
        inv.DTouching = l;
    }
    private void Update()
    {
        if (l)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (Dp == 0)
                {
                    inv.C1[Dc] = !inv.C1[Dc];
                }
                else
                {
                    inv.C2[Dc] = !inv.C2[Dc];
                }
            }
            inv.DTouching = l;
        }
        if (Dp == 0)
        {
            gameObject.GetComponent<Image>().color = (!inv.C1[Dc]) ? new Color32(255, 255, 255, 137) : new Color32(255, 0, 0, 137);
        }
        else
        {
            gameObject.GetComponent<Image>().color = (!inv.C2[Dc]) ? new Color32(255, 255, 255, 137) : new Color32(255, 0, 0, 137);
        }

    }
}
