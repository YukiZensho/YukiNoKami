using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotButton : MonoBehaviour , IPointerEnterHandler,IPointerDownHandler , IPointerExitHandler
{
    public int pos;
    public Inventory Thory;
    public void Settr()
    {
        if (pos < 100)
        {
//            Debug.Log("lol");
            Thory.ArmorSelected = false;
            Thory.SlotTouched = pos;
            
        }
        else
        {
            Thory.ArmorSelected = true;
            Thory.SlotTouched = pos-100;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Thory.PapyrusOpen)
        {
            Settr();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Thory.PapyrusOpen)
        {
            Settr();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Thory.ArmorSelected = false;
        Thory.SlotTouched = -1;
    }
    private void Update()
    {
        if (pos < 100)
        {
            if (Thory.SlotTouched == pos)
            {
                gameObject.GetComponent<Image>().sprite = Thory.slotOnOff[1];
            }
            else gameObject.GetComponent<Image>().sprite = Thory.slotOnOff[0];
            if(Thory.DTouching)
                gameObject.GetComponent<Image>().sprite = Thory.slotOnOff[0];
        }

    }
}