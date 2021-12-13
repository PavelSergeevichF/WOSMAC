using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
public class ControllerСharacter 
{
    public float speedModifier = 3;
    private ViewСharacter _viewСharacter;
    private ModelСharacter _modelСharacter;
    private float HpRatio;
    private float EnduranceRatio;
    private float MannaRatio;


    public ControllerСharacter(ViewСharacter view, ModelСharacter model)
    {
        _viewСharacter = view;
        _modelСharacter = model;
    }

    public void StartControllerСharacter()
    {
        SetHp();
        SetDataHpPanel();
    }
    public void EnableEvents()
    {
        _viewСharacter.SavePlayerPosEvent += Save;
        _viewСharacter.SpeedModPlayerEvent += SetSpeedModifier;
        _viewСharacter.GetGameObject += TakeGameItem;
        _viewСharacter.ChangeHpEvent += ChangeHp;
    }

    public float GetSpeed()
    {
        return _modelСharacter.speed;
    }
    #region HP
    void SetHp()
    {
        _modelСharacter.SetHp();
    }
    void ChangeHp(int changeHp)
    {
        _modelСharacter.ChangeHp(changeHp);
        SetDataHpPanel();
    }
    void SetDataHpPanel()
    {
        int tmp = _modelСharacter.GetMaxHp();
        HpRatio = 1 / (float)tmp;
        string HpStr = _modelСharacter.HP.ToString();
        HpStr += "%";
        float sizeHpImage = (float)_modelСharacter.HP * HpRatio;
        _viewСharacter.panelParametrCharater.GetComponent<PanelParametrCharater>().HpImage.fillAmount = sizeHpImage;
        _viewСharacter.panelParametrCharater.GetComponent<PanelParametrCharater>().HpText.text = HpStr;
    }
    #endregion
    #region Endurance

    #endregion
    #region Item
    private void TakeGameItem(GameObject gameObject)
    {
        if (_modelСharacter.fullVolume < _modelСharacter.occupied_volume + gameObject.GetComponent<ItemParameters>().sizeVolume)
        {
            Debug.Log("Инвентарь полон");
            //реализовать информационное окно о полном инвентаре
        }
        else
        {
            Debug.Log("GameObject=" + gameObject);
            _modelСharacter.item.Add(gameObject);
            _modelСharacter.occupied_volume += gameObject.GetComponent<ItemParameters>().sizeVolume;
        }
    }
    #endregion
    private void SetSpeedModifier(float InData)
    {
        speedModifier=InData;
    }
    #region SaveAndLoad
    public void Save(Vector3 Pos)
    {
        Debug.Log("Save");
    }
    public void Load()
    {

    }
    #endregion
    #region frendOrEnemy
    public void SetFrend(int id)
    {
        if (!_modelСharacter.frendOrEnemyList.ContainsKey(id))
        {
            _modelСharacter.frendOrEnemyList.Add(id, 1);
        }
        else 
        {
            _modelСharacter.frendOrEnemyList[id] = 1;
        }
        
    }
    public void removeFrend(int id)
    {
        if (_modelСharacter.frendOrEnemyList.ContainsKey(id))
        {
            _modelСharacter.frendOrEnemyList[id] = 0;
        }
        else 
        {
            Debug.Log("Данного ID нет в базе друзей!");
            _modelСharacter.frendOrEnemyList[id] = 0;//программа упадет
        }
    }
    public void SetEnemy(int id)
    {
        if (!_modelСharacter.frendOrEnemyList.ContainsKey(id))
        {
            _modelСharacter.frendOrEnemyList.Add(id, -1);
        }
        else
        {
            _modelСharacter.frendOrEnemyList[id] = -1;
        }
    }
    public void removeEnemy(int id)
    {
        if (_modelСharacter.frendOrEnemyList.ContainsKey(id))
        {
            _modelСharacter.frendOrEnemyList[id] = 0;
        }
        else
        {
            Debug.Log("Данного ID нет в базе друзей!");
            _modelСharacter.frendOrEnemyList[id] = 0;//программа упадет
        }
    }
    #endregion
    #region Reputation
    public void ChekcReputation(int id,out int Reputation)
    {
        if (!_modelСharacter.reputationList.ContainsKey(id))
        {
            SetReputation(id);
        }
        Reputation = GetReputation(id);
    }
    public int GetReputation(int id)
    {
        int reputation = 0;
        if(!_modelСharacter.reputationList.ContainsKey(id))
        {
            SetReputation(id);
        }
        reputation = _modelСharacter.reputationList[id];
        return reputation;
    }
    public void SetReputation(int id)
    {
        if (!_modelСharacter.reputationList.ContainsKey(id))
        {
            _modelСharacter.reputationList.Add(id, 0);
        }
        else
        {
            Debug.Log("Репутация уже есть! Равна " + _modelСharacter.reputationList[id]);
        }

    }
    public void SetReputation(int id, int Reputation)
    {
        if (!_modelСharacter.reputationList.ContainsKey(id))
        {
            _modelСharacter.reputationList.Add(id, Reputation);
        }
        else
        {
            Debug.Log("Репутация уже есть! Равна " + _modelСharacter.reputationList[id]);
        }

    }
    public void UpReputation(int id, int Up)
    {
        if (_modelСharacter.reputationList.ContainsKey(id))
        {
            _modelСharacter.reputationList[id] += Up;
        }
        else
        {
            Debug.Log("Данного ID нет в базе репутации!");
            SetReputation(id, Up);
        }
    }
    public void DownReputation(int id, int Down)
    {
        if (_modelСharacter.reputationList.ContainsKey(id))
        {
            _modelСharacter.reputationList[id] -= Down;
        }
        else
        {
            Debug.Log("Данного ID нет в базе репутации!");
            SetReputation(id, -Down);
        }
    }
    #endregion

}
