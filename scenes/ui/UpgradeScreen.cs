using Godot;
using Godot.Collections;

public partial class UpgradeScreen : CanvasLayer
{
	[Export]
	public PackedScene UpgradeCardScene { get; set; }

	private HBoxContainer _cardContainer;

	public override void _Ready()
	{
		_cardContainer = GetNode<HBoxContainer>("%CardContainer");
		GetTree().Paused = true;
	}

	public void SetAbilityUpgrades(Array<AbilityUpgrade> upgrades)
	{
		foreach (var upgrade in upgrades)
		{
			var cardInstance = UpgradeCardScene.Instantiate() as AbilityUpgradeCard;
			_cardContainer.AddChild(cardInstance);
			cardInstance.SetAbilityUpgrade(upgrade);
		}
	}
}
