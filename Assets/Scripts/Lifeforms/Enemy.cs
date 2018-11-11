using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public class Enemy : BaseLifeform
{
    protected override void die()
    {
        if (GameObject.FindWithTag("KillCountUI"))
        {
            GameObject.FindWithTag("KillCountUI").GetComponent<UITesting>().UpdateKillCount();
        }
        
        Destroy(gameObject);
    }
}
