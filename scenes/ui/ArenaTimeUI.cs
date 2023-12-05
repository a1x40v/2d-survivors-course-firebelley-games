using Godot;

public partial class ArenaTimeUI : CanvasLayer
{
	[Export]
	public ArenaTimeManager ArenaTimeManager { get; set; }

	private Label label;

	public override void _Ready()
	{
		label = GetNode<Label>("%Label");
	}

	public override void _Process(double delta)
	{
		if (ArenaTimeManager == null) return;

		double timeElapsed = ArenaTimeManager.GetTimeElapsed();
		label.Text = FormatSeconds(timeElapsed);
	}

	private string FormatSeconds(double seconds)
	{
		var minutes = Mathf.Floor(seconds / 60);
		var remainingSeconds = Mathf.Floor(seconds - (minutes * 60));

		return $"{minutes:00}:{remainingSeconds:00}";
	}
}
