using UnityEngine;

public interface IMovable
{
    public void PerformMove(float direction = 0);
    public void PerformJump(bool isJump = true);
    public void PerformDash();
    public void CheckGroundedStatus();
    public void SetFields(PlayerAttributes attributes, Rigidbody2D objectRigidbody);
    public void GetFields(PlayerAttributes attributes);
}