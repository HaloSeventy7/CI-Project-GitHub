﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentTimeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    LevelTransitionManager levelTransitionManager;

    void Start()
    {
        levelTransitionManager = FindObjectOfType<LevelTransitionManager>();

        timerText.text = levelTransitionManager.GetEndTime();
    }
}
