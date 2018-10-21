using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public class Enemy : BaseLifeform
{
    protected override void die()
    {
        Destroy(gameObject);
    }
}
