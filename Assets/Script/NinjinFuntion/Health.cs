using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Inventory Inv;

    public int  i;
    public float HP, SP;
    public int[] Ar/*Armour*/;
    public Image[] Hearts, Melons, Armour;
    public Sprite[] HeartLook, MelonLook, NoItemArm;
    public int SPM /*Saturation points minimizer*/;
    private void Start()
    {
        Heartts();
    }
    public void Hunger()
    { // Melons
        if (SP > 32)
            SP = 32;
        for (i = 0; i < 16; i++)
            Melons[i].sprite = MelonLook[0];
        for (i = 0; i < (int)(SP / 2); i++)
            Melons[i].sprite = MelonLook[2];
        if (Mathf.FloorToInt(SP) % 2 == 1)
        {
            Melons[Mathf.FloorToInt(SP) / 2].sprite = MelonLook[1];
        }
    }
    public void Heartts()
    {
        // Hearts
        for (i = 0; i < 8; i++)
            Hearts[i].sprite = HeartLook[0];
        for (i = 0; i < (int)HP / 2; i++)
            Hearts[i].sprite = HeartLook[2];
        if (Mathf.FloorToInt(HP) % 2 == 1)
            Hearts[(int)HP / 2].sprite = HeartLook[1];
    }
    public void Hurting(float count)
    {

        for (int i = 0; i < 5; i++)
        {
            if (Ar[i] > 90)
            {
                if (Ar[i] - (94 + i * 4) == 1)
                    HP -= count / 8;
                if (Ar[i] - (94 + i * 4) == 2)
                    HP -= count / 12;
                if (Ar[i] - (94 + i * 4) == 3)
                    HP -= count / 16;
                if (Ar[i] - (94 + i * 4) == 4)
                    HP -= count / 20;
            }
            else
                HP -= count / 5;
        }
        if (HP > 16) HP = 16;
        if (HP < 0) HP = 0;
            Heartts();
    }
    public void Feed(int t)
    {
        if (t == 24||t==40||t==48||t==61||t==65)
            SP += 1;
        if (t == 62||t==63||t==64||t==65)
            SP += 2;
        if (t == 32||t==66||t==68)
            SP += 4;
        if (t == 67||t==69)
            SP += 8;
        Hunger();
    }
    public void HungCh(int n)
    {
        SP += n;
    }
    void Update()
    {
        SP -= 0.0000001f;
        Hunger();
        for (int i = 0; i < 5; i++)
            Armour[i].enabled = !GameObject.Find("_SUTS").GetComponent<UISUTS>().advanced;

        for (i=0;i<5;i++)
            if(Ar[i]>94+(i*4)&& Ar[i] < 99 + (i * 4))
            Armour[i].sprite = Inv.items[Ar[i]];
            else
            {
                Armour[i].sprite = NoItemArm[i];
            }
        
        
    }
}
