using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryList 
{
    public static string inventory_list_text;
    public static void AddItem(string name)
    {
        inventory_list_text += "/n" + name;
    }
    public static void RemovItem(string name)
    {
        if (inventory_list_text.Contains(name))
        {
            string tmp = "/n" + name;
            int indexIn = inventory_list_text.IndexOf(name);
            if (indexIn > 3) inventory_list_text.Replace(tmp, "");
            else inventory_list_text.Replace(name, "");
        }    
    }
}
