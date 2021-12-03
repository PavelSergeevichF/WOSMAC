using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
public class ControllerСharacter 
{
    public ModelСharacter modelСharacter;
    public float speedModifier = 1;



    private void Start()
    {
        modelСharacter = new ModelСharacter();
    }

    public float GetSpeed()
    {
        return modelСharacter.speed;
    }
    public void SetSpeedModifier(float InData)
    {
        speedModifier=InData;
    }
}
