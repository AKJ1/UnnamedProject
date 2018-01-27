using System.Collections.Generic;
using UnityEngine;

    public class GameManager : MonoSingleton<GameManager>
    { 
        public List<Player> ActivePlayers;

        public List<PlayerEntity> ActiveEntities;

        public void RegisterPlayer(Player player)
        {
            if (ActivePlayers == null)
            {
                ActivePlayers = new List<Player>();
                ActiveEntities = new List<PlayerEntity>();
            }
            ActivePlayers.Add(player);
            ActiveEntities.Add(player.Entity);
        }

        public void BeginGame()
        {
            
        }

        public void EndGame()
        {
            
        }
    }
