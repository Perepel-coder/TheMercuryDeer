using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public Weapon Weapon { get; private set; }

    public bool UseFollowMousePosition { get; set; } = false;

    private void Awake()
    {
        Weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        FollowMousePosition();
    }

    private void FollowMousePosition()
    {
        if(UseFollowMousePosition) 
            transform.rotation = GameInput.Instance.MousePosition.x < Player.Instance.ScreenPosition.x ?
                Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }
}
