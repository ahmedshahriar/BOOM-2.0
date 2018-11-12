using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class NetworkLobbyHook :LobbyHook{

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        //  base.OnLobbyServerSceneLoadedForPlayer(manager, lobbyPlayer, gamePlayer);
       // LobbyPlayer lobby =lobbyPlayer.GetComponent<LobbyPlayer>();
        
    }
}
