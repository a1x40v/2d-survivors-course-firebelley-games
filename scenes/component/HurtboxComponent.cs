using Godot;

public partial class HurtboxComponent : Area2D
{
	[Export]
	public HealthComponent HealthComponent { get; set; }

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
	}
}
