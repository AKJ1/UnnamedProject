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

        public int AllowedLinks;

        public List<TransmissionNode> LinkedNodes;
        public void OnInvasion(Player.Player invader, TransmissionNode from)
        {
            if (Owner == null)
            {
                GetComponent<SpriteRenderer>().color = invader.Color;
            }
        }

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
    }
}