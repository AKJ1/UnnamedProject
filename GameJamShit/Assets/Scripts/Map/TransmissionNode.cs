using System;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class TransmissionNode : NetworkBehaviour
{
    public Player Owner;
    public Queue<NewsEntity> NewsEntities;
    public event UnityAction OnIvnsionDoneEffect;
    

    public void OnInvasion(Player invader, TransmissionNode from)
    {
        if (Owner == null)
        {
            GetComponent<SpriteRenderer>().color = invader.Color;
        }

        
    }
}