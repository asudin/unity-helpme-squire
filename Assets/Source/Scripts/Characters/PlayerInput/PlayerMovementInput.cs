using NSCharacters;
using NSHandlers;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovementInput : PlayerMovementInputBase
{
    private PlayerMovementHandler _handler;

    protected override void Awake()
    {
        base.Awake();

        _handler.InitializePlayer(GetComponent<Player>());
    }

    protected override IPlayerMovementHandler CreateMovementHandler()
    {
        _handler = new PlayerMovementHandler(_properties);

        return _handler;
    }

    protected override bool IsPlayerScreenTouching()
    {
        return Input.touchCount > 0 || Input.GetMouseButtonDown(0);
    }
}
