using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LobbyPauseMenu : MonoBehaviour {


    public static bool IsOn = false;

    [SerializeField]
    public GameObject LBM;

    public void LeaveLobby()
    {
        Application.Quit();
    }
    public void BackMain()
    {
        
        //Network.Disconnect();
      //  NetworkLobbyManager.Destroy(server);
       // Destroy(LBM);
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Debug.Log("yessss"+SceneManager.GetActiveScene().buildIndex);
        
        SceneManager.LoadScene(0);

    }

}
