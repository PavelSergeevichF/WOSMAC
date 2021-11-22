using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParameters : MonoBehaviour, IItemParameters
{
    public string name = " ";
    public float mass=0;
    public int sizeX =1;
    public int sizeY =1;
    public GameObject gameObject;
    void Awake()
    {
        if (gameObject.GetComponent<Rigidbody>().mass <= 0)
        {
            throw new System.Exception();
        }
        mass = gameObject.GetComponent<Rigidbody>().mass;
    }
}
