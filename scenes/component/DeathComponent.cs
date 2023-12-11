using Godot;

public partial class DeathComponent : Node2D
{
	[Export]
	public HealthComponent HealthComponent { get; set; }

	[Export]
	public Sprite2D Sprite { get; set; }

	public override void _Ready()
	{
		var particle = GetNode<GpuParticles2D>("GPUParticles2D");
		particle.Texture = Sprite.Texture;

		HealthComponent.Connect("Died", new Callable(this, nameof(OnDied)));
	}

	public void OnDied()
	{
		if (Owner == null || Owner is not Node2D) return;

		Vector2 spawnPosition = (Owner as Node2D).GlobalPosition;

		var entitiesLayer = GetTree().GetFirstNodeInGroup("entities_layer");
		GetParent().RemoveChild(this);
		entitiesLayer.AddChild(this);
		GlobalPosition = spawnPosition;

		var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("default");
	}
}
