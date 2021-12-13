using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewNPSCharacter : MonoBehaviour
{
    public int ID = 0;
    [SerializeField] private GameObject gameObject;
    [SerializeField] private GameObject baseIdGameObject;
    [SerializeField] private float _distance = 10.0f;
    [SerializeField] private GameObject PointOfViewGameObject;
    [SerializeField] private int reputationOfInterlocutor = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetId();
    }

    // Update is called once per frame
    void Update()
    {
        ViewNPS();
    }
    private void SetId()
    {
        //реализовать модель Id персонажей и через преаф сохранить коллекцию
        if (ID == 0)
        {
            if (baseIdGameObject.GetComponent<BaseID>().baseIdDictonary.Count < 1)
            {
                baseIdGameObject.GetComponent<BaseID>().baseIdDictonary.Add(1, gameObject.name);
                ID = 1;
                baseIdGameObject.GetComponent<BaseID>().SaveDictionary();
            }
            else
            {
                baseIdGameObject.GetComponent<BaseID>().baseIdDictonary.Add
                    (
                    baseIdGameObject.GetComponent<BaseID>().baseIdDictonary.Count,
                    gameObject.name
                    );
                ID = baseIdGameObject.GetComponent<BaseID>().baseIdDictonary.Count - 1;
                baseIdGameObject.GetComponent<BaseID>().SaveDictionary();
            }
        }

    }
    private void ViewNPS()
    {
        RaycastHit hit;
        ViewTarget(out hit);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<ViewСharacter>())
            {
                //hit.collider.GetComponent<ViewСharacter>().Reputation(ID, out reputationOfInterlocutor);
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
            Debug.DrawLine(ray.origin, hit.point, Color.blue);
        }
    }

}
