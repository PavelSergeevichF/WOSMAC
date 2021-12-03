using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ViewСharacter : MonoBehaviour
{
    private ControllerСharacter controllerСharacter;
    
    // Start is called before the first frame update
    void Start()
    {
        controllerСharacter = new ControllerСharacter();
    }

    // Update is called once per frame
    void Update()
    {
        controllerСharacter.UpdateSent();
    }
    private void FixedUpdate()
    {
        controllerСharacter.FixedUpdateSent();
    }
}
