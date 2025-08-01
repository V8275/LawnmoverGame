using UnityEngine;

public class DiameterUpgradeController : MonoBehaviour
{
    [SerializeField]
    private GrassLawnController grassLawnController;
    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private UpgradesSO Upgrades;

    private int upgradeCost = 0;
    private int currentlevel = 0;

    public void UpgradeDiameter(int playerCoins = 0)
    {
        var nextlevel = Upgrades.levels[currentlevel + 1];

        if (playerCoins >= nextlevel.cost)
        {
            grassLawnController.SetDiameter(nextlevel.diameter);

            ScaleObjectToDiameter(nextlevel.diameter);

            playerCoins -= upgradeCost;
        }
        else
        {
            Debug.Log("Недостаточно монет для улучшения.");
        }
    }

    private void ScaleObjectToDiameter(float diameter)
    {
        if (targetObject != null)
        {
            float newDiameter = diameter;
            targetObject.transform.localScale = new Vector3(newDiameter, newDiameter, newDiameter);
        }
    }
}
