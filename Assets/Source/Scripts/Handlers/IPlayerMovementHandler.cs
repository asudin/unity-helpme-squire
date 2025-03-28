using NSCharacters.Movement;

namespace NSHandlers
{
    public interface IPlayerMovementHandler
    {
        void Move();

        void Stop();

        void Update(float time);
    }
}