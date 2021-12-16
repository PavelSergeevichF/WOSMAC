using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

public class ViewСharacter : MonoBehaviour
{
    [SerializeField] private GameObject PointOfViewGameObject;
    [SerializeField] public GameObject PlayerCharater;
    [SerializeField] private SaveAndLoad saveAndLoad;
    [SerializeField] int timeEffect = 0;
    private bool _ifInHand = false;
    private bool _leftHandIsBusy = false;
    private bool _rightHandIsBusy = false;
    float tempMod = 0;
    [SerializeField] private float speed;
    public event Action<Vector3> SavePlayerPosEvent;
    public event Action<float> SpeedModPlayerEvent;
    public event Action<GameObject> GetGameObject;
    public event Action<int> ChangeHpEvent;
    public event Action<bool> ExtractLastItemEvent;
    public event Action<bool> ThrowOutItemEvent;
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
        saveAndLoad = UnityEngine.Object.FindObjectOfType<SaveAndLoad>();
    }
    // Update is called once per frame
    private void Update()
    {
        ViewTarget();
        WorkingWithItem();
        Effect();
        SaveAndLoad();
        WorkingWithHP();
    }

    #region HP
    private void WorkingWithHP()
    {
        if (Input.GetKey("m"))
        {
            Damag(10);
        }
        if (Input.GetKey("n"))
        {
            healing(5);
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
    #endregion
    #region Save and Load
    private void SaveAndLoad()
    {
        if (Input.GetKey(KeyCode.F5))//Save
        {
            SavePosEvent(PlayerCharater.transform.position);
        }
        if (Input.GetKey(KeyCode.F6))//Load
        {
            Debug.Log("Load");
            saveAndLoad.LoadData();
        }
    }
    void SavePosEvent(Vector3 pos)
    {
        SavePlayerPosEvent?.Invoke(pos);
    }
    #endregion
    #region Item
    private void WorkingWithItem()
    {
        UseItem();
        GetItem();
        if (Input.GetKey("h")) ExtractLastItemEvent?.Invoke(true);
        if (Input.GetKey("j")) ThrowOutItemEvent?.Invoke(true);
    }
    public void CreatObject(GameObject goPos,GameObject goItem)
    {
        GameObject item = Instantiate(
                    goItem,
                    goPos.transform.position,
                    Quaternion.identity) as GameObject;
    }
    void GetItem()//взять предмет
    {
        if (Input.GetKey("f"))
        {
            RaycastHit hit;
            //Закладка под взаимодействие с объектам
            ViewTarget(out hit);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<SpeedSmalFlask1>())
                {
                    if (hit.collider.name == "smalSpeedFlask")
                    {
                        tempMod = _controllerСharacter.speedModifier;
                        timeEffect = hit.collider.gameObject.GetComponent<SpeedSmalFlask1>().timeOfAction;
                        ModSpeed(hit.collider.gameObject.GetComponent<SpeedSmalFlask1>().Speed);
                        speed = hit.collider.gameObject.GetComponent<SpeedSmalFlask1>().Speed;
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
    
    void UseItem()//использовать предмет
    {
        if (Input.GetKey("g"))
        {
            if(!_ifInHand)
            {
                UseAnExternalItem();
            }
        }
    }
    void UseAnExternalItem()
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
                //GameObject GO = hit.collider.gameObject;
                CopyGameObject <GameObject> copyGameObject1 = new CopyGameObject<GameObject>();
                copyGameObject1.SetGameObject(hit.collider.gameObject);
                CopyGameObject<GameObject> copyGameObject2 = (CopyGameObject<GameObject>)copyGameObject1.Clone(hit.collider.gameObject);
                GameObject GO = copyGameObject2.GetGameObjectData();
                GetGameObject?.Invoke(GO);
                Destroy(hit.collider.gameObject);
                Debug.Log(copyGameObject2.GetGameObjectData());
            }
        }
    }
    void GetEventTakeItem(GameObject GO)
    { 
        GetGameObject?.Invoke(GO);
    }
    #endregion
    void ModSpeed(float mod)
    {
        SpeedModPlayerEvent?.Invoke(mod);
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
public class CopyGameObject<T>
{
    GameObject GO;
    public CopyGameObject()
    { }
    public CopyGameObject(GameObject getGO)
    { GO = getGO; }
    public void SetGameObject (GameObject GetGameObject)
    {
        GO = GetGameObject;
    }
    public GameObject GetGameObjectData()
    {
        return GO;
    }
    public object Clone(GameObject getGO)
    {
        return new CopyGameObject<T>(getGO);
    }
}
