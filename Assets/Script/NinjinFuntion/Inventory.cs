using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject BlockPointer, BlockPar,o;
    public GameObject Block, Tree, Door, Fence,Drop;
    public UISUTS UISUTS;
    public Image Papyrus, GhostSlot, InvBar;
    public Image[] Slots, CraftSlots, AD1, AD2 /*Aspect Diagonal*/;
    public Sprite[] numbers, items, slotOnOff;
    public Sprite ghostEmpty;
    public Health H;

    public bool PapyrusOpen, ArmorSelected;
    public int SlotTouched, GT /*GhostThing*/, GQ, inv, InvSelected, InvThing;
    public int[] CT, CQ;
    public bool Clicked, BTouching/*Block Touching*/, DTouching/*Diagonal Touching*/, Crafting, BenchCrafting;
    public bool[] C1, C2;
    public Vector2[] Item; // X is the thing ( aspect) and Y the quantity
    public void Gettr(Vector2 Thing)
    {
        //Debug.Log("Started");
        int i;
        for (i = 0; i < inv; i++)
        {
            if (Item[i].y < 64 && Item[i].x == Thing.x && Thing.y > 0)
                break;
        }
        if (!(i < inv))
        {
            for (i = 0; i < inv; i++)
            {
                if (Item[i].y == 0)
                    break;
                //Debug.Log(i);
            }
        }
        //Debug.Log("i = "+i);
        //if (Item[i].y == 0 || (Item[i].y < 64 && Item[i].x == Thing.x))
        if (i < inv)
        {
            if (Item[i].y == 0)
            {
                Item[i].x = Thing.x;
                Item[i].y = Thing.y;
            }
            else
            {
                Item[i].x = Thing.x;
                if (Thing.x == Item[i].x)
                {
                    if (Thing.y + Item[i].y <= 64)
                        Item[i].y += Thing.y;
                    else
                    {
                        int t;
                        t = (int)Item[i].y + (int)Thing.y - 64;
                        Item[i].y = 64;
                        Gettr(new Vector2(Thing.x, t));
                    }
                }
            }
        }
        else Debug.Log("Error");
    }
    public void Droppr()
    {
        if((int)Item[InvSelected].y>0)
        {
            o = Instantiate(Drop);
            Vector3 t = transform.position;
            t.x += (GetComponent<Movement>().dir==0)?2: (GetComponent<Movement>().dir == 1) ? -2 : 0;
            t.y += (GetComponent<Movement>().dir == 2) ? 2 : (GetComponent<Movement>().dir==3)?-2:0;
            o.transform.position = t;
            o.GetComponent<Thing>().TheThing = (int)Item[InvSelected].x;
            Item[InvSelected].y -= 1;
        }
    }
    public void Ghosttr()
    {
        if (SlotTouched != -1)
        {
            if (!Clicked)
            {
                //Debug.Log("Shut");
                if (ArmorSelected)
                {

                    if (Input.GetMouseButtonDown(0))
                    {
                        Clicked = true;
                        if (H.Ar[SlotTouched] == 0 && GT > 94 + (SlotTouched * 4) && GT < 99 + (SlotTouched * 4) && GQ == 1)
                        {
                            H.Ar[SlotTouched] = GT;
                            GT = -1;
                        }
                        else
                        {
                            if (GT == -1)
                            {
                                GT = H.Ar[SlotTouched];
                                GQ = 1;
                                H.Ar[SlotTouched] = 0;
                            }
                        }
                    }
                }
                else
                {

                    if (SlotTouched == 97 || SlotTouched == 98 || SlotTouched == 99)
                    {
                        if (!DTouching)
                        {
                            // Debug.Log("sa");
                            if (Input.GetMouseButtonDown(0))
                            {
                                Clicked = true;
                                Debug.Log("s");
                                if (SlotTouched == 97)
                                {
                                    if (GT != CT[0])
                                    {
                                        int t;
                                        t = GT;
                                        GT = CT[0];
                                        CT[0] = t;

                                        t = GQ;
                                        GQ = CQ[0];
                                        CQ[0] = t;
                                    }
                                    else
                                    {
                                        CQ[0] += GQ;
                                        if (CQ[0] > 64)
                                        {
                                            GQ = CQ[0] - 64;
                                            CQ[0] -= 64;
                                        }
                                        else
                                            GQ = 0;
                                    }
                                }
                                else if (SlotTouched == 98)
                                {
                                    if (GT != CT[1])
                                    {
                                        int t;
                                        t = GT;
                                        GT = CT[1];
                                        CT[1] = t;

                                        t = GQ;
                                        GQ = CQ[1];
                                        CQ[1] = t;
                                    }
                                    else
                                    {
                                        CQ[1] += GQ;
                                        if (CQ[1] > 64)
                                        {
                                            GQ = CQ[1] - 64;
                                            CQ[1] -= 64;
                                        }
                                        else
                                            GQ = 0;
                                    }
                                }
                                else if (SlotTouched == 99)
                                {
                                    if (GQ < 64)
                                    {
                                        if (GQ == 0 || GT == CT[2])
                                        {
                                            GT = CT[2];
                                            GQ += CQ[2];
                                            if (GQ > 64)
                                            {
                                                CQ[2] = GQ - 64;
                                                GQ = 64;
                                            }
                                            else
                                            {
                                                CQ[2] = 0;
                                                CT[2] = -1;
                                            }
                                            if (CQ[1] > 0)
                                                CQ[1] -= 1;
                                            if (CQ[1] == 0)
                                                CT[1] = -1;
                                            if (CQ[0] > 0)
                                                CQ[0] -= 1;
                                            if (CQ[2] == 0)
                                                CT[2] = -1;
                                        }
                                    }
                                }
                            }
                            else if (Input.GetMouseButtonDown(1))
                            {

                                Clicked = true;
                                //Debug.Log("s");
                                if (SlotTouched == 97)
                                {
                                    if (GT == CT[0])
                                    {
                                        Debug.Log("l");
                                        if (CT[0] < 64)
                                        {
                                            CQ[0] += 1;
                                            GQ -= 1;
                                        }
                                    }
                                    else if (CT[0] == -1)
                                    {
                                        CQ[0] = 1;
                                        CT[0] = GT;
                                        GQ -= 1;
                                        if (GQ == 0)
                                            GT = -1;
                                    }
                                }
                                else if (SlotTouched == 98)
                                {
                                    if (GT == CT[1])
                                    {
                                        Debug.Log("l");
                                        if (CT[1] < 64)
                                        {
                                            CQ[1] += 1;
                                            GQ -= 1;
                                        }
                                    }
                                    else if (CT[1] == -1)
                                    {
                                        CQ[1] = 1;
                                        CT[1] = GT;
                                        GQ -= 1;
                                        if (GQ == 0)
                                            GT = -1;
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            if (CT[i] != -1 && CQ[i] > 0)
                            {
                                CraftSlots[i].gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
                                CraftSlots[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = items[CT[i]];
                                CraftSlots[i].gameObject.transform.GetChild(1).GetComponent<Image>().enabled = true;
                                CraftSlots[i].gameObject.transform.GetChild(1).GetComponent<Image>().sprite = numbers[CQ[i] - 1];
                            }
                            else
                            {
                                CraftSlots[i].gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
                                CraftSlots[i].gameObject.transform.GetChild(1).GetComponent<Image>().enabled = false;
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Clicked = true;
                            //Debug.Log("Pressed");
                            if (GQ == 0 && (int)(Item[SlotTouched].y) > 0)
                            {
                                // Debug.Log("Inv");
                                GT = (int)(Item[SlotTouched].x);
                                GQ = (int)(Item[SlotTouched].y);
                                (Item[SlotTouched].y) = 0;
                            }
                            else
                            {

                                if (GQ > 0 && (int)(Item[SlotTouched].y) == 0)
                                {
                                    Debug.Log("ghost");
                                    (Item[SlotTouched].x) = GT;
                                    (Item[SlotTouched].y) = GQ;
                                    GQ = 0;
                                }
                                else
                                {
                                    if (GT == (int)(Item[SlotTouched].x))
                                    {
                                        if (GQ + (int)(Item[SlotTouched].y) <= 64)
                                        {
                                            (Item[SlotTouched].y) += GQ;
                                            GQ = 0;
                                        }
                                        else
                                        {
                                            if ((int)(Item[SlotTouched].y) <= 64)
                                                GQ = (int)(Item[SlotTouched].y) + GQ - 64;
                                            (Item[SlotTouched].y) = 64;
                                        }
                                    }
                                    else
                                    {
                                        int temp = GT;
                                        GT = (int)(Item[SlotTouched].x);
                                        (Item[SlotTouched].x) = temp;
                                        temp = GQ;
                                        GQ = (int)(Item[SlotTouched].y);
                                        (Item[SlotTouched].y) = temp;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Input.GetMouseButtonDown(1))
                            {
                                Clicked = true;
                                if (GQ == 0 || (int)(Item[SlotTouched].y) == 0)
                                {
                                    if ((int)(Item[SlotTouched].y) == 0 || ((int)(Item[SlotTouched].x) == GQ && (int)(Item[SlotTouched].y) < 64))
                                    {
                                        (Item[SlotTouched].x) = GT;
                                        (Item[SlotTouched].y)++;
                                        GQ--;
                                    }
                                    else
                                    {
                                        if (GQ == 0 && (int)(Item[SlotTouched].y) > 0)
                                        {
                                            GT = (int)(Item[SlotTouched].x);
                                            GQ++;
                                            (Item[SlotTouched].y)--;
                                        }
                                    }
                                }
                                else
                                {
                                    if (GT == (int)(Item[SlotTouched].x) && (int)(Item[SlotTouched].y) < 64)
                                    {
                                        GQ -= 1;
                                        Item[SlotTouched].y += 1;
                                    }
                                }
                            }
                            else
                            {
                                if (Input.GetMouseButtonDown(2))
                                {
                                    if (Item[SlotTouched].y > 1)
                                    {
                                        if ((int)(Item[SlotTouched].y) == 0 || ((int)(Item[SlotTouched].x) == GQ && (int)(Item[SlotTouched].y) + (GQ / 2) <= 64))
                                        {
                                            (Item[SlotTouched].x) = GT;
                                            (Item[SlotTouched].y) += GQ / 2;
                                            (Item[SlotTouched].y) += (GQ % 2 == 0) ? 0 : 1;
                                            GQ /= 2;
                                        }
                                        else
                                        {
                                            if (GQ == 0 && (int)(Item[SlotTouched].y) > 0)
                                            {
                                                GT = (int)(Item[SlotTouched].x);
                                                GQ = (int)(Item[SlotTouched].y) / 2;
                                                GQ += ((int)(Item[SlotTouched].y) % 2 == 0) ? 0 : 1;
                                                (Item[SlotTouched].y) /= 2;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    // Placer
    public void Placer(bool bp, bool moused, Vector3 blockpointerpos, int TheThing, int flipX, int flipY)//butonPrincipal
    {
        //Armour
        if (TheThing > 94 && TheThing < 115 && bp)
        {
            int n = (int)Item[InvSelected].x - 95;
            if (H.Ar[n / 4] == 0)
            {
                H.Ar[n / 4] = (int)Item[InvSelected].x;
                Item[InvSelected].y -= 1;
            }
        }
        //Food
        else if ((TheThing > 60 && bp && TheThing < 70) || TheThing == 24 || TheThing == 32 || TheThing == 40 || TheThing == 48)
        {
            if (H.SP <= 31)
            {
                H.Feed((int)Item[InvSelected].x);
                Item[InvSelected].y -= 1;
            }
        }
        //Blocks
        else if (!BTouching || moused||bp!=BlockPointer.GetComponent<BlockPointer>().bp)
        {
            if (TheThing < 73 || TheThing > 136)
            {
                if (TheThing > 12 && TheThing < 17)
                {
                    o = Instantiate(Tree);
                    if (!moused)
                    {
                        int n = Random.Range(0, 5);

                        if (n == 0)
                        {
                            o.GetComponent<Tree>().nMax = Random.Range(100, 250);
                        }
                        else
                        {
                            o.GetComponent<Tree>().nMax = o.GetComponent<Tree>().deltat + 1;
                            o.GetComponent<Tree>().chtime = Time.time + Random.Range(0f, 30f);
                        }
                    }
                }
                else if (TheThing > 52 && TheThing < 61)
                    o = Instantiate(Door);
                else if ((TheThing == 22 || TheThing == 30 || TheThing == 38 || TheThing == 46) && (!BTouching || !moused))
                    o = Instantiate(Fence);
                else
                {
                    {
                        //Debug.Log(BTouching+" "+bp + " " + BlockPointer.GetComponent<BlockPointer>().touched.GetComponent<Thing>().col.enabled);
                        o = Instantiate(Block);
                        o.GetComponent<SpriteRenderer>().flipX = (flipX == 1);
                        o.GetComponent<SpriteRenderer>().flipY = (flipY == 1);
                        if (!bp)//bp = solid
                        {
                            o.GetComponent<Collider2D>().enabled = false;
                            o.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);
                            o.GetComponent<SpriteRenderer>().sortingOrder = 6;
                        }
                    }
                }
                if (o != null)
                {
                    o.transform.position = blockpointerpos;
                    if (blockpointerpos.x > -8 && blockpointerpos.x < -4)
                        o.transform.Translate(new Vector3(-1, 0));
                    if (blockpointerpos.x == -8)
                        o.transform.position = new Vector3(-8.5f, blockpointerpos.y);
                    if (blockpointerpos.y == -16)
                        o.transform.position = new Vector3(blockpointerpos.x, -16.5f);
                    if (blockpointerpos.y == -64)
                        o.transform.position = new Vector3(blockpointerpos.x, -64.5f);
                    o.transform.parent = BlockPar.transform;
                    o.GetComponent<Thing>().TheThing = TheThing;
                    o.GetComponent<Thing>().Inv = gameObject.GetComponent<Inventory>();
                }
                if (moused)
                    Item[InvSelected].y -= 1;
            }
        }
    }
    public void BenchStart()
    {
        BenchCrafting = true;
        PapyrusOpen = true;
        SlotTouched = -1;
        for (int i = 0; i < inv; i++)
            Slots[i].sprite = slotOnOff[0];
    }
    public void CraftEndr()
    {
        if (CT[0] == -1)
        {
            for (int kk = 0; kk < 6; kk++)
            {
                C1[kk] = false;
            }
        }
        if (CT[1] == -1)
        {
            for (int kk = 0; kk < 6; kk++)
            {
                C2[kk] = false;
            }
        }
        //Basic Crafts
        if (C1[2] == false && C1[3] == false && C1[4] == false && C1[5] == false && C2[2] == false && C2[3] == false && C2[4] == false && C2[5] == false)
        {
            //Doors
            if ((CT[0] == 42 && !C1[0] && !C1[1]) && (CT[1] == 42 && !C2[0] && !C2[1])) { CT[2] = 60; CQ[2] = 2; }
            else if ((CT[0] == 34 && !C1[0] && !C1[1]) && (CT[1] == 34 && !C2[0] && !C2[1])) { CT[2] = 59; CQ[2] = 2; }
            else if ((CT[0] == 26 && !C1[0] && !C1[1]) && (CT[1] == 26 && !C2[0] && !C2[1])) { CT[2] = 58; CQ[2] = 2; }
            else if ((CT[0] == 18 && !C1[0] && !C1[1]) && (CT[1] == 18 && !C2[0] && !C2[1])) { CT[2] = 57; CQ[2] = 2; }
            //Fence
            else if (CT[0] == 18 && C1[0] && !C1[1] && CT[1] == 18 && C2[0] && !C2[1]) { CT[2] = 22; CQ[2] = 4; }
            else if (CT[0] == 26 && C1[0] && !C1[1] && CT[1] == 26 && C2[0] && !C2[1]) { CT[2] = 30; CQ[2] = 4; }
            else if (CT[0] == 34 && C1[0] && !C1[1] && CT[1] == 34 && C2[0] && !C2[1]) { CT[2] = 38; CQ[2] = 4; }
            else if (CT[0] == 42 && C1[0] && !C1[1] && CT[1] == 42 && C2[0] && !C2[1]) { CT[2] = 46; CQ[2] = 4; }
            //Gate
            else if (CT[0] == 22 && !C1[0] && !C1[1] && CT[1] == 18 && !C2[0] && !C2[1]) { CT[2] = 53; CQ[2] = 1; }
            else if (CT[0] == 30 && !C1[0] && !C1[1] && CT[1] == 26 && !C2[0] && !C2[1]) { CT[2] = 54; CQ[2] = 1; }
            else if (CT[0] == 38 && !C1[0] && !C1[1] && CT[1] == 34 && !C2[0] && !C2[1]) { CT[2] = 55; CQ[2] = 1; }
            else if (CT[0] == 46 && !C1[0] && !C1[1] && CT[1] == 42 && !C2[0] && !C2[1]) { CT[2] = 56; CQ[2] = 1; }
            //Signs
            else if ((CT[0] == 115 && !C1[0] && !C1[1]) && (CT[1] == 18 && !C2[0] && C2[1])) { CT[2] = 49; CQ[2] = 2; }
            else if ((CT[0] == 115 && !C1[0] && !C1[1]) && (CT[1] == 26 && !C2[0] && C2[1])) { CT[2] = 50; CQ[2] = 2; }
            else if ((CT[0] == 115 && !C1[0] && !C1[1]) && (CT[1] == 34 && !C2[0] && C2[1])) { CT[2] = 52; CQ[2] = 2; }
            else if ((CT[0] == 115 && !C1[0] && !C1[1]) && (CT[1] == 42 && !C2[0] && C2[1])) { CT[2] = 51; CQ[2] = 2; }
            else if ((CT[0] == 18 && !C1[0] && C1[1]) && (CT[1] == 115 && !C2[0] && !C2[1])) { CT[2] = 49; CQ[2] = 2; }
            else if ((CT[0] == 26 && !C1[0] && C1[1]) && (CT[1] == 115 && !C2[0] && !C2[1])) { CT[2] = 50; CQ[2] = 2; }
            else if ((CT[0] == 34 && !C1[0] && C1[1]) && (CT[1] == 115 && !C2[0] && !C2[1])) { CT[2] = 52; CQ[2] = 2; }
            else if ((CT[0] == 42 && !C1[0] && C1[1]) && (CT[1] == 115 && !C2[0] && !C2[1])) { CT[2] = 51; CQ[2] = 2; }
            //Vert Slab
            else if ((CT[0] == 42 && C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 45; CQ[2] = 2; }
            else if ((CT[0] == 34 && C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 37; CQ[2] = 2; }
            else if ((CT[0] == 26 && C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 29; CQ[2] = 2; }
            else if ((CT[0] == 18 && C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 21; CQ[2] = 2; }
            //Slab to Plank
            else if (((CT[0] == 20 || CT[0] == 21) && !C1[0] && !C1[1]) && ((CT[1] == 20 || CT[1] == 21) && !C2[0] && !C2[1])) { CT[2] = 18; CQ[2] = 1; }
            else if (((CT[0] == 28 || CT[0] == 29) && !C1[0] && !C1[1]) && ((CT[1] == 28 || CT[1] == 29) && !C2[0] && !C2[1])) { CT[2] = 26; CQ[2] = 1; }
            else if (((CT[0] == 36 || CT[0] == 37) && !C1[0] && !C1[1]) && ((CT[1] == 36 || CT[1] == 37) && !C2[0] && !C2[1])) { CT[2] = 34; CQ[2] = 1; }
            else if (((CT[0] == 44 || CT[0] == 45) && !C1[0] && !C1[1]) && ((CT[1] == 44 || CT[1] == 45) && !C2[0] && !C2[1])) { CT[2] = 42; CQ[2] = 1; }
            //Horiz Slab
            else if ((CT[0] == 42 && !C1[0] && C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 44; CQ[2] = 2; }
            else if ((CT[0] == 34 && !C1[0] && C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 36; CQ[2] = 2; }
            else if ((CT[0] == 26 && !C1[0] && C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 28; CQ[2] = 2; }
            else if ((CT[0] == 18 && !C1[0] && C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 20; CQ[2] = 2; }
            //Slab Inverting
            else if ((CT[0] == 20 && !C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 21; CQ[2] = 1; }
            else if ((CT[0] == 28 && !C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 29; CQ[2] = 1; }
            else if ((CT[0] == 36 && !C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 37; CQ[2] = 1; }
            else if ((CT[0] == 44 && !C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 45; CQ[2] = 1; }
            else if ((CT[0] == 21 && !C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 20; CQ[2] = 1; }
            else if ((CT[0] == 29 && !C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 28; CQ[2] = 1; }
            else if ((CT[0] == 37 && !C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 36; CQ[2] = 1; }
            else if ((CT[0] == 45 && !C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 44; CQ[2] = 1; }
            //Axe
            else if ((CT[0] == 42 || CT[0] == 18 || CT[0] == 26 || CT[0] == 34) && !C1[0] && !C1[1] && CT[1] == 115 && !C2[0] && !C2[1]) { CT[2] = 77; CQ[2] = 1; }
            else if ((CT[0] == 2 && !C1[0] && !C1[1]) && (CT[1] == 115 && !C2[0] && !C2[1])) { CT[2] = 78; CQ[2] = 1; }
            //Stone Axe
            else if ((CT[0] == 42 || CT[0] == 18 || CT[0] == 26 || CT[0] == 34) && !C1[0] && !C1[1] && CT[1] == 116 && !C2[0] && !C2[1]) { CT[2] = 81; CQ[2] = 1; }
            else if ((CT[0] == 2 && !C1[0] && !C1[1]) && (CT[1] == 116 && !C2[0] && !C2[1])) { CT[2] = 82; CQ[2] = 1; }
            //PickAxe
            else if ((CT[0] == 21 || CT[0] == 29 || CT[0] == 37 || CT[0] == 45 || CT[0] == 20 || CT[0] == 28 || CT[0] == 36 || CT[0] == 44) && !C1[0] && !C1[1] && CT[1] == 115 && !C2[0] && !C2[1]) { CT[2] = 85; CQ[2] = 1; }
            else if ((CT[0] == 2 && C1[0] != C1[1]) && (CT[1] == 115 && !C2[0] && !C2[1])) { CT[2] = 86; CQ[2] = 1; }
            //Stone PickAxe
            else if ((CT[0] == 21 || CT[0] == 29 || CT[0] == 37 || CT[0] == 45 || CT[0] == 20 || CT[0] == 28 || CT[0] == 36 || CT[0] == 44) && !C1[0] && !C1[1] && CT[1] == 116 && !C2[0] && !C2[1]) { CT[2] = 89; CQ[2] = 1; }
            else if ((CT[0] == 2 && C1[0] != C1[1]) && (CT[1] == 116 && !C2[0] && !C2[1])) { CT[2] = 90; CQ[2] = 1; }
            //Hoe
            else if ((CT[0] == 20 || CT[0] == 28 || CT[0] == 36 || CT[0] == 44 || CT[0] == 21 || CT[0] == 29 || CT[0] == 37 || CT[0] == 45) && C1[0] != C1[1] && CT[1] == 115 && !C2[0] && !C2[1]) { CT[2] = 73; CQ[2] = 1; }
            else if (CT[0] == 2 && C1[0] && C1[1] && CT[1] == 115 && !C2[0] && !C2[1]) { CT[2] = 74; CQ[2] = 1; }
            //Sticks
            else if (((CT[0] == 21 || CT[0] == 29 || CT[0] == 37 || CT[0] == 45) && C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 115; CQ[2] = 2; }
            //Crafting Table
            else if (((CT[0] == 18 || CT[0] == 26 || CT[0] == 34 || CT[0] == 42) && C1[0] && C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 137; CQ[2] = 1; }
            // StoneSticks
            else if ((CT[0] == 2 && C1[0] && !C1[1]) && (CT[1] == -1 && !C2[0] && !C2[1])) { CT[2] = 116; CQ[2] = 4; }
            //planks
            else if (CT[0] == 17 && C1[0] && C1[1] && CT[1] == -1 && !C2[0] && !C2[1]) { CT[2] = 18; CQ[2] = 4; }
            else if (CT[0] == 25 && C1[0] && C1[1] && CT[1] == -1 && !C2[0] && !C2[1]) { CT[2] = 26; CQ[2] = 4; }
            else if (CT[0] == 33 && C1[0] && C1[1] && CT[1] == -1 && !C2[0] && !C2[1]) { CT[2] = 34; CQ[2] = 4; }
            else if (CT[0] == 41 && C1[0] && C1[1] && CT[1] == -1 && !C2[0] && !C2[1]) { CT[2] = 42; CQ[2] = 4; }
            else if (CT[0] == -1 && !C1[0] && !C1[1] && CT[1] == 17 && C2[0] && C2[1]) { CT[2] = 18; CQ[2] = 4; }
            else if (CT[0] == -1 && !C1[0] && !C1[1] && CT[1] == 25 && C2[0] && C2[1]) { CT[2] = 26; CQ[2] = 4; }
            else if (CT[0] == -1 && !C1[0] && !C1[1] && CT[1] == 33 && C2[0] && C2[1]) { CT[2] = 34; CQ[2] = 4; }
            else if (CT[0] == -1 && !C1[0] && !C1[1] && CT[1] == 41 && C2[0] && C2[1]) { CT[2] = 42; CQ[2] = 4; }
            else if (CT[0] == 17 && C1[0] && C1[1] && CT[1] == 17 && C2[0] && C2[1]) { CT[2] = 18; CQ[2] = 8; }
            else if (CT[0] == 25 && C1[0] && C1[1] && CT[1] == 25 && C2[0] && C2[1]) { CT[2] = 26; CQ[2] = 8; }
            else if (CT[0] == 33 && C1[0] && C1[1] && CT[1] == 33 && C2[0] && C2[1]) { CT[2] = 34; CQ[2] = 8; }
            else if (CT[0] == 41 && C1[0] && C1[1] && CT[1] == 41 && C2[0] && C2[1]) { CT[2] = 42; CQ[2] = 8; }
            //Crafting bench
            else if ((CT[0] == 23 || CT[0] == 26 || CT[0] == 34 || CT[0] == 42) && C1[0] && C1[1] && CT[1] == -1 && !C2[1] && !C2[0]) { CT[2] = 121; CQ[2] = 1; }

            else { CT[2] = 0; CQ[2] = 0; }
        }
        //Complex
        //Stairs
        else if (CT[0] == 18 && (!C1[0] && !C1[1] &&(C1[2]!=C1[3]) && (C1[2] == C1[4]) == (C1[3] == C1[5])) && CT[1] == -1 && (!C2[0] && !C2[1] && !C2[2] && !C2[3] && !C2[4] && !C2[5])) { CT[2] = 19; CQ[2] = 2; }
        else if (CT[0] == 26 && (!C1[0] && !C1[1] &&(C1[2]!=C1[3]) && (C1[2] == C1[4]) == (C1[3] == C1[5])) && CT[1] == -1 && (!C2[0] && !C2[1] && !C2[2] && !C2[3] && !C2[4] && !C2[5])) { CT[2] = 27; CQ[2] = 2; }
        else if (CT[0] == 34 && (!C1[0] && !C1[1] &&(C1[2]!=C1[3]) && (C1[2] == C1[4]) == (C1[3] == C1[5])) && CT[1] == -1 && (!C2[0] && !C2[1] && !C2[2] && !C2[3] && !C2[4] && !C2[5])) { CT[2] = 35; CQ[2] = 2; }
        else if (CT[0] == 42 && (!C1[0] && !C1[1] &&(C1[2]!=C1[3]) && (C1[2] == C1[4]) == (C1[3] == C1[5])) && CT[1] == -1 && (!C2[0] && !C2[1] && !C2[2] && !C2[3] && !C2[4] && !C2[5])) { CT[2] = 43; CQ[2] = 2; }
        //Stone Breather
        else if (CT[0] == 2 && (!C1[0] && !C1[1] && C1[2] && C1[3] && !C1[4] && !C1[5]) && CT[1] == -1 && (!C2[0] && !C2[1] && !C2[2] && !C2[3] && !C2[4] && !C2[5])) { CT[2] = 95; CQ[2] = 1; }
        //Stone Helmet
        else if (CT[0] == 2 && (!C1[0] && !C1[1] && !C1[2] && !C1[3] && C1[4] && C1[5]) && CT[1] == -1 && (!C2[0] && !C2[1] && !C2[2] && !C2[3] && !C2[4] && !C2[5])) { CT[2] = 99; CQ[2] = 1; }
        //Stone Chestplate
        else if (CT[0] == 2 && (!C1[0] && !C1[1] && C1[2] && C1[3] && !C1[4] && !C1[5]) && CT[1] == 2 && (!C2[0] && !C2[1] && !C2[2] && !C2[3] && !C2[4] && !C2[5])) { CT[2] = 103; CQ[2] = 1; }
        //Stone pants
        else if (CT[0] == 2 && (!C1[0] && !C1[1] && !C1[2] && !C1[3] && C1[4] && C1[5]) && CT[1] == 2 && (!C2[0] && !C2[1] && !C2[2] && !C2[3] && !C2[4] && !C2[5])) { CT[2] = 107; CQ[2] = 1; }
        //Stone boots
        else if (CT[0] == 2 && (C1[0] && !C1[1] && !C1[2] && !C1[3] && C1[4] && C1[5]) && CT[1] == -1 && (!C2[0] && !C2[1] && !C2[2] && !C2[3] && !C2[4] && !C2[5])) { CT[2] = 111; CQ[2] = 1; }

        else { CT[2] = 0; CQ[2] = 0; }
    }
    void Update()
    {
        InvThing = (int)Item[InvSelected].x;
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
            Clicked = false;
        if (Input.GetKeyDown("q"))
            Droppr();
        if (Item[InvSelected].y > 0 && (Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(1)) && !PapyrusOpen)
            Placer(Input.GetMouseButtonDown(1), true, BlockPointer.transform.position, (int)Item[InvSelected].x, 0, 0);
        if (Input.GetAxisRaw("Scroll") != 0)
        {
            InvSelected += (Input.GetAxisRaw("Scroll") < -0.1) ? 1 : (Input.GetAxisRaw("Scroll") > 0.1) ? -1:0;
            if (InvSelected < 0) InvSelected = 7;
            if (InvSelected > 7) InvSelected = 0;
        }
        if (Input.GetKeyDown("1")) InvSelected = 0;
        if (Input.GetKeyDown("2")) InvSelected = 1;
        if (Input.GetKeyDown("3")) InvSelected = 2;
        if (Input.GetKeyDown("4")) InvSelected = 3;
        if (Input.GetKeyDown("5")) InvSelected = 4;
        if (Input.GetKeyDown("6")) InvSelected = 5;
        if (Input.GetKeyDown("7")) InvSelected = 6;
        if (Input.GetKeyDown("8")) InvSelected = 7;
        InvBar.rectTransform.anchoredPosition = new Vector3(50, -75 - InvSelected * 46);
        /*  if (Input.GetKeyDown("q"))
          {
              Gettr(new Vector2(12, 1));
          }
          if (Input.GetKeyDown("z"))
          {
              Item[0].x = 2;
              (Item[0].y) = 64;
          }*/
        if (Input.GetKeyDown("e"))
        {
            PapyrusOpen = !PapyrusOpen;
            BenchCrafting = false;
            if (!PapyrusOpen)
                Crafting = false;
            SlotTouched = -1;
            for (int i = 0; i < inv; i++)
                Slots[i].sprite = slotOnOff[0];
        }
        if (PapyrusOpen)
        {
            if (CQ[0] == 0)
                CT[0] = -1;
            if (CT[0] == -1)
                CQ[0] = 0;
            if (CT[1] == -1)
                CQ[1] = 0;
            if (CQ[1] == 0)
                CT[1] = -1;
            if (CT[0] < 1 && CT[1] < 1)
            {
                CT[2] = -1;
                CQ[2] = 0;
            }
            CraftEndr();
        }
        Papyrus.gameObject.SetActive(PapyrusOpen);
        if (UISUTS.paused) Papyrus.gameObject.SetActive(false);
        for (int i = 0; i < inv; i++)
        {

            if (Item[i].y == 0)
                Item[i].x = -1;
            if (Item[i].x > -1)
            {
                if (Slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().enabled == false)
                    Slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
                if (Slots[i].gameObject.transform.GetChild(1).GetComponent<Image>().enabled == false)
                    Slots[i].gameObject.transform.GetChild(1).GetComponent<Image>().enabled = true;
                Slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = items[(int)Item[i].x];
                Slots[i].gameObject.transform.GetChild(1).GetComponent<Image>().sprite = numbers[(int)Item[i].y - 1];
            }
            else
            {
                if (Slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().enabled)
                    Slots[i].gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
                if (Slots[i].gameObject.transform.GetChild(1).GetComponent<Image>().enabled)
                    Slots[i].gameObject.transform.GetChild(1).GetComponent<Image>().enabled = false;
            }
            AD1[0].enabled = AD1[1].enabled = CQ[0] > 0;
            AD2[0].enabled = AD2[1].enabled = CQ[1] > 0;
            if (BenchCrafting)
                for (int w = 2; w < 6; w++)
                {
                    AD1[w].enabled = CQ[0] > 0;
                    AD2[w].enabled = CQ[1] > 0;
                }
            else
                for (int w = 2; w < 6; w++)
                {
                    AD1[w].enabled = false;
                    AD2[w].enabled = false;
                }
            if (GQ == 0)
                GT = -1;
            GhostSlot.gameObject.SetActive(PapyrusOpen);
            if (GQ > 0 && GT != -1)
            {
                GhostSlot.rectTransform.anchoredPosition = new Vector3(Input.mousePosition.x + 20, Input.mousePosition.y - 20);
                GhostSlot.sprite = items[GT];
                GhostSlot.gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
                GhostSlot.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                GhostSlot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = numbers[GQ - 1];
            }
            else
            {
                GhostSlot.rectTransform.anchoredPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y - 35);
                GhostSlot.sprite = ghostEmpty;
                GhostSlot.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.44f, 2f, 1);
                GhostSlot.gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
            }


            if (Item[i].x == -1)
                Item[i].y = 0;
        }


        Ghosttr();
    }
}
//This Includes any crafting
