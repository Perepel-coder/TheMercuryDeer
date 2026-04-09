using Assets.InputActions;
using Assets.Scripts.Services.Player;
using UnityEngine;

public class ActiveWeaponService : MonoBehaviour
{
    public WeaponService Weapon { get; private set; }

    public bool UseFollowMousePosition { get; set; } = false;

    private void Awake()
    {
        Weapon = GetComponentInChildren<WeaponService>();
    }

    private void Update()
    {
        FollowMousePosition();
    }

    private void FollowMousePosition()
    {
        if(UseFollowMousePosition) 
            transform.rotation = GameInput.Instance.MousePosition.x < PlayerService.Instance.ScreenPosition.x ?
                Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }
}
