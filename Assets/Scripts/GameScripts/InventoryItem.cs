using System;

[Serializable]
public class InventoryItem
{
    public int Id;
    public string Name;

    public InventoryItem(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() => $"{Id}: {Name}";
}
