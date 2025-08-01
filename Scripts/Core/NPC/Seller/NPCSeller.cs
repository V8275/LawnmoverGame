using TMPro;
using UnityEngine;

public class NPCSeller : NPC
{
    [SerializeField]
    private TMP_Text costText;
    [SerializeField]
    private GameObject UpgradeFX;

    DiameterUpgradeController upgradeController;

    private void Start()
    {
        upgradeController = FindAnyObjectByType<DiameterUpgradeController>();
        upgradeController.OnUpgrade += UpdateWeapon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TradeAction(other.gameObject);
        }
    }

    protected override void TradeAction(GameObject player)
    {
        InventoryController playerInventory = player.GetComponent<InventoryController>();

        base.TradeAction(player);

        if (playerInventory != null)
        {
            //current money count
            int money = playerInventory.MoneyCount;

            playerInventory.MoneyCount -= upgradeController.UpgradeDiameter(money);

            int nextCost = upgradeController.NextCost();
            if (costText != null)
            {
                costText.text = $"Стоимость улучшения: {nextCost}";
            }
        }
    }

    private void UpdateWeapon(bool upgrade)
    {
        if(upgrade)
            Instantiate(UpgradeFX, transform.position, transform.rotation);
    }
}
