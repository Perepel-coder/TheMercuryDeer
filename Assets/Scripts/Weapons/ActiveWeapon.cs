using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public Weapon Weapon { get; private set; }

    public static ActiveWeapon Instance { get; private set; }

    private void Awake()
    {
        Weapon = GetComponentInChildren<Weapon>();
        Instance = this;
    }

    private void Update()
    {
        FollowMousePosition();
    }   

    private void FollowMousePosition()
    {
        transform.rotation = GameInput.Instance.MousePosition.x < Player.Instance.ScreenPosition.x ?
            Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }
}
