using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



//extra
[RequireComponent(typeof(PlayerSetup))]


public class Player : NetworkBehaviour {


    private bool isDead = false;
    [SerializeField]
    private AudioClip deathSound;
    
  //  private AudioSource source;

    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

 /*   private void Awake()
    {
        source=GetComponent<AudioSource>();
    }*/

    public bool IsDead { get { return isDead;} protected set { isDead = value; } }


   



    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private GameObject deathEffect;

    //[SerializeField]
    //Text informationText;

    [SyncVar]
    private int currentHealth = 100;
    
    public float GetHealthPct()
    {
        return (float)currentHealth / maxHealth;
    }

    private void Start()
    {
        SetDefaultValues();
    }
    public void SetDefaultValues()
    {
       // isDead = false;
        currentHealth = maxHealth;
    }
    //private void Awake()
    //{
    //    informationText.text = "";
    //}

    public void SetupPlayer()
    {
        if (isLocalPlayer)
        {
            
            //Switch cameras
           // GameManager1.instance.SetSceneCameraActive(false);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);
        }

        
    }



    [ClientRpc]
    public void RpcTakeDamage(int amount,GameObject p)
    {


        if (isDead)
        {
            return;
        }
        currentHealth -= amount;
       // Debug.Log(transform.name+"has"+currentHealth+"health");

        //float c;
        //c = (float) currentHealth / maxHealth;
        ////GetComponent<PlayerUI>()
        //healthBarFill.localScale = new Vector3(1f, c, 1f);



        if (currentHealth<=0)
        {
            

            Die(p);
          //  RpcDied();
        }
    }

    //[ClientRpc]
    //public void RpcDied()
    //{
    //    if (isLocalPlayer)
    //    {
    //        informationText.text = "Over";
    //    }
    //    else
    //    {
    //        informationText.text = "Won";
    //    }
    //}

 /*   [Command]
    private void CmdAudioOnDeath()
    {
        RpcPlayAudio();
    }

    [ClientRpc]
    private void RpcPlayAudio()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(deathSound, vol);
        

    }*/

 
    private void Die(GameObject p)

    {
        IsDead = true;
        // CmdAudioOnDeath();


        AudioSource source1 = GetComponent<AudioSource>();
        float vol = Random.Range(volLowRange, volHighRange);
        source1.PlayOneShot(deathSound, vol);


        GameObject gfxDeath=(GameObject)Instantiate(deathEffect,transform.position,Quaternion.identity);
       
        Destroy(gfxDeath,3f);
        


        // Debug.Log(transform.name +"is dead");


        // GameObject ob= GetComponent<PlayerSetup>().playerUIInstance;
        //NetworkServer.Destroy(ob);

        // Died.text = "DIED";
        NetworkServer.Destroy(p);

      //  Debug.Log(transform.name+"is DEAD");
       

    }

    




}
