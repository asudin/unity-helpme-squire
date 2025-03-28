using NSCharacters;
using NSCharacters.Movement;
using NSHandlers;

public class PlayerMovementHandler : IPlayerMovementHandler
{
    private readonly PlayerMovementProperties _properties;
    private Player _player;
    private bool _isPlayerMoving;

    public PlayerMovementHandler(PlayerMovementProperties properties)
    {
        _properties = properties;
    }

    public void InitializePlayer(Player player) =>
        _player = player;

    public void Move()
    {
        if (_isPlayerMoving)
            return;

        _player.SetMover(new PlayerMovePattern(_player, _properties));
        _isPlayerMoving = true;
    }

    public void Stop()
    {
        if (_isPlayerMoving == false)
            return;

        _player.SetMover(new NoMovePattern());
        _isPlayerMoving = false;
    }
}
