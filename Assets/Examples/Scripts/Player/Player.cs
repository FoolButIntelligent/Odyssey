using System.Numerics;

public class Player : Entity<Player>
{
    public PlayerEvents PlayerEvents;
    public PlayerInputManager inputs { get; protected set; }

    public virtual void Accelerate(Vector3 direction)
    {
        
    }
}