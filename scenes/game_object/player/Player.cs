using Godot;

public partial class Player : CharacterBody2D
{
	public const int Speed = 125;
	public const int AccelerationSmoothing = 25;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		Vector2 movementVector = GetMovementVector();
		Vector2 direction = movementVector.Normalized();
		Vector2 targetVelocity = direction * Speed;

		Velocity = Velocity.Lerp(targetVelocity, 1 - Mathf.Exp(-(float)delta * AccelerationSmoothing));

		MoveAndSlide();
	}

	private Vector2 GetMovementVector()
	{
		float xMovement = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		float yMovement = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

		return new Vector2(xMovement, yMovement);
	}
}
