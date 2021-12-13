using UnityEngine;

public class ControllerNPC 
{
    public ModelNPC modelNPC;
    public float speedModifier = 1;

    private void Start()
    {
        modelNPC = new ModelNPC();
    }
    #region Reputation
    public void SetReputation(int id)
    {
        if (!modelNPC.reputationList.ContainsKey(id))
        {
            modelNPC.reputationList.Add(id, 0);
        }
        else
        {
            Debug.Log("��������� ��� ����! ����� " + modelNPC.reputationList[id]);
        }

    }
    public void SetReputation(int id, int Reputation)
    {
        if (!modelNPC.reputationList.ContainsKey(id))
        {
            modelNPC.reputationList.Add(id, Reputation);
        }
        else
        {
            Debug.Log("��������� ��� ����! ����� " + modelNPC.reputationList[id]);
        }

    }
    public void UpReputation(int id, int Up)
    {
        if (modelNPC.reputationList.ContainsKey(id))
        {
            modelNPC.reputationList[id] += Up;
        }
        else
        {
            Debug.Log("������� ID ��� � ���� ���������!");
            SetReputation(id, Up);
        }
    }
    public void DownReputation(int id, int Down)
    {
        if (modelNPC.reputationList.ContainsKey(id))
        {
            modelNPC.reputationList[id] -= Down;
        }
        else
        {
            Debug.Log("������� ID ��� � ���� ���������!");
            SetReputation(id, -Down);
        }
    }
    #endregion
}
