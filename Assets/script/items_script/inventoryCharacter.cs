using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryCharacter : MonoBehaviour
{
    List<GameObject> item;
    float massInventory = 0;
    int occupied_volume=0;
    int fullVolume = 100;

    private void Update()
    {
        listener();
    }
    void listener()
    {
        if (EventGetItem.GetObjRNow)
        {
            if (fullVolume < occupied_volume + EventGetItem.sizeVolume)
            { }
            else
            {
                Debug.Log("++");
                item.Add(EventGetItem.GObject);
                massInventory += EventGetItem.mass;
                InventoryList.AddItem(EventGetItem.GObject.GetComponent<ItemParameters>().name);
                if (occupied_volume < occupied_volume + EventGetItem.sizeVolume + 1)
                { EventGetItem.FullInventary = true; }
            }
            EventGetItem.GetObjRNow = false;
        }
    }
    public void ShowInventory()
    {
        
    }
}
