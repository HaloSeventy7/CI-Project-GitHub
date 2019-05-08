﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Roll")]
public class ROllAbility : Ability
{
    [Header("Specific Settings")]
    public float dashSpeed = 1.3f;
    public float dashTime;
    public float startDashTime;
    Roll roll;

    public override void Initialize(GameObject obj)
    {
        roll = obj.GetComponent<Roll>();
        roll.dashSpeed = dashSpeed;
        roll.dashTime = dashTime;
        roll.startDashTime = startDashTime;
        roll.Setup();
    }

    public override void TriggerAbility()
    { 
        roll.PerformRoll();
    }
}