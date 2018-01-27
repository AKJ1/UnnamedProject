using System;
using UnityEngine;

public class TransmissionNode : MonoBehaviour
{
    public int Health = 5;

    public PlayerEntity Controller;

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