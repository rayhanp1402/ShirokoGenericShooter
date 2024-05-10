using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandDoubleSpeed : CheatCommandBase{
    public override string commandID { get; protected set; }
    public override string commandDescription { get; protected set; }
    private GameObject _player = GameObject.FindGameObjectWithTag("Player");

    private ShirokoMovement playerMovement;

    public CommandDoubleSpeed(){
        commandID = "doublespeed";
        commandDescription = "Double the player speed";

        AddCommandToConsole();
    }

    public override void RunCommand(){
        playerMovement = _player.GetComponent<ShirokoMovement>();
        playerMovement.speed *= 2;
    }

    public static CommandDoubleSpeed CreateCommand(){
        return new CommandDoubleSpeed();
    }
}