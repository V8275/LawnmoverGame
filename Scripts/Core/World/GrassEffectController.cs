using UnityEngine;

public class GrassEffectController : MonoBehaviour
{
    [SerializeField]
    private GameObject grassFX;

    private void OnDestroy()
    {
        Instantiate(grassFX, transform.position, transform.rotation);
    }
}
