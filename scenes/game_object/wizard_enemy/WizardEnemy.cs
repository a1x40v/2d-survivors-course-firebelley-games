using Godot;

public partial class WizardEnemy : CharacterBody2D
{
	private bool _isMoving;
	private Node2D _visuals;

	public VelocityComponent VelocityComponent { get; set; }

	public override void _Ready()
	{
		_visuals = GetNode<Node2D>("Visuals");
		VelocityComponent = GetNode<VelocityComponent>("VelocityComponent");
	}

	public override void _Process(double delta)
	{
		if (_isMoving)
		{
			VelocityComponent.AccelerateToPlayer();
		}
		else
		{
			VelocityComponent.Decelerate();
		}

		VelocityComponent.Move(this);

		var moveSign = Mathf.Sign(Velocity.X);
		if (moveSign != 0)
		{
			_visuals.Scale = new Vector2(moveSign, 1);
		}
	}

	public void SetIsMoving(bool isMoving)
	{
		_isMoving = isMoving;
	}
}
