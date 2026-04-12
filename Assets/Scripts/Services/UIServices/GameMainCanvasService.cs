using UnityEngine;
using UnityEngine.UI;

public class GameMainCanvasService : MonoBehaviour
{
    public static GameMainCanvasService Instance { get; private set; }

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