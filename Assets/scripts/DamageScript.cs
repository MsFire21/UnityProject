using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int DamageCount = 10;
    private float damageInterval = 0.4f;
    private float lastDamageTime;
    private PlayerManager playerManager;

    private void Start()
    {
        lastDamageTime = Time.time; 
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                playerManager.StartCoroutine(playerManager.Damage(DamageCount));
                lastDamageTime = Time.time;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerManager.StartCoroutine(playerManager.Damage(DamageCount));
    }
}
