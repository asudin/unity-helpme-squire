using NSCharacters;
using NSCharacters.Movement;
using NSHandlers;

public class PlayerMovementHandler : IPlayerMovementHandler
{
    private readonly PlayerMovementProperties _properties;
    private Player _player;
    private bool _isPlayerMoving = false;
    private IMover _currentMover;

    public PlayerMovementHandler(Player player, PlayerMovementProperties properties)
    {
        _player = player;
        _properties = properties;
    }

    private void SetMover(IMover mover)
    {
        _currentMover?.StopMove();
        _currentMover = mover;
        _currentMover.StartMove();
    }

    public void Move()
    {
        if (_isPlayerMoving)
            return;

        SetMover(new PlayerMovePattern(_player, _properties));
        _isPlayerMoving = true;
    }

    public void Stop()
    {
        if (_isPlayerMoving == false)
            return;

        SetMover(new NoMovePattern());
        _isPlayerMoving = false;
    }

    public void Update(float time)
    {
        _currentMover?.Update(time);
    }
}
