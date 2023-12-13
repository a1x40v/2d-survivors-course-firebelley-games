using Godot;

public partial class HurtboxComponent : Area2D
{
	[Export]
	public HealthComponent HealthComponent { get; set; }

	private PackedScene _floatingTextScene = ResourceLoader.Load("res://scenes/ui/FloatingText.tscn") as PackedScene;

	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}

	public void OnAreaEntered(Area2D otherArea)
	{
		if (otherArea is not HitboxComponent) return;
		if (HealthComponent == null) return;

		var hitboxComponent = otherArea as HitboxComponent;
		HealthComponent.Damage(hitboxComponent.Damage);

		var floatingText = _floatingTextScene.Instantiate() as FloatingText;
		GetTree().GetFirstNodeInGroup("foreground_layer").AddChild(floatingText);

		floatingText.GlobalPosition = GlobalPosition + Vector2.Up * 16;
		floatingText.Start(hitboxComponent.Damage.ToString());
	}
}
