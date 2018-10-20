using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public class Character : BaseLifeform

{

    [SerializeField]
    private String characterName;

    protected override void die()
    {
        Destroy(gameObject);
    }
}
