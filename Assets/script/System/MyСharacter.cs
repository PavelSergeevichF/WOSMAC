using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My—haracter : MonoBehaviour
{
    private float power;
    private int HP;
    [SerializeField] int timeEffect = 0;
    float tempMod = 0;
    public bool ChekTargetObject=false;
    public string InfoText = "";
    public Vector2 targetObject;
    [SerializeField] private float _distance = 500.0f;
    private Camera _camera;
    public —harProperty ÒharProperty;

    private void Start()
    {
        _camera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
        tempMod = ÒharProperty.speed_modifier;
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
                if (hit.collider.gameObject.GetComponent<flask_script>())
                {
                    Debug.Log("***");
                    if (hit.collider.name == "smalSpeedFlask")
                    {
                        Debug.Log("++");
                        tempMod = ÒharProperty.speed_modifier;
                        timeEffect = hit.collider.gameObject.GetComponent<flask_script>().timeOfAction;
                        ÒharProperty.speed_modifier = hit.collider.gameObject.GetComponent<flask_script>().modSpid;
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
                if (hit.collider.gameObject.GetComponent<CanTake>())
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
    void ItemAction(out bool Action, out RaycastHit hit)
    {
        Action = false;
        ViewTarget(out hit);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<CanTake>())
            {
                Destroy(hit.collider.gameObject);
                Action = false;
            }
            else
                Action = true;

        }
    }
   
    void ViewTarget(out RaycastHit hit)
    {
        Ray ray = _camera.ScreenPointToRay(targetObject);
        Physics.Raycast(ray, out hit, _distance);
        if (hit.collider != null)
        {
            ChekTargetObject = true;
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