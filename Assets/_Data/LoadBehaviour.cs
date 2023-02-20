using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        this.LoadComponents();
        this.ResetValues();
    }

    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    protected virtual void Start()
    {
        //For Overide
    }

    protected virtual void Update()
    {
        //For Overide
    }

    protected virtual void FixedUpdate()
    {
        //For Overide
    }

    protected virtual void OnDisable()
    {
        //For Overide
    }

    protected virtual void OnEnable()
    {
        //For Overide
    }

    public virtual void LoadComponents()
    {
        //For Overide
    }

    public virtual void ResetValues()
    {
        //For Overide
    }
}
