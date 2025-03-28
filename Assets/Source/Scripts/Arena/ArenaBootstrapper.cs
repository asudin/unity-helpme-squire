using NSCharacters;
using NSCharacters.PlayerInput;
using UnityEngine;

namespace NSArena
{
    public class ArenaBootstrapper : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private PlayerMovementInput _playerMovementInput;

        private void Awake()
        {
            _playerMovementInput = _player.GetComponent<PlayerMovementInput>();
            _playerMovementInput.UpdateGameState(ArenaState.Paused);
        }
    }
}