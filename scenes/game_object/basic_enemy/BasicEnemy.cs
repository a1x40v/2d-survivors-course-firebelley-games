using Godot;

public partial class BasicEnemy : CharacterBody2D
{
	public const int MaxSpeed = 40;

	private Node2D _visuals;

	public HealthComponent HealthComponent { get; set; }

	public override void _Ready()
	{
		_visuals = GetNode<Node2D>("Visuals");
	}

	public override void _Process(double delta)
	{
		Vector2 direction = GetDirectionToPlayer();
		Velocity = direction * MaxSpeed;
		MoveAndSlide();

		var moveSign = Mathf.Sign(Velocity.X);
		if (moveSign != 0)
		{
			_visuals.Scale = new Vector2(-moveSign, 1);
		}
	}

	private Vector2 GetDirectionToPlayer()
	{
		var playerNode = GetTree().GetFirstNodeInGroup("player") as Node2D;

		if (playerNode != null)
			return (playerNode.GlobalPosition - GlobalPosition).Normalized();

		return Vector2.Zero;
	}
}
