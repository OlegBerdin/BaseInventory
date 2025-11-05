using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class WorldItem : MonoBehaviour
{
    [Header("Данные предмета")]
    public int Id;
    public string ItemName;

    private void OnValidate()
    {
        // Автоматически заполнить данные при создании
        if (Id == 0)
            Id = Random.Range(1, 100000);
        if (string.IsNullOrEmpty(ItemName))
            ItemName = gameObject.name;
    }

    public override string ToString() => $"{Id}: {ItemName}";
}
