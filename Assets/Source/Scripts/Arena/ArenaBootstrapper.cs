using NSCharacters;
using UnityEngine;

namespace NSArena
{
    public class ArenaBootstrapper : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private PlayerMovementInput _playerMovementInput;

        private void Awake()
        {
            _playerMovementInput = GetComponent<PlayerMovementInput>();
            _playerMovementInput.UpdateGameState(ArenaState.Paused);
        }
    }
}