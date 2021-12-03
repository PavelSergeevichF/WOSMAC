using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

public class ViewСharacter : MonoBehaviour
{

    [SerializeField] int timeEffect = 0;
    float tempMod = 0;
    private ControllerСharacter controllerСharacter;
    [SerializeField] private FirstPersonController firstPersonController;
    private Camera _Camera;
    private CharacterController m_CharacterController;
    [SerializeField] private int _sizeInventoryX = 5, _sizeInventoryY = 5;
    public string InfoText = "";
    public bool ChekTargetObject = false;
    public Vector2 targetObject;
    [SerializeField] private float _distance = 500.0f;
    [SerializeField] GameObject CanvasDisplay;
    [SerializeField] inventoryCharacter inventoryCharacter;
    [SerializeField] private EventGetItem _eventGetItem;

    // Start is called before the first frame update
    void Start()
    {
        controllerСharacter = new ControllerСharacter();
        tempMod = controllerСharacter.speedModifier;
        _Camera = Camera.main;
        targetObject.x = CanvasDisplay.GetComponent<RectTransform>().position.x;
        targetObject.y = CanvasDisplay.GetComponent<RectTransform>().position.y;
    }
    // Update is called once per frame
    private void Update()
    {
        ViewTarget();
        GetItems();
        UseItems();
        Effect();
    }


    void UseItems()
    {
        if (Input.GetKey("g"))
        {
            RaycastHit hit;
            //Закладка под взаимодействие с объектам
            ViewTarget(out hit);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<FlaskScript>())
                {
                    if (hit.collider.name == "smalSpeedFlask")
                    {
                        tempMod = controllerСharacter.speedModifier;
                        timeEffect = hit.collider.gameObject.GetComponent<FlaskScript>().timeOfAction;
                        controllerСharacter.SetSpeedModifier(hit.collider.gameObject.GetComponent<FlaskScript>().modSpid);
                        firstPersonController.speedModifier = controllerСharacter.speedModifier;
                    }
                }
                if (hit.collider.gameObject.GetComponent<CanTake>())
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
    void Effect()
    {
        if (timeEffect > 0)
        {
            timeEffect--;
        }
        else
        {
            if (controllerСharacter.speedModifier != tempMod)
            {
                controllerСharacter.SetSpeedModifier(tempMod);
                firstPersonController.speedModifier = tempMod;
            }
        } 
    }
    void GetItems()
    {
        if (Input.GetKey("f"))
        {
            RaycastHit hit;
            ViewTarget(out hit);
            if (hit.collider != null)
            {
                if (
                    hit.collider.gameObject.GetComponent<CanTake>() &&
                    !_eventGetItem.FullInventary
                    )
                {
                    //EventGetItem
                    _eventGetItem.GetObjectRightNow = true;
                    _eventGetItem.SetParameters
                        (
                            hit.collider.GetComponent<ItemParameters>().mass,
                            hit.collider.GetComponent<ItemParameters>().sizeX,
                            hit.collider.GetComponent<ItemParameters>().sizeY,
                            hit.collider.gameObject
                        );
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }


    void ViewTarget(out RaycastHit hit)
    {
        Ray ray = _Camera.ScreenPointToRay(targetObject);
        Physics.Raycast(ray, out hit, _distance);
        if (hit.collider != null)
        {
            ChekTargetObject = true;
            //InfoText = hit.collider.name;
            if (hit.collider.GetComponent<ItemParameters>())
                InfoText = hit.collider.GetComponent<ItemParameters>().name;
            else
                InfoText = hit.collider.name;
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
        else
        {
            InfoText = "";
            ChekTargetObject = false;
        }
    }
    void ViewTarget()
    {
        RaycastHit hit;
        Ray ray = _Camera.ScreenPointToRay(targetObject);
        Physics.Raycast(ray, out hit, _distance);
        if (hit.collider != null)
        {
            ChekTargetObject = true;
            //InfoText = hit.collider.name;
            if (hit.collider.GetComponent<ItemParameters>())
                InfoText = hit.collider.GetComponent<ItemParameters>().name;
            else
                InfoText = hit.collider.name;
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
        else
        {
            InfoText = "";
            ChekTargetObject = false;
        }
    }
}
