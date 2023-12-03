using System.Linq;
using Godot;

public partial class SwordAbilityController : Node
{
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

		var swordInstance = SwordAbility.Instantiate() as Node2D;
		player.GetParent().AddChild(swordInstance);
		swordInstance.GlobalPosition = player.GlobalPosition;
	}
}
