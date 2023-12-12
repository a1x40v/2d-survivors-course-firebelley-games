using Godot;

public partial class ExperienceVial : Node2D
{
	private Sprite2D _sprite2D;
	private CollisionShape2D _colShape2D;

	public override void _Ready()
	{
		_sprite2D = GetNode<Sprite2D>("Sprite2D");
		_colShape2D = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
		var area2D = GetNode<Area2D>("Area2D");
		area2D.AreaEntered += OnAreaEntered;
	}

	private void TweenCollect(float percent, Vector2 startPosition)
	{
		var player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		if (player == null) return;

		GlobalPosition = startPosition.Lerp(player.GlobalPosition, percent);

		Vector2 dirFromStart = player.GlobalPosition - startPosition;
		var targetRotation = dirFromStart.Angle() + Mathf.DegToRad(90);
		Rotation = Mathf.LerpAngle(Rotation, targetRotation, 1 - Mathf.Exp(-2 * (float)GetProcessDeltaTime()));
	}

	private void Collect()
	{
		var gameEvents = GetNode<GameEvents>("/root/GameEvents");
		gameEvents.EmitExperienceVialCollected(1);
		QueueFree();
	}

	private void DisableCollision()
	{
		_colShape2D.Disabled = true;
	}

	private void OnAreaEntered(Area2D area)
	{
		Callable.From(DisableCollision).CallDeferred();

		var initPos = GlobalPosition;

		var tween = CreateTween();
		tween.SetParallel();
		tween.TweenMethod(Callable.From((float x) => TweenCollect(x, initPos)), 0f, 1f, .5)
			.SetEase(Tween.EaseType.In)
			.SetTrans(Tween.TransitionType.Back);

		tween.TweenProperty(_sprite2D, "scale", Vector2.Zero, .15).SetDelay(.35);
		tween.Chain();

		tween.TweenCallback(Callable.From(Collect));
	}
}
