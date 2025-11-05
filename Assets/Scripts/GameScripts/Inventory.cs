using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Inventory
{
    public readonly List<WorldItem> items = new();

    public bool AddItem(WorldItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "Нельзя добавить пустой объект.");

        if (items.Exists(i => i.Id == item.Id))
            throw new InvalidOperationException($"Объект с ID {item.Id} уже добавлен в инвентарь.");

        items.Add(item);
        return true;
    }

    public bool RemoveItem(int id)
    {
        var item = items.FirstOrDefault(i => i.Id == id);
        if (item == null)
            return false;

        items.Remove(item);
        return true;
    }

    public bool ContainsItem(int id) => items.Any(i => i.Id == id);

    public IReadOnlyList<WorldItem> GetAllItems() => items.AsReadOnly();
}
