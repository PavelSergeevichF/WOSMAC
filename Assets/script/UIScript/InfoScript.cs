using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoScript : MonoBehaviour
{
    public Text InfoText;
    public Text InventoryText;
    [SerializeField] string info;
    My—haracter MChar;
    int time = 30;
    public GameObject PanelInfo;
    public GameObject CanvasDisplayInventory;

    private void Start()
    {
        MChar= GameObject.Find("FPSController").GetComponent<My—haracter>();
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
            InventoryText.text = InventoryList.inventory_list_text;
        }
        if(!Input.GetKey("i"))
        {
            CanvasDisplayInventory.SetActive(false);
        }
    }
    void ShowInfo()
    {
        
        if (MChar.ChekTargetObject)
        {
            PanelInfo.SetActive(true);
            InfoText.text = MChar.InfoText;
            if (time < 30) time = 30;
        }
        else
        {
            if (time > 30) time--;
            else 
            {
                InfoText.text = "";
                PanelInfo.SetActive(false);
            }
        }
    }
}
