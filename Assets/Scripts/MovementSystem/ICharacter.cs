public interface IMovable
{
    public void Move();
}

public interface IBounce
{
    public void PerformJump();
}

public interface IDash
{
    public void PerformDash();
}

public interface IStandingGrounded
{
    public void CheckGroundedStatus();
}