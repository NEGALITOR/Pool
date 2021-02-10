using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror
{
    public class OnlineWork : NetworkManager
    {
        //public Mirror.NetworkManager NM;
        public Transform pOne;
        public Transform pTwo;
        public GameObject player;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            Transform start = numPlayers == 0 ? pOne : pTwo;
            player = Instantiate(playerPrefab, start.position, start.rotation);
            NetworkServer.AddPlayerForConnection(conn, player);
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }


    }
}