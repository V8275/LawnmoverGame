using UnityEngine;
using System.Collections;

public class NPCBuyer : MonoBehaviour
{
    private Vector3 waitPoint;
    private Vector3 endPoint;
    private int moneyToGive = 1;
    private float speed = 2f;

    private bool isWaiting = false;

    private NPCBuyer nextNPC;
    private NPCBuyerFactory factory;

    public void Init(Vector3 WaitPoint, Vector3 EndPoint,
    int money, float Speed)
    {
        waitPoint = WaitPoint;
        endPoint = EndPoint;
        moneyToGive = money;
        speed = Speed;
        StartCoroutine(MoveToPoint(waitPoint));
    }

    public void SetFactory(NPCBuyerFactory factory)
    {
        this.factory = factory;
    }

    IEnumerator MoveToPoint(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            if (nextNPC != null && Vector3.Distance(transform.position, nextNPC.transform.position) < 1f)
            {
                yield return null;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }
        }

        if (target == waitPoint)
        {
            isWaiting = true;
        }
        else if (target == endPoint)
        {
            factory?.NPCFinished(this);
            Destroy(gameObject);
        }
    }

    public void SetNextNPC(NPCBuyer npc)
    {
        nextNPC = npc;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isWaiting && other.CompareTag("Player"))
        {
            InventoryController playerInventory = other.GetComponent<InventoryController>();
            if (playerInventory != null && playerInventory.GrassCount > 0)
            {
                playerInventory.GrassCount -= 1;
                playerInventory.MoneyCount += moneyToGive;
                isWaiting = false;
                StartCoroutine(MoveToPoint(endPoint));
            }
        }
    }
}
