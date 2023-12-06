using Godot;

public partial class ExperienceManager : Node
{
	[Signal]
	public delegate void ExpirienceUpdatedEventHandler(float currentExp, float targetExp);

	private const float TargetExperienceGrow = 5;
	private int _currentLevel = 1;
	private float _currentExp;
	private float _targetExp = 5;

	public override void _Ready()
	{
		var gameEvents = GetNode<GameEvents>("/root/GameEvents");
		gameEvents.Connect("ExpirienceVialCollected", new Callable(this, nameof(OnExpirienceVialCollected)));
	}

	public void IncrementExperience(float amount)
	{
		_currentExp = Mathf.Min(_currentExp + amount, _targetExp);
		EmitSignal("ExpirienceUpdated", _currentExp, _targetExp);

		if (_currentExp == _targetExp)
		{
			_currentLevel++;
			_targetExp += TargetExperienceGrow;
			_currentExp = 0;
			EmitSignal("ExpirienceUpdated", _currentExp, _targetExp);
		}
	}

	public void OnExpirienceVialCollected(float amount)
	{
		IncrementExperience(amount);
	}
}
