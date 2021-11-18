using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoScript : MonoBehaviour
{
    public Text InfoText;
    [SerializeField] string info;
    My—haracter MChar;
    int time = 30;
    public GameObject PanelInfo;

    private void Start()
    {
        MChar= GameObject.Find("FPSController").GetComponent<My—haracter>();
    }
    private void Update()
    {
        ShowInfo();
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
