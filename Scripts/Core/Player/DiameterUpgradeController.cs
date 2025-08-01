using System;
using UnityEngine;

public class DiameterUpgradeController : MonoBehaviour
{
    public event Action<bool> OnUpgrade;

    [SerializeField]
    private GrassLawnController grassLawnController;
    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private UpgradesSO Upgrades;

    private int currentlevel = 0;

    public void TriggerUpgrade(bool up)
    {
        OnUpgrade?.Invoke(up);
    }

    public int UpgradeDiameter(int playerCoins = 0)
    {
        if (currentlevel >= Upgrades.levels.Length - 1)
        {
            Debug.Log("Достигнут максимальный уровень улучшения.");
            return 0;
        }

        var nextlevel = Upgrades.levels[currentlevel + 1];

        if (playerCoins >= nextlevel.cost)
        {
            grassLawnController.SetDiameter(nextlevel.diameter);

            ScaleObjectToDiameter(nextlevel.diameter);

            currentlevel++;
            TriggerUpgrade(true);

            return nextlevel.cost;
        }
        else
        {
            TriggerUpgrade(false);
            Debug.Log("Недостаточно монет для улучшения.");
            return 0;
        }
    }

    public int NextCost()
    {
        if (currentlevel >= Upgrades.levels.Length - 1)
        {
            Debug.Log("Достигнут максимальный уровень улучшения.");
            return 0;
        }

        return Upgrades.levels[currentlevel + 1].cost;
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
