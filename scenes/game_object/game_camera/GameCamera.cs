using Godot;

public partial class GameCamera : Camera2D
{
	private Vector2 _targetPosition = Vector2.Zero;

	public override void _Ready()
	{
		MakeCurrent();
	}

	public override void _Process(double delta)
	{
		AcquireTarget();
		GlobalPosition = GlobalPosition.Lerp(_targetPosition, 1 - Mathf.Exp(-(float)delta * 20));
	}

	private void AcquireTarget()
	{
		var playerNodes = GetTree().GetNodesInGroup("player");

		if (playerNodes.Count > 0)
		{
			var player = (Node2D)playerNodes[0];
			_targetPosition = player.GlobalPosition;
		}
	}
}
