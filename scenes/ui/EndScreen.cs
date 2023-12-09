using Godot;

public partial class EndScreen : CanvasLayer
{
	private Label _titleLabel;
	private Label _descriptionLabel;

	public override void _Ready()
	{
		GetTree().Paused = true;
		_titleLabel = GetNode<Label>("%TitleLabel");
		_descriptionLabel = GetNode<Label>("%DescriptionLabel");

		var restartButton = GetNode<Button>("%RestartButton");
		var quitButton = GetNode<Button>("%QuitButton");
		restartButton.Pressed += OnRestartButtonPressed;
		quitButton.Pressed += OnQuitButtonPressed;
	}

	public void SetDefeat()
	{
		_titleLabel.Text = "Defeat";
		_descriptionLabel.Text = "You lost!";
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
