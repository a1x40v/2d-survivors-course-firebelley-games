using Godot;

public partial class AbilityUpgradeCard : Panel
{
	private Label _nameLabel;
	private Label _descriptionLabel;

	public override void _Ready()
	{
		_nameLabel = GetNode<Label>("%NameLabel");
		_descriptionLabel = GetNode<Label>("%DescriptionLabel");
	}

	public void SetAbilityUpgrade(AbilityUpgrade upgrade)
	{
		_nameLabel.Text = upgrade.Name;
		_descriptionLabel.Text = upgrade.Description;
	}
}
