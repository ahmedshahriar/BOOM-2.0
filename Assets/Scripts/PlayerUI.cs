using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


[RequireComponent(typeof(Player))]
public class PlayerUI : MonoBehaviour {

    

    [SerializeField]
    RectTransform healthBarFill;

    [SerializeField]
    Text ammoText;

    [SerializeField]
    GameObject pauseMenu;
    


    private Player player;
    
    private WeaponManager weaponManager;


    

    public void SetPlayer(Player _player)
    {
        player = _player;
        weaponManager = player.GetComponent<WeaponManager>();
        
        
    }

    void Start()
    {
        PauseMenu.IsOn = false;
        
    }

    void Update()
    {
       

        SetHealthAmount(player.GetHealthPct());

       // Debug.Log(player.GetHealthPct());

        SetAmmoAmount(weaponManager.GetCurrentWeapon().bullets);

        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            
            TogglePauseMenu();
        }




    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.IsOn = pauseMenu.activeSelf;
        AudioListener.pause = !AudioListener.pause;

    }



    public void SetHealthAmount(float amount)
    {
        healthBarFill.localScale = new Vector3(1f, amount, 1f);
    }

    void SetAmmoAmount(uint amount)
    {
        ammoText.text = amount.ToString();
       
    }

}

