using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Nightmare;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cheat : MonoBehaviour
{
    public Button okButton;

    public Button cancelButton;

    public TMP_InputField cheatInputField;
    public PlayerMovement playerMovement; 
    public DefaultGun defaultGun;
    public PlayerHealth playerHealth;

    public EnemyPetHealth enemyPetHealth;

    public PetHealth petHealth;
    
    private string doublespeed = "doublespeed";

    private string onehitkill = "onehitkill";

    private string godmode = "godmode";

    private string killpet = "killpet";

    private string fullhppet = "fullhppet";
    private string skip = "skip";

    private string motherlode = "motherlode";

    private string orb = "orb";

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote)){
            gameObject.SetActive(!gameObject.activeSelf);
        }
        okButton.onClick.AddListener(ActivateCheat);

        cancelButton.onClick.AddListener(CancelButton);
    }

    void ActivateCheat()
    {
        // Check if the input matches the cheat code
        if (cheatInputField.text == doublespeed)
        {
            // Call the method to double the speed in PlayerMovement
            playerMovement.DoubleSpeed();
        }

        else if(cheatInputField.text == onehitkill)
        {
            // Call the method to activate one hit kill in EnemyBaseHealth
            defaultGun.OneHitKill();
        }

        else if(cheatInputField.text == godmode)
        {
            // Call the method to activate god mode in PlayerMovement
            playerHealth.SetGodMode(true);
        }

        else if(cheatInputField.text == killpet)
        {
            // Call the method to kill the pet in EnemyPetHealth
            enemyPetHealth.SetPetDead();
        }

        else if(cheatInputField.text == fullhppet)
        {
            // Call the method to set the pet health to full in PetHealth
            petHealth.SetGodModePet(true);
        }
    }

    void CancelButton()
    {
        gameObject.SetActive(false);
    }


}
