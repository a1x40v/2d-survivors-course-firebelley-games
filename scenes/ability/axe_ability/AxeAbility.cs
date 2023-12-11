using Godot;

public partial class AxeAbility : Node2D
{
	public HitboxComponent HitboxComponent { get; set; }

	private const int MaxRadius = 100;

	private Vector2 _baseRotation = Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau));

	public override void _Ready()
	{
		HitboxComponent = GetNode<HitboxComponent>("HitboxComponent");

		var tween = CreateTween();
		tween.TweenMethod(Callable.From((float x) => TweenMethod(x)), 0f, 2f, 3);
		tween.TweenCallback(Callable.From(QueueFree));
	}

	private void TweenMethod(float rotations)
	{
		float persent = rotations / 2;
		float currentRadius = persent * MaxRadius;
		Vector2 currentDirection = _baseRotation.Rotated(rotations * Mathf.Tau);

		var player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		if (player == null) return;

		GlobalPosition = player.GlobalPosition + currentDirection * currentRadius;
	}
}
