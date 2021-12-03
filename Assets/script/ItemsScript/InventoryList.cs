using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryList : MonoBehaviour
{
    public string inventoryListText;
    public void AddItem(string name)
    {
        inventoryListText += "/n" + name;
    }
    public void RemovItem(string name)
    {
        if (inventoryListText.Contains(name))
        {
            string tmp = "/n" + name;
            int indexIn = inventoryListText.IndexOf(name);
            if (indexIn > 3) inventoryListText.Replace(tmp, "");
            else inventoryListText.Replace(name, "");
        }    
    }
}
