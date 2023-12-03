using Godot;

public partial class Player : CharacterBody2D
{
	public const int Speed = 200;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		Vector2 movementVector = GetMovementVector();
		Vector2 direction = movementVector.Normalized();

		Velocity = direction * Speed;
		MoveAndSlide();
	}

	private Vector2 GetMovementVector()
	{
		float xMovement = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		float yMovement = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

		return new Vector2(xMovement, yMovement);
	}
}
