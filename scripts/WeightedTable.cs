using System.Collections.Generic;
using System.Linq;
using Godot;

public class WeightedItem<T>
{
	public string Id { get; set; }
	public T Item { get; set; }
	public int Weight { get; set; }
}

public class WeightedTable<T>
{
	private int _weightSum;
	private List<WeightedItem<T>> _items { get; set; } = new List<WeightedItem<T>>();

	public int CountItems() => _items.Count;

	public void AddItem(string id, T item, int weight)
	{
		_items.Add(new WeightedItem<T> { Id = id, Item = item, Weight = weight });
		_weightSum += weight;
	}

	public void RemoveItem(string id)
	{
		_items = _items.Where(x => x.Id != id).ToList();
		_weightSum = 0;
		foreach (var item in _items)
		{
			_weightSum += item.Weight;
		}
	}

	public T PickItem(ICollection<string> excludeIds = null)
	{
		List<WeightedItem<T>> adjustedItems = _items;
		int adjustedWeightSum = _weightSum;
		if (excludeIds != null && excludeIds.Count > 0)
		{
			adjustedItems = new List<WeightedItem<T>>();
			adjustedWeightSum = 0;
			foreach (var item in _items)
			{
				if (excludeIds.Contains(item.Id)) continue;
				adjustedItems.Add(item);
				adjustedWeightSum += item.Weight;
			}
		}

		var chosenWeight = GD.RandRange(1, adjustedWeightSum);
		int iterationSum = 0;

		foreach (var item in adjustedItems)
		{
			iterationSum += item.Weight;
			if (chosenWeight <= iterationSum)
			{
				return item.Item;
			}
		}

		throw new System.Exception("Cannot pick item");
	}
}
