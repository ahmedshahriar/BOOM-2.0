using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerWeapon {

    public string weaponName = "Ak-47";
    public int  damage = 10;
    public float range = 100f;
    public float fireRate = 0f;

	public uint maxBullets;// = 20;

    [HideInInspector]
    public uint bullets;
    public GameObject weaponGfx;
    public PlayerWeapon()
    {
        bullets = maxBullets;
    }
    
}
