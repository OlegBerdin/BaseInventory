using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    [Header("Camera")]
    public Camera playerCamera;
    public float interactionDistance = 3f;

    [Header("DebugIds")]
    public int removeId;
    public int containsId;

    private float _rotationX;
    private CharacterController _controller;
    private Inventory _inventory;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _inventory = new Inventory();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
        HandleInteraction();
        HandleInventoryManagment();

    }
    public void HandleInventoryManagment()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            GetAllItems();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveItem(removeId); // удалить предмет 
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ContainsItem(containsId); // проверить, есть ли предмет 
        }
    }
    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        _controller.SimpleMove(move * moveSpeed);
    }

    private void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -80f, 80f);

        playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactionDistance))
            {
                var item = hit.collider.GetComponent<WorldItem>();
                if (item != null)
                {
                    TryPickupItem(item);
                }
            }
        }
    }

    private void TryPickupItem(WorldItem item)
    {
        bool success = _inventory.AddItem(item);
        if (success)
        {
            Debug.Log($"Подобран предмет: {item.ItemName}");
            Destroy(item.gameObject);
        }
    }

    // Методы для доступа к инвентарю
    public bool RemoveItem(int id)
    {
        bool result = _inventory.RemoveItem(id);
        Debug.Log(result ? $"Удалён предмет с Id {id}" : $"Не найден предмет с Id {id}");
        return result;
    }

    public bool ContainsItem(int id)
    {
        bool hasItem = _inventory.ContainsItem(id);
        Debug.Log(hasItem ? $"Есть предмет с Id {id}" : $"Нет предмета с Id {id}");
        return hasItem;
    }

    public IReadOnlyList<WorldItem> GetAllItems()
    {
        var items = _inventory.GetAllItems();
        Debug.Log($"В инвентаре {items.Count} предметов:");
        foreach (var i in items)
            Debug.Log($"• {i.ItemName} (Id {i.Id})");
        return items;
    }
}
