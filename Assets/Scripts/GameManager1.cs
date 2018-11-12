using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameManager1: MonoBehaviour {

    public static GameManager1 instance;

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    private void Awake()
    {
        if (instance!=null)
        {
            Debug.LogError("More than one gamemanager" + transform.name);
        }
        else
        {
            instance = this;
        }
    }

    public static Player GetPlayer(string playerID)
    {
        if (!players.ContainsKey(playerID))
        {


            Debug.Log("Error Player Id " + playerID);

            return null;
        }
        return players[playerID];
    }







    public static void RegisterPlayer(string networkId,Player player)
    {
        string playerId = "Player" + networkId;
        players.Add(playerId,player);
        player.transform.name = playerId;
    }


    public static void UnregisterPlayer(string playerId)
    {
        players.Remove(playerId);
    }




    //public static Player Getplayer(string playerId)
    //{
    //    return players[playerId];
    //}
}
