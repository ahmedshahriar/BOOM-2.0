using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {



    [SerializeField]
    private string weaponLayerName = "Weapon";
    [SerializeField]
    private Transform weaponHolder;
    [SerializeField]
    private PlayerWeapon primaryWeapon;

    private PlayerWeapon currentWeapon;

    private WeaponGfx currentWeaponGfx;


    // Use this for initialization
    void Start ()
    {
        EquipWeapon(primaryWeapon);	
        
	}


	
	// Update is called once per frame
	void Update () {
		
	}
    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public WeaponGfx GetCurrentWeaponGfx()
    {
        return currentWeaponGfx;
    }


    private void EquipWeapon(PlayerWeapon weapon)
    {
        currentWeapon = weapon;
        GameObject weaponInstance=(GameObject)Instantiate(weapon.weaponGfx,weaponHolder.position,weaponHolder.rotation);
        weaponInstance.transform.SetParent(weaponHolder);

        currentWeaponGfx = weaponInstance.GetComponent<WeaponGfx>();

        if (currentWeaponGfx==null)
        {
            Debug.LogError(weaponInstance.name + "No weapon gfx");
        }

        if (isLocalPlayer)
        {
           // weaponInstance.layer = LayerMask.NameToLayer(weaponLayerName);

            SetLayerRecursively(weaponInstance, LayerMask.NameToLayer(weaponLayerName));  //avoid clipping all layer mask

            //Debug.Log(weaponInstance.layer);

        }


        
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null)
            return;

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (child == null)
                continue;

            SetLayerRecursively(child.gameObject,newLayer);
        }
    }
}
