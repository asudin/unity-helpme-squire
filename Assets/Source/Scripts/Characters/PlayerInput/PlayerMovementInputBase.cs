using NSCharacters.Movement;
using NSHandlers;
using UnityEngine;

public abstract class PlayerMovementInputBase : MonoBehaviour
{
    [SerializeField] protected PlayerMovementProperties _properties;

    private IPlayerMovementHandler _playerMovementHandler;

    protected virtual void Awake()
    {
        _playerMovementHandler = CreateMovementHandler();
    }

    private void Update()
    {
        var isScreenTouched = IsPlayerScreenTouching();

        if (isScreenTouched)
        {
            _playerMovementHandler.Move();
        }
        else
        {
            _playerMovementHandler.Stop();
        }        
    }

    protected abstract IPlayerMovementHandler CreateMovementHandler();

    protected abstract bool IsPlayerScreenTouching();
}
