using Godot;

public partial class VictoryScreen : CanvasLayer
{
	public override void _Ready()
	{
		GetTree().Paused = true;
		var restartButton = GetNode<Button>("%RestartButton");
		var quitButton = GetNode<Button>("%QuitButton");
		restartButton.Pressed += OnRestartButtonPressed;
		quitButton.Pressed += OnQuitButtonPressed;
	}

	public void OnRestartButtonPressed()
	{
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://scenes/main/Main.tscn");
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
