using Godot;

public partial class WizardEnemy : CharacterBody2D
{
	private Node2D _visuals;

	public VelocityComponent VelocityComponent { get; set; }

	public override void _Ready()
	{
		_visuals = GetNode<Node2D>("Visuals");
		VelocityComponent = GetNode<VelocityComponent>("VelocityComponent");
	}

	public override void _Process(double delta)
	{
		VelocityComponent.AccelerateToPlayer();
		VelocityComponent.Move(this);

		var moveSign = Mathf.Sign(Velocity.X);
		if (moveSign != 0)
		{
			_visuals.Scale = new Vector2(moveSign, 1);
		}
	}
}
