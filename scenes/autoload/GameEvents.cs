using Godot;
using System;

public partial class GameEvents : Node
{
	[Signal]
	public delegate void ExpirienceVialCollectedEventHandler(float amount);

	public void EmitExperienceVialCollected(float amount)
	{
		EmitSignal("ExpirienceVialCollected", amount);
	}
}
