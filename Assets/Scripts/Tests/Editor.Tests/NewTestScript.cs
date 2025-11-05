using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;

public class NewTestScript
{
    Inventory inventory;
    WorldItem item1;
    WorldItem item2;

    [SetUp]
    public void SetUp()
    {
        inventory = new Inventory();
        item1 = new GameObject().AddComponent<WorldItem>();
        item2 = new GameObject().AddComponent<WorldItem>();
    }
    [TearDown]
    public void TearDown()
    {
        inventory = null;
        item1 = null;
    }
    [Test]
    public void AddItem_WhenPressE_AddesCountItemsInList()
    {
        inventory.AddItem(item1);

        Assert.AreEqual(1, inventory.items.Count);
    }
    [Test]
    public void AddItem_WhenIdEquals_Exception()
    {
        item1.Id = 1;
        item2.Id = 1;

        inventory.AddItem(item1);
        
        var ex = Assert.Throws<InvalidOperationException>(() =>
        {
            inventory.AddItem(item2);
        });
    }
    [Test]
    public void RemoveItem_WhenNormalDeleteItem_DecreasesCount()
    {
        inventory.AddItem(item1);

        Assert.AreEqual(1, inventory.GetAllItems().Count);

        inventory.RemoveItem(item1.Id);

        Assert.AreEqual(0, inventory.GetAllItems().Count);
    }
    [Test]
    public void RemoveItem_NonExistentItem_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => inventory.RemoveItem(9999999));

        Assert.AreEqual(0, inventory.GetAllItems().Count);
    }
    [Test]
    public void ContainsItem_ReturnsCorrectValue()
    {
        item1.Id = 1;

        inventory.AddItem(item1);

        Assert.IsTrue(inventory.ContainsItem(1));

        Assert.IsFalse(inventory.ContainsItem(999));
    }
    [Test]
    public void GetAllItems_ReturnsReadOnlyList()
    {
        item1.Id = 1;

        inventory.AddItem(item1);

        var items = inventory.GetAllItems();

        Assert.AreEqual(1, items.Count);

        Assert.Throws<NotSupportedException>(() =>
        {
            ((IList)items).Add(new WorldItem());
        });

        Assert.Throws<NotSupportedException>(() =>
        {
            ((IList)items).Remove(item1);
        });
    }
}
