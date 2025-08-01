using UnityEngine;
using System;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private int grassCount = 0;
    [SerializeField]
    private int moneyCount = 0;

    public event Action<int> OnGrassCountChanged;
    public event Action<int> OnMoneyCountChanged;

    public int GrassCount
    {
        get { return grassCount; }
        set
        {
            if (value >= 0)
            {
                grassCount = value;
                OnGrassCountChanged?.Invoke(grassCount);
            }
            else
            {
                Debug.LogWarning("Значение GrassCount не может быть отрицательным.");
            }
        }
    }

    public int MoneyCount
    {
        get { return moneyCount; }
        set
        {
            if (value >= 0)
            {
                moneyCount = value;
                OnMoneyCountChanged?.Invoke(moneyCount);
            }
            else
            {
                Debug.LogWarning("Значение MoneyCount не может быть отрицательным.");
            }
        }
    }
}
