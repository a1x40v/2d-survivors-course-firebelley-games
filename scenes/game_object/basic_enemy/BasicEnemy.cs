using Godot;

public partial class BasicEnemy : CharacterBody2D
{
	public const int MaxSpeed = 40;

	public HealthComponent HealthComponent { get; set; }

	public override void _Process(double delta)
	{
		Vector2 direction = GetDirectionToPlayer();
		Velocity = direction * MaxSpeed;
		MoveAndSlide();
	}

	private Vector2 GetDirectionToPlayer()
	{
		var playerNode = GetTree().GetFirstNodeInGroup("player") as Node2D;

		if (playerNode != null)
			return (playerNode.GlobalPosition - GlobalPosition).Normalized();

		return Vector2.Zero;
	}
}
