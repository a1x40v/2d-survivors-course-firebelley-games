using Godot;

public partial class VelocityComponent : Node
{
	[Export]
	public int MaxSpeed { get; set; } = 40;

	[Export]
	public float Acceleration { get; set; } = 5;

	private Vector2 _velocity;

	public void AccelerateToPlayer()
	{
		var owner2D = Owner as Node2D;
		if (owner2D == null) return;

		var playerNode = GetTree().GetFirstNodeInGroup("player") as CharacterBody2D;
		if (playerNode == null) return;

		Vector2 direction = (playerNode.GlobalPosition - owner2D.GlobalPosition).Normalized();
		AccelerateInDirection(direction);
	}

	public void AccelerateInDirection(Vector2 direction)
	{
		var desiredVelocity = direction * MaxSpeed;
		_velocity = _velocity.Lerp(desiredVelocity, 1 - Mathf.Exp(-Acceleration * (float)GetProcessDeltaTime()));
	}

	public void Move(CharacterBody2D characterBody)
	{
		characterBody.Velocity = _velocity;
		characterBody.MoveAndSlide();
		_velocity = characterBody.Velocity;
	}
}
