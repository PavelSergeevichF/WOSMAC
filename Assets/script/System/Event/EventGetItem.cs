using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGetItem : MonoBehaviour
{
    public float mass;
    public float sizeX, sizeY;
    public float sizeVolume = 0;
    public GameObject GObject;
    public bool GetObjectRightNow=false;
    public bool FullInventary = false;
    //private float sharpness = 1;

    public void SetParameters(float massObj, float sizeXObj, float sizeYObj, GameObject GObj)
    {
        mass = massObj;
        sizeX = sizeXObj;
        sizeY = sizeYObj;
        GObject = GObj;
        sizeVolume = sizeX * sizeY;
    }
    public void GetParameters(out float massObj, out float sizeVol, out GameObject GObj)
    {
        massObj=mass;
        sizeVol = sizeVolume;
        GObj=GObject;
    }
}
