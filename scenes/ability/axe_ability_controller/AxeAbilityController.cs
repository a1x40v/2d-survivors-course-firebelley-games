using Godot;

public partial class AxeAbilityController : Node
{
	[Export]
	public PackedScene AxeAbilityScene { get; set; }

	private float _damage = 10;

	public override void _Ready()
	{
		var timer = GetNode<Timer>("Timer");
		timer.Timeout += OnTimerTimeout;
	}

	public void OnTimerTimeout()
	{
		var player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		if (player == null) return;

		var foregroundLayer = GetTree().GetFirstNodeInGroup("foreground_layer");
		var axeInstance = AxeAbilityScene.Instantiate() as AxeAbility;
		foregroundLayer.AddChild(axeInstance);
		axeInstance.GlobalPosition = player.GlobalPosition;
		axeInstance.HitboxComponent.Damage = _damage;
	}

}
