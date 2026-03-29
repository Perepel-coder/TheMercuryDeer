using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameMainCanvas : MonoBehaviour
{
    public static GameMainCanvas Instance { get; private set; }

    public static Slider HealthSlider { get; set; }

    private void Awake()
    {
        HealthSlider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        Instance = this;
    }
}