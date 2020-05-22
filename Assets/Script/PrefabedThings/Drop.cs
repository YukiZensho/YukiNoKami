using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public UISUTS c;
    void Start()
    {
        c = GameObject.Find("_SUTS").GetComponent<UISUTS>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Inventory>().Gettr(new Vector2(gameObject.GetComponent<Thing>().TheThing,1));
        Destroy(gameObject);
    }
    void Update()
    {
        if (c.DropRotationg)
        {
            transform.Rotate(1, 0, 0);
        }
    }
}
