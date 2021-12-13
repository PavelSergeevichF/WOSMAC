using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

public class ViewСharacter : MonoBehaviour
{
    [SerializeField] private GameObject PointOfViewGameObject;
    [SerializeField] private GameObject PlayerCharater;
    [SerializeField] private SaveAndLoad saveAndLoad;
    [SerializeField] int timeEffect = 0;
    float tempMod = 0;
    [SerializeField] private float speed;
    public event Action<Vector3> SavePlayerPosEvent;
    public event Action<float> SpeedModPlayerEvent;
    public event Action<GameObject> GetGameObject;
    public event Action<int> ChangeHpEvent;
    private ControllerСharacter _controllerСharacter;
    private ModelСharacter _modelСharacter;
    [SerializeField] public PanelParametrCharater panelParametrCharater;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private int _sizeInventoryX = 5, _sizeInventoryY = 5;
    public string InfoText = "";
    public bool ChekTargetObject = false;
    [SerializeField] private float _distance = 5.0f;
    [SerializeField] private EventGetItem _eventGetItem;


    void Start()
    {
        _modelСharacter = new ModelСharacter();
        _controllerСharacter = new ControllerСharacter(PlayerCharater.GetComponent<ViewСharacter>(), _modelСharacter);
        panelParametrCharater = FindObjectOfType<PanelParametrCharater>();
        _controllerСharacter.EnableEvents();
        _controllerСharacter.StartControllerСharacter();
        tempMod = _controllerСharacter.speedModifier;
        //_Camera = Camera.main;
        saveAndLoad = UnityEngine.Object.FindObjectOfType<SaveAndLoad>();
    }
    // Update is called once per frame
    private void Update()
    {
        ViewTarget();
        GetItems();
        UseItems();
        Effect();
        SaveAndLoad();
        if (Input.GetKey("m"))
        {
            Damag(10);
        }
        if (Input.GetKey("n"))
        {
            healing(5);
        }
    }

    private void SaveAndLoad()
    { 
        if(Input.GetKey(KeyCode.F5))//Save
        {
            SavePosEvent(PlayerCharater.transform.position);
        }
        if (Input.GetKey(KeyCode.F6))//Load
        {
            Debug.Log("Load");
            saveAndLoad.LoadData();
        }
    }
    public void Damag(int dmg)
    {
        dmg = -dmg;
        ChangeHpEvent?.Invoke(dmg);
    }
    public void healing(int heal)
    {
        ChangeHpEvent?.Invoke(heal);
    }
    void SavePosEvent(Vector3 pos)
    {
        SavePlayerPosEvent?.Invoke(pos);
    }
    void ModSpeed(float mod)
    {
        SpeedModPlayerEvent?.Invoke(mod);
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
                if (hit.collider.gameObject.GetComponent<Flask>())
                {
                    if (hit.collider.name == "smalSpeedFlask")
                    {
                        tempMod = _controllerСharacter.speedModifier;
                        timeEffect = hit.collider.gameObject.GetComponent<Flask>().timeOfAction;
                        ModSpeed(hit.collider.gameObject.GetComponent<Flask>().Speed);
                        Debug.Log("hit.collider.gameObject.GetComponent<FlaskScript>().modSpid=" +
                            hit.collider.gameObject.GetComponent<Flask>().Speed);
                        speed = hit.collider.gameObject.GetComponent<Flask>().Speed;
                        PlayerCharater.GetComponent<FirstPersonController>().speedModifier = _controllerСharacter.speedModifier;
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
            if (_controllerСharacter.speedModifier != tempMod)
            {
                ModSpeed(tempMod);
                PlayerCharater.GetComponent<FirstPersonController>().speedModifier = tempMod;
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
                    GameObject GO = hit.collider.gameObject;
                    Debug.Log("GO=" + GO);
                    GetEventTakeItem(GO);
                    //GetGameObject?.Invoke(GO);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
    void GetEventTakeItem(GameObject GO)
    { 
        GetGameObject?.Invoke(GO);
    }


    void ViewTarget(out RaycastHit hit)
    {
        Vector3 position = PointOfViewGameObject.transform.position;
        Ray ray = new Ray(position, PointOfViewGameObject.transform.forward);
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
        Ray ray = new Ray(PointOfViewGameObject.transform.position, PointOfViewGameObject.transform.forward);
        Physics.Raycast(ray, out hit, _distance);
        if (hit.collider != null)
        {
            ChekTargetObject = true;
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
    //public void Reputation(int id,out int Reputation)
    //{
    //    //_controllerСharacter.ChekcReputation(id, out Reputation);
    //}
}
