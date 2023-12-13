using Godot;

public partial class FloatingText : Node2D
{
	private Label _label;

	public override void _Ready()
	{
		_label = GetNode<Label>("Label");
	}

	public void Start(string text)
	{
		_label.Text = text;

		var tween = CreateTween();
		tween.SetParallel();
		tween.TweenProperty(this, "global_position", GlobalPosition + Vector2.Up * 16, .3)
			.SetEase(Tween.EaseType.Out)
			.SetTrans(Tween.TransitionType.Cubic);
		tween.Chain();

		tween.TweenProperty(this, "global_position", GlobalPosition + Vector2.Up * 48, .5)
			.SetEase(Tween.EaseType.In)
			.SetTrans(Tween.TransitionType.Cubic);
		tween.TweenProperty(this, "scale", Vector2.Zero, .5)
			.SetEase(Tween.EaseType.In)
			.SetTrans(Tween.TransitionType.Cubic);

		tween.TweenCallback(Callable.From(QueueFree));

		var scaleTween = CreateTween();
		scaleTween.TweenProperty(this, "scale", Vector2.One * 1.5f, .15)
			.SetEase(Tween.EaseType.Out)
			.SetTrans(Tween.TransitionType.Cubic);

		scaleTween.TweenProperty(this, "scale", Vector2.One, .15)
			.SetEase(Tween.EaseType.In)
			.SetTrans(Tween.TransitionType.Cubic);
	}
}
