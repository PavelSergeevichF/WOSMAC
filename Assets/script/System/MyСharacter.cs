using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My—haracter : MonoBehaviour
{
    private float power;
    private int HP;
    [SerializeField] int timeEffect = 0;
    [SerializeField] GameObject CanvasDisplay;
    float tempMod = 0;
    public bool ChekTargetObject=false;
    public string InfoText = "";
    public Vector2 targetObject;
    [SerializeField] private float _distance = 500.0f;
    private Camera _camera;
    public —harProperty ÒharProperty;
    [SerializeField] inventoryCharacter inventoryCharacter;
    [SerializeField] private int _sizeInventoryX = 5, _sizeInventoryY=5;
    [SerializeField] private EventGetItem _eventGetItem;

    private void Start()
    {
        _camera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
        tempMod = ÒharProperty.speed_modifier;
        //inventoryCharacter.sizeX = _sizeInventoryX;
        //inventoryCharacter.sizeY = _sizeInventoryY;
        targetObject.x = CanvasDisplay.GetComponent<RectTransform>().position.x;
        targetObject.y = CanvasDisplay.GetComponent<RectTransform>().position.y;
    }
    private void Update()
    {
        ViewTarget();
        GetItems();
        UseItems();
        Effect();
    }

    
    void UseItems()
    { 
        if(Input.GetKey("g"))
        {
            RaycastHit hit;
            //«‡ÍÎ‡‰Í‡ ÔÓ‰ ‚Á‡ËÏÓ‰ÂÈÒÚ‚ËÂ Ò Ó·˙ÂÍÚ‡Ï
            ViewTarget(out hit);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<FlaskScript>())
                {
                    if (hit.collider.name == "smalSpeedFlask")
                    {
                        tempMod = ÒharProperty.speed_modifier;
                        timeEffect = hit.collider.gameObject.GetComponent<FlaskScript>().timeOfAction;
                        ÒharProperty.speed_modifier = hit.collider.gameObject.GetComponent<FlaskScript>().modSpid;
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
            if (ÒharProperty.speed_modifier != tempMod) ÒharProperty.speed_modifier = tempMod;
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
                    hit.collider.gameObject.GetComponent<CanTake>()&& 
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
        Ray ray = _camera.ScreenPointToRay(targetObject);
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
        Ray ray = _camera.ScreenPointToRay(targetObject);
        Physics.Raycast(ray, out hit, _distance);
        if (hit.collider != null)
        {
            ChekTargetObject = true;
            //InfoText = hit.collider.name;
            if(hit.collider.GetComponent<ItemParameters>())
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