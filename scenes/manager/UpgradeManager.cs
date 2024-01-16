using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class UpgradeManager : Node
{
    [Export]
    public Node ExperienceManager { get; set; }

    [Export]
    public PackedScene UpgradeScreenScene { get; set; }

    private WeightedTable<AbilityUpgrade> _upgradePool = new WeightedTable<AbilityUpgrade>();
    private Ability _upgradeAxe = ResourceLoader.Load("res://resources/upgrades/axe.tres") as Ability;
    private AbilityUpgrade _upgradeAxeDamage = ResourceLoader.Load("res://resources/upgrades/axe_damage.tres") as AbilityUpgrade;
    private AbilityUpgrade _upgradeSwordRate = ResourceLoader.Load("res://resources/upgrades/sword_rate.tres") as AbilityUpgrade;
    private AbilityUpgrade _upgradeSwordDamage = ResourceLoader.Load("res://resources/upgrades/sword_damage.tres") as AbilityUpgrade;

    public Godot.Collections.Dictionary<string, Dictionary> CurrentUpgrades { get; set; } = new Godot.Collections.Dictionary<string, Dictionary>();

    public override void _Ready()
    {
        _upgradePool.AddItem(_upgradeAxe.Id, _upgradeAxe, 10);
        _upgradePool.AddItem(_upgradeSwordRate.Id, _upgradeSwordRate, 10);
        _upgradePool.AddItem(_upgradeSwordDamage.Id, _upgradeSwordDamage, 10);

        ExperienceManager.Connect("LevelUp", new Callable(this, nameof(OnLevelUp)));
    }

    private void UpdateUpgradePool(AbilityUpgrade chosenUpgrade)
    {
        if (chosenUpgrade.Id == _upgradeAxe.Id)
        {
            _upgradePool.AddItem(_upgradeAxeDamage.Id, _upgradeAxeDamage, 10);
        }
    }

    private void ApplyUpgrade(AbilityUpgrade upgrade)
    {
        var hasUpgrade = CurrentUpgrades.ContainsKey(upgrade.Id);
        if (!hasUpgrade)
        {
            var dict = new Dictionary
            {
                { "resource", upgrade },
                { "quantity", 1 }
            };
            CurrentUpgrades.Add(upgrade.Id, dict);
        }
        else
        {
            CurrentUpgrades[upgrade.Id]["quantity"] = (int)CurrentUpgrades[upgrade.Id]["quantity"] + 1;
        }

        if (upgrade.MaxQuantity > 0)
        {
            var currentQuantity = (int)CurrentUpgrades[upgrade.Id]["quantity"];
            if (currentQuantity == upgrade.MaxQuantity)
            {
                _upgradePool.RemoveItem(upgrade.Id);
            }
        }

        UpdateUpgradePool(upgrade);
        var gameEvents = GetNode<GameEvents>("/root/GameEvents");
        gameEvents.EmitAbilityUpgradeAdded(upgrade, CurrentUpgrades);
    }

    private ICollection<AbilityUpgrade> PickUpgrades()
    {
        var chosenUpgrades = new List<AbilityUpgrade>();

        for (int i = 0; i < 2; i++)
        {
            if (_upgradePool.CountItems() == chosenUpgrades.Count) break;
            var chosenUpgrade = _upgradePool.PickItem(chosenUpgrades.Select(x => x.Id).ToList());
            chosenUpgrades.Add(chosenUpgrade);
        }

        return chosenUpgrades;
    }

    public void OnLevelUp(int currentLevel)
    {
        var upgradeScreenInstance = UpgradeScreenScene.Instantiate() as UpgradeScreen;
        AddChild(upgradeScreenInstance);

        var chosenUpgrades = PickUpgrades();
        upgradeScreenInstance.SetAbilityUpgrades(chosenUpgrades);

        upgradeScreenInstance.Connect("UpgradeSelected", new Callable(this, nameof(OnUpgradeSelected)));
    }

    public void OnUpgradeSelected(AbilityUpgrade upgrade)
    {
        ApplyUpgrade(upgrade);
    }
}
