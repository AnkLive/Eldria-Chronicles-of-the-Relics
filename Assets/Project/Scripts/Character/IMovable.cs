using System;
using UnityEngine;

public interface IMovable
{
    public void PerformMove(float direction = 0);
    public void PerformJump(bool isJump = true);
    public void PerformDash();
    public void CheckGroundedStatus();
    public void UpdateMovementComponentData(MovementComponentData componentData);
    public void SetObjectRigidbody(Rigidbody2D objectRigidbody);
    public event Action<bool> OnDashing;
}