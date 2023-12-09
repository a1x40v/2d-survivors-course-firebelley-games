using Godot;

public partial class ArenaTimeManager : Node
{
	[Signal]
	public delegate void ArenaDifficultyIncreasedEventHandler(int difficulty);

	[Export]
	public PackedScene EndScreenScene { get; set; }

	private const int DifficultyInterval = 5;

	private int _arenaDifficulty;
	private Timer _timer;

	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += OnTimerTimeout;
	}

	public override void _Process(double delta)
	{
		var nextTimeTarget = _timer.WaitTime - (_arenaDifficulty + 1) * DifficultyInterval;
		if (_timer.TimeLeft <= nextTimeTarget)
		{
			_arenaDifficulty++;
			EmitSignal("ArenaDifficultyIncreased", _arenaDifficulty);
		}
	}

	public double GetTimeElapsed()
	{
		return _timer.WaitTime - _timer.TimeLeft;
	}

	public void OnTimerTimeout()
	{
		var endScreenInstance = EndScreenScene.Instantiate();
		AddChild(endScreenInstance);
	}
}
