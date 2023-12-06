using Godot;

public partial class VialDropComponent : Node
{
	[Export(PropertyHint.Range, "0,1,")]
	public float DropPercent { get; set; } = 0.5f;

	[Export]
	public PackedScene VialScene { get; set; }

	[Export]
	public HealthComponent HealthComponent { get; set; }

	public override void _Ready()
	{
		HealthComponent.Connect("Died", new Callable(this, nameof(OnDied)));
	}

	public void OnDied()
	{
		if (GD.Randf() > DropPercent) return;
		if (VialScene == null) return;
		if (Owner is not Node2D) return;

		Vector2 spawnPosition = (Owner as Node2D).GlobalPosition;
		var vialInstance = VialScene.Instantiate() as Node2D;
		Owner.GetParent().AddChild(vialInstance);
		vialInstance.GlobalPosition = spawnPosition;
	}
}
