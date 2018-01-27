using System;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionNode : MonoBehaviour
{
    public int Health = 5;

    public PlayerEntity Controller;

    public int AllowedLinks;

    public List<TransmissionNode> LinkedNodes;

    public void Link(TransmissionNode other)
    {
        if (LinkedNodes == null)
        {
            LinkedNodes = new List<TransmissionNode>();
        }
        if (!LinkedNodes.Contains(other))
        {
            LinkedNodes.Add(other);
        }
    } 

    public bool IsLinkedTo(TransmissionNode other)
    {
        if (LinkedNodes.Contains(other))
        {
            return true;
        }
        return false;
    }

    public event Action OnControllerChanged;

    public void GetOwned(PlayerEntity entity)
    {
        Controller = entity;
        OnOnControllerChanged();
    }
    
    public void Initialize()
    {
        Map.Instance.RegisterNode(this);
    }

    protected virtual void OnOnControllerChanged()
    {
        OnControllerChanged?.Invoke();
    }
}