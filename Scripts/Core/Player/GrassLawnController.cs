using UnityEngine;

public class GrassLawnController : MonoBehaviour
{
    [SerializeField]
    private float diameter;
    [SerializeField]
    private float diameterMultipler;
    [SerializeField]
    private string GrassTag = "Grass";

    private float radius;
    private InventoryController ic;

    void Start()
    {
        ic = GetComponent<InventoryController>();
        diameter *= diameterMultipler;
        UpdateRadius();
    }

    public void SetDiameter(float newDiameter)
    {
        diameter = newDiameter * diameterMultipler;
        UpdateRadius();
    }

    private void UpdateRadius()
    {
        radius = diameter / 2;
    }

    void Update()
    {
        RemoveObjectsWithTriggers();
    }

    private void RemoveObjectsWithTriggers()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.isTrigger)
            {
                if (hitCollider.gameObject.CompareTag(GrassTag))
                {
                    ic.GrassCount++;
                    Destroy(hitCollider.gameObject);
                }
            }
        }
    }
}
