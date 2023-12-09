using System.Linq;
using Godot;
using Godot.Collections;

public partial class SwordAbilityController : Node
{
	public const int MaxRange = 150;

	private Timer _timer;
	private double _baseWaitTime;
	private float _damage = 5;

	[Export]
	public PackedScene SwordAbility { get; set; }

	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += OnTimerTimeout;
		_baseWaitTime = _timer.WaitTime;

		var gameEvents = GetNode<GameEvents>("/root/GameEvents");
		gameEvents.Connect("AbilityUpgradeAdded", new Callable(this, nameof(OnAbilityUpgradeAdded)));
	}

	private void OnTimerTimeout()
	{
		var player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		if (player == null) return;

		var enemies = GetTree().GetNodesInGroup("enemy");

		var inRangeEnemies = enemies
			.Cast<Node2D>()
			.Where(x =>
				x.GlobalPosition.DistanceSquaredTo(player.GlobalPosition) < Mathf.Pow(MaxRange, 2))
			.ToList();

		if (inRangeEnemies.Count == 0) return;

		var closestEnemy = inRangeEnemies
			.OrderBy(x => x.GlobalPosition.DistanceSquaredTo(player.GlobalPosition))
			.First();

		var swordInstance = SwordAbility.Instantiate() as SwordAbility;
		var foregroundLayer = GetTree().GetFirstNodeInGroup("foreground_layer");
		foregroundLayer.AddChild(swordInstance);
		swordInstance.HitboxComponent.Damage = _damage;
		swordInstance.GlobalPosition = closestEnemy.GlobalPosition;
		swordInstance.GlobalPosition += Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau)) * 4;

		var enemyDirecion = closestEnemy.GlobalPosition - swordInstance.GlobalPosition;
		swordInstance.Rotation = enemyDirecion.Angle();
	}

	public void OnAbilityUpgradeAdded(AbilityUpgrade upgrade, Dictionary<string, Dictionary> currentUpgrades)
	{
		if (upgrade.Id != "sword_rate") return;

		double percentReduction = (int)currentUpgrades["sword_rate"]["quantity"] * .7;
		_timer.WaitTime = Mathf.Max(_baseWaitTime * (1 - percentReduction), 0.3);
		// _timer.Start();
	}
}
