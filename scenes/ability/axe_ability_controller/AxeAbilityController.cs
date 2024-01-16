using Godot;
using Godot.Collections;

public partial class AxeAbilityController : Node
{
	[Export]
	public PackedScene AxeAbilityScene { get; set; }

	private float _baseDamage = 10;
	private float _additionalDamagePercent = 1;

	public override void _Ready()
	{
		var timer = GetNode<Timer>("Timer");
		timer.Timeout += OnTimerTimeout;

		var gameEvents = GetNode<GameEvents>("/root/GameEvents");
		gameEvents.Connect("AbilityUpgradeAdded", new Callable(this, nameof(OnAbilityUpgradeAdded)));
	}

	public void OnTimerTimeout()
	{
		var player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		if (player == null) return;

		var foregroundLayer = GetTree().GetFirstNodeInGroup("foreground_layer");
		var axeInstance = AxeAbilityScene.Instantiate() as AxeAbility;
		foregroundLayer.AddChild(axeInstance);
		axeInstance.GlobalPosition = player.GlobalPosition;
		axeInstance.HitboxComponent.Damage = _baseDamage * _additionalDamagePercent;
	}

	public void OnAbilityUpgradeAdded(AbilityUpgrade upgrade, Dictionary<string, Dictionary> currentUpgrades)
	{
		if (upgrade.Id == "axe_damage")
		{
			_additionalDamagePercent = 1 + (int)currentUpgrades["axe_damage"]["quantity"] * .1f;
		}
	}

}
