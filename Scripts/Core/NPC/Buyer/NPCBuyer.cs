using UnityEngine;
using System.Collections;

public class NPCBuyer : NPC
{
    [SerializeField]
    private float Distance = 2f;
    [SerializeField]
    private GameObject BuyRadius;
    [SerializeField]
    private GameObject BuyerFX;

    private Vector3 waitPoint;
    private Vector3 endPoint;
    private int moneyToGive = 1;
    private float speed = 2f;

    private bool isWaiting = false;

    private NPC nextNPC;
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
            if (nextNPC != null && Vector3.Distance(transform.position, nextNPC.transform.position) < Distance)
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
            BuyRadius.SetActive(true);
            isWaiting = true;
        }
        else if (target == endPoint)
        {
            factory?.NPCFinished(this);
            Destroy(gameObject);
        }
    }

    public void SetPrevNPC(NPC npc)
    {
        nextNPC = npc;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isWaiting && other.CompareTag("Player"))
        {
            TradeAction(other.gameObject);
        }
    }

    protected override void TradeAction(GameObject player)
    {
        InventoryController playerInventory = player.GetComponent<InventoryController>();
        base.TradeAction(player);

        if (playerInventory != null && playerInventory.GrassCount > 0)
        {
            playerInventory.GrassCount -= 1;
            playerInventory.MoneyCount += moneyToGive;
            isWaiting = false;
            BuyRadius.SetActive(false);
            Instantiate(BuyerFX, transform.position, transform.rotation);
            StartCoroutine(MoveToPoint(endPoint));
        }
    }
}
