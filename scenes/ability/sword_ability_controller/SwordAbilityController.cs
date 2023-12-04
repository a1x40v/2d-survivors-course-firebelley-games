using System.Linq;
using Godot;

public partial class SwordAbilityController : Node
{
	public const int MaxRange = 150;

	[Export]
	public PackedScene SwordAbility { get; set; }

	public override void _Ready()
	{
		var timer = GetNode<Timer>("Timer");
		timer.Timeout += OnTimerTimeout;
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

		var swordInstance = SwordAbility.Instantiate() as Node2D;
		player.GetParent().AddChild(swordInstance);
		swordInstance.GlobalPosition = closestEnemy.GlobalPosition;
	}
}
