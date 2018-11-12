using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour {



    [SerializeField]
	private Camera cam;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private AudioClip shootSound;

    private AudioSource source;

    private float volLowRange = .5f;
    private float volHighRange = 1.0f;


    private PlayerWeapon currentWeapon;
    private WeaponManager weaponManager;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start () {
        if (cam == null)
        {
           // Debug.LogError("No cam");
            this.enabled = false;
        }

        weaponManager = GetComponent<WeaponManager>();

    }
	
	// Update is called once per frame
	void Update () {

        if (PauseMenu.IsOn)
        {
            return;
        }

        currentWeapon = weaponManager.GetCurrentWeapon();
        if (currentWeapon.fireRate<=0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {

                Shoot();
            }

        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {

                InvokeRepeating("Shoot",0f,1f/currentWeapon.fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
            
        }
        
		
	}


    [Command]
    private void CmdOnShoot()
    {
        RpcDoShootEffect();
    }

    [ClientRpc]
    private void RpcDoShootEffect()
    {
       
        weaponManager.GetCurrentWeaponGfx().muzzleFlash.Play();
    }





    [Command]
    private void CmdAudioOnShoot()
    {
        RpcPlayAudio();
    }

    [ClientRpc]
    private void RpcPlayAudio()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(shootSound, vol);

    }





    [Command]
    private void CmdOnHit(Vector3 pos,Vector3 normal)
    {
        RpcDoHitEffect(pos,normal);
    }

    [ClientRpc]
    private void RpcDoHitEffect(Vector3 pos,Vector3 normal)
    {

        GameObject hitEffect=(GameObject) Instantiate(weaponManager.GetCurrentWeaponGfx().hitEffectPrefab, pos, Quaternion.LookRotation(normal));
        Destroy(hitEffect,2f);
    }



    [Client]
    private void Shoot()
        
    {


        if (!isLocalPlayer)
        {
            return;
        }
        if (currentWeapon.bullets<=0  )
        {
          //  Debug.Log("No ammo");  //reload method
            return;
        }

        currentWeapon.bullets--;
      //  Debug.Log("bullets remain"+currentWeapon.bullets);

        CmdOnShoot();
        CmdAudioOnShoot();


        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,currentWeapon.range,mask))
        {

            if (hit.collider.tag=="Player")
            {
                CmdPlayerShot(hit.collider.name,currentWeapon.damage,hit.transform.gameObject);
                
            }


            if (hit.collider.tag == "AmmoBox")
            {
                currentWeapon.bullets = weaponManager.GetCurrentWeapon().maxBullets;

                //  Destroy(hit.collider.transform.parent.gameObject);   //**************************
                CmdAmmoBoxShoot(hit.collider.transform.parent.gameObject);

            }


            CmdOnHit(hit.point,hit.normal);


            //Debug.Log("hit"+hit.collider.name);
        }
    }

    //wanna destroy obj totally?use cmd+Rpc

    [Command]
    void CmdAmmoBoxShoot(GameObject ammoBox)

    {

        RpcVanishAmmoBox(ammoBox);
    }
    [ClientRpc]
    void RpcVanishAmmoBox(GameObject a)
    {
        NetworkServer.Destroy(a);
    }



    [Command]
    void CmdPlayerShot(string playerId,int damage,GameObject p)
    {

        Debug.Log(playerId+"has been shot");
        Player player = GameManager1.GetPlayer(playerId);



        player.RpcTakeDamage(damage,p);



        //NetworkServer.Destroy(p);


        // NetworkTransformChild.Destroy(p);
        //  player.RpcTakeDamage(damage);


    }
    

}
