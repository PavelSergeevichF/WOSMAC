using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventGetItem
{
    public static float mass;
    public static float sizeX, sizeY;
    public static float sizeVolume = 0;
    public static GameObject GObject;
    public static bool GetObjRNow=false;
    public static bool FullInventary = false;
    //private float sharpness = 1;

    public static void SetParameters(float massObj, float sizeXObj, float sizeYObj, GameObject GObj)
    {
        mass = massObj;
        sizeX = sizeXObj;
        sizeY = sizeYObj;
        GObject = GObj;
        sizeVolume = sizeX * sizeY;
    }
    public static void GetParameters(out float massObj, out float sizeVol, out GameObject GObj)
    {
        massObj=mass;
        sizeVol = sizeVolume;
        GObj=GObject;
    }
}
