using Godot;

public partial class ExperienceManager : Node
{
	private float _currentExp;

	public override void _Ready()
	{
		var gameEvents = GetNode<GameEvents>("/root/GameEvents");
		gameEvents.Connect("ExpirienceVialCollected", new Callable(this, nameof(OnExpirienceVialCollected)));
	}

	public void IncrementExperience(float amount)
	{
		_currentExp += amount;
		GD.Print(_currentExp);
	}

	public void OnExpirienceVialCollected(float amount)
	{
		IncrementExperience(amount);
	}
}
