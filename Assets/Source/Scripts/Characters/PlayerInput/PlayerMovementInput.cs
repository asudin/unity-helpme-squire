using NSCharacters;
using NSHandlers;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovementInput : PlayerMovementInputBase
{
    protected override IPlayerMovementHandler CreateMovementHandler()
    {
        return new PlayerMovementHandler(GetComponent<Player>(), _properties);
    }

    protected override bool IsPlayerScreenTouching()
    {
        return (Input.touchCount > 0 || Input.GetMouseButton(0));
    }
}
