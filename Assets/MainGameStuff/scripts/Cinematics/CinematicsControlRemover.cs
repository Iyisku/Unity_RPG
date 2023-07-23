using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Controller;

public class CinematicsControlRemover : MonoBehaviour
{
    GameObject player;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        GetComponent<PlayableDirector>().played += OnDisableControl;
        GetComponent<PlayableDirector>().stopped +=OnEnableControl;
    }
    void OnEnableControl(PlayableDirector pd) {
        
        player.GetComponent<PlayerController>().enabled = true;
    }
    void OnDisableControl(PlayableDirector pd) {
        player.GetComponent<ActionScheduler>().CancelCurrentAction();
        player.GetComponent<PlayerController>().enabled = false;
    }
}
