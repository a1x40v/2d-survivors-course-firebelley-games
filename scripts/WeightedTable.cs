using Godot;
using Godot.Collections;

public class WeightedTable
{
	private int _weightSum;
	private Array<Dictionary> _items { get; set; } = new Array<Dictionary>();

	public void AddItem(PackedScene item, int weight)
	{
		_items.Add(new Dictionary {
			{ "item", item },
			{ "weight", weight}
		});
		_weightSum += weight;
	}

	public PackedScene PickItem()
	{
		var chosenWeight = GD.RandRange(1, _weightSum);
		int iterationSum = 0;

		foreach (var item in _items)
		{
			iterationSum += (int)item["weight"];
			if (chosenWeight <= iterationSum)
			{
				return (PackedScene)item["item"];
			}
		}

		throw new System.Exception("Cannot pick item");
	}
}
