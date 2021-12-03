using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoScript : MonoBehaviour
{
    public Text InfoText;
    public Text InventoryText;
    [SerializeField] string info;
    View—haracter view—haracter;
    int time = 30;
    public GameObject PanelInfo;
    public GameObject CanvasDisplayInventory;
    [SerializeField] private InventoryList inventoryList;

    private void Start()
    {
        view—haracter = GameObject.Find("FPSController").GetComponent<View—haracter>();
        inventoryList = Object.FindObjectOfType<InventoryList>();
        CanvasDisplayInventory.SetActive(false);
    }
    private void Update()
    {
        ShowInfo();
        showInventory();
    }
    void showInventory()
    {
        if (Input.GetKey("i"))
        {
            CanvasDisplayInventory.SetActive(true);
            InventoryText.text = inventoryList.inventoryListText;
        }
        if(!Input.GetKey("i"))
        {
            CanvasDisplayInventory.SetActive(false);
        }
    }
    void ShowInfo()
    {

        if (view—haracter.ChekTargetObject)
        {
            PanelInfo.SetActive(true);
            InfoText.text = view—haracter.InfoText;
            if (time < 30) time = 30;
        }
        else
        {
            if (time > 30)
            {
                time--;
            }
            else
            {
                if (InfoText.text != "")
                {
                    InfoText.text = "";
                }
                if (PanelInfo.active)
                {
                    PanelInfo.SetActive(false);
                }

            }
        }
    }
}
