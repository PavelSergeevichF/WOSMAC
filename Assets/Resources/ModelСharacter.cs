using System;
using System.Collections.Generic;
using UnityEngine;

public class ModelСharacter
{
    public List<GameObject> item;
    public float speed=5;
    public float strong;
    public int HP;
    private int HPMax=100;
    public System.Collections.Generic.Dictionary<int,int> reputationList = new System.Collections.Generic.Dictionary<int, int>();
    public System.Collections.Generic.Dictionary<int, int> frendOrEnemyList = new System.Collections.Generic.Dictionary<int, int>();
    public int fullVolume = 100;
    public int occupied_volume = 0;
    public void SetHp()
    {
        HP = HPMax;
    }
    public void ChangeHp(int DataHp)
    {
        HP += DataHp;
    }
    public void SetMaxHp(int newMaxHp)
    {
        HPMax = newMaxHp;
    }
    public int GetMaxHp()
    {
        return HPMax;
    }
}
