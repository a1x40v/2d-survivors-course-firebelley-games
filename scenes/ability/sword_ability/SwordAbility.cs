using Godot;

public partial class SwordAbility : Node2D
{
	[Export]
	public HitboxComponent HitboxComponent { get; set; }

	public override void _Ready()
	{
		HitboxComponent = GetNode<HitboxComponent>("HitboxComponent");
	}
}
