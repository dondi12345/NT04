using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : LoadBehaviour
{
    public Text textScore;

    protected override void OnEnable()
    {
        try
        {
            this.textScore.text = TetrisManager.instance.score.ToString();  
        }
        catch (System.Exception){}
    }
}
