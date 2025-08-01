using UnityEngine;
using System.Collections;

public class NPCBuyerFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject npcPrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform waitPoint;
    [SerializeField]
    private Transform endPoint;
    [SerializeField]
    private int moneyToGive = 1;
    [SerializeField]
    private int Speed = 1;
    [SerializeField]
    private int maxNPCs = 5;

    private int currentNPCs = 0;

    NPC prevNPC = null;

    void Start()
    {
        StartCoroutine(SpawnNPCs());
    }

    IEnumerator SpawnNPCs()
    {
        while (true)
        {
            if (currentNPCs < maxNPCs)
            {
                GameObject npcObj = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
                NPCBuyer npc = npcObj.GetComponent<NPCBuyer>();
                npc.Init(waitPoint.position, endPoint.position, moneyToGive, Speed);
                npc.SetFactory(this);
                if(prevNPC)
                    npc.SetPrevNPC(prevNPC);

                prevNPC = npc;

                currentNPCs++;
            }
            yield return new WaitForSeconds(2f);
        }
    }

    public void NPCFinished(NPCBuyer npc)
    {
        currentNPCs--;
        Destroy(npc.gameObject);
    }
}