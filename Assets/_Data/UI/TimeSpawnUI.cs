using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpawnUI : LoadBehaviour
{
    public RectTransform process;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadProcess();
    }

    protected void LoadProcess(){
        this.process = transform.Find("Process").GetComponent<RectTransform>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        float scale = (float)TetrisManager.instance.countSpwan / (float)TetrisManager.instance.currentMaxSpwan;
        this.process.localScale = new Vector3(scale,1,1);
    }
}
