using Godot;
using System;

public partial class HealthComponent : Node
{
	[Export]
	public float MaxHealth { get; set; } = 10;

	[Signal]
	public delegate void DiedEventHandler();

	public float CurrentHealth { get; set; }

	public override void _Ready()
	{
		CurrentHealth = MaxHealth;
	}

	private void CheckDeath()
	{
		if (CurrentHealth == 0)
		{
			EmitSignal("Died");
			Owner.QueueFree();
		}
	}

	public void Damage(float damageAmount)
	{
		CurrentHealth = Mathf.Max(CurrentHealth - damageAmount, 0);

		CallDeferred(nameof(CheckDeath));
	}
}
