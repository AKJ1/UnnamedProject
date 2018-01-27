using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Assets.Scripts.Map
{
    public class TransmissionNode : NetworkBehaviour
    {
        public Player.Player Owner;
        public Queue<NewsEntity> NewsEntities;
        public event UnityAction OnIvnsionDoneEffect;
    
        public void OnInvasion(Player.Player invader, TransmissionNode from)
        {
            if (Owner == null)
            {
                GetComponent<SpriteRenderer>().color = invader.Color;
            }
        }
    }
}