using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchCraftingActivator : MonoBehaviour
{
    /*
    private void OnMouseOver()
    {
        Debug.Log("Bench");
        if (Input.GetMouseButton(1))
        {
            GameObject.Find("Ninjin").GetComponent<Inventory>().BenchStart();
        }
    }*/
    Ray ray;
    RaycastHit hit;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject.Find("Ninjin").GetComponent<Inventory>().BenchStart();
            Debug.Log(hit.collider.name);
        }
    }
}
