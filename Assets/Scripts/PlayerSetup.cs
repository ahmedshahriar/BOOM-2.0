using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componetsToDisable;
    [SerializeField]
    private string remoteLayerName="RemotePlayer";
   

    [SerializeField]
    private GameObject playerUIprefab;
    [HideInInspector]
    public GameObject playerUIInstance;


    Camera sceneCamera;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponent();
            AssignRemotePlayer();
            
        }
        else
        {
            sceneCamera = Camera.main;
            //if (sceneCamera != null)
            //{
            //    sceneCamera.gameObject.SetActive(false);
            //    Debug.Log("found dup");

            //}






            playerUIInstance = Instantiate(playerUIprefab);
            playerUIInstance.name = playerUIprefab.name;




            
            PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
            if (ui == null)
                Debug.LogError("No PlayerUI component on PlayerUI prefab.");
            ui.SetPlayer(GetComponent<Player>());

            GetComponent<Player>().SetupPlayer();


        }

      
        



    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        string netid = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        GameManager1.RegisterPlayer(netid,player);
    }

    //private void RegisterPlayer()
    //{
    //    string id = "Player" + GetComponent<NetworkIdentity>().netId;
    //    transform.name = id;
    //}

    private void DisableComponent()

    {
        for (int i = 0; i < componetsToDisable.Length; i++)
        {
            componetsToDisable[i].enabled = false;

        }

    }
    private void AssignRemotePlayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    //destoryed player/object
    private void OnDisable()
    {

        Destroy(playerUIInstance);
        if (playerUIInstance==null)
        {
            Debug.Log("destroyed UI");
        }
        else
        {
            Debug.Log("noooo");
        }

       // Debug.Log((playerUIInstance.active=false)?"y":"n");

       // NetworkServer.Destroy(playerUIInstance);

        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
            Debug.Log("sceneCam active");
        }
        
        GameManager1.UnregisterPlayer(transform.name);

       // Destroy(this.gameObject);
    }
}
