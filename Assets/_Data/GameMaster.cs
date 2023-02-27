using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : LoadBehaviour
{
    public Transform inGameUI;
    public Transform inGame;

    public Transform homeUI;

    public void Home(){
        this.homeUI.gameObject.SetActive(true);
        this.inGame.gameObject.SetActive(false);
        this.inGameUI.gameObject.SetActive(false);
    }

    public void InGame(){
        this.homeUI.gameObject.SetActive(false);
        this.inGame.gameObject.SetActive(true);
        this.inGameUI.gameObject.SetActive(true);
    }
}
