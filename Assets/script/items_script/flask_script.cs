using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flask_script : MonoBehaviour, IItemProperties
{
    public string description;
    public int timeOfAction;
    public float modSpid;
    public bool Activ = true;
    private void Start()
    {
        timeOfAction *= 30;
    }
}
