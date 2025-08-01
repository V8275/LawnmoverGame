using UnityEngine;
using TMPro;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text grassText;
    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private InventoryController inventoryController;

    private void OnEnable()
    {
        if (inventoryController != null)
        {
            inventoryController.OnGrassCountChanged += UpdateGrassUI;
            inventoryController.OnMoneyCountChanged += UpdateMoneyUI;
        }

        UpdateGrassUI(inventoryController.GrassCount);
        UpdateMoneyUI(inventoryController.MoneyCount);
    }

    private void OnDisable()
    {
        if (inventoryController != null)
        {
            inventoryController.OnGrassCountChanged -= UpdateGrassUI;
            inventoryController.OnMoneyCountChanged -= UpdateMoneyUI;
        }
    }

    private void UpdateGrassUI(int newCount)
    {
        if (grassText != null)
        {
            grassText.text = $"Трава: {newCount}";
        }
    }

    private void UpdateMoneyUI(int newCount)
    {
        if (moneyText != null)
        {
            moneyText.text = $"Деньги: {newCount}";
        }
    }
}
