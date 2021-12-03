using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryCharacter : MonoBehaviour
{
    List<GameObject> item;
    float massInventory = 0;
    int occupied_volume=0;
    int fullVolume = 100;
    [SerializeField] private InventoryList _inventoryList;
    [SerializeField] private EventGetItem _eventGetItem;

   private void Awake()
    {
        _inventoryList = Object.FindObjectOfType<InventoryList>();
        _eventGetItem  = Object.FindObjectOfType<EventGetItem>();
    }

    private void Update()
    {
        listener();
    }
    void listener()
    {
        if (_eventGetItem.GetObjectRightNow)
        {
            if (fullVolume < occupied_volume + _eventGetItem.sizeVolume)
            {
                Debug.Log("Инвентарь полон");
                //реализовать информационное окно о полном инвентаре
            }
            else
            {
                Debug.Log("++");
                item.Add(_eventGetItem.GObject);
                massInventory += _eventGetItem.mass;
                _inventoryList.AddItem(_eventGetItem.GObject.GetComponent<ItemParameters>().name);
                if (occupied_volume < occupied_volume + _eventGetItem.sizeVolume + 1)
                { _eventGetItem.FullInventary = true; }
            }
            _eventGetItem.GetObjectRightNow = false;
        }
    }
    public void ShowInventory()
    {
        
    }
}
