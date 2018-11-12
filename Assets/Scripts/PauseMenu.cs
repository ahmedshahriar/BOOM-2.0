using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PauseMenu : MonoBehaviour {

    public static bool IsOn = false;

/*   private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton;

        NetworkManager.singleton.StartMatchMaker();
        
    }*/

    public void LeaveRoom()
    {
        ////MatchInfo matchInfo = networkManager.matchInfo;
        ////networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
        ////networkManager.StopHost();

        //MatchInfo matchInfo = networkManager.matchInfo;
        //NetworkManager.singleton.StartMatchMaker();
        //NetworkManager.singleton.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);


        //  networkManager.StopClient();
        Application.Quit();
       // networkManager.StopHost();

        // Debug.Log("pause working");
    }
}
