using UnityEngine;
using System;

public class Cake : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int currentHealth;

    [Header("Stage Prefabs (Stage1 to Stage5)")]
    [SerializeField] private GameObject[] stagePrefabs;

    private int currentStageIndex = -1;
    private GameObject currentStageInstance;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }


    public event Action<Cake> OnCakeDestroyed;

    private bool canTakeDamage = true;

    private void Start()
    {
        currentHealth = maxHealth;
        SpawnStage(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTakeDamage)
            return;

        if (!other.CompareTag("Projectile"))
            return;

        canTakeDamage = false;

        Destroy(other.gameObject);
        TakeDamage(1);

        Invoke(nameof(ResetDamageLock), 0f);
    }

    private void ResetDamageLock()
    {
        canTakeDamage = true;
    }

    private void TakeDamage(int amount)
    {
        int previousHealth = currentHealth;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        int newStageIndex = Mathf.Clamp(
            (maxHealth - currentHealth) / 10,
            0,
            stagePrefabs.Length - 1
        );

        if (newStageIndex != currentStageIndex)
        {
            SpawnStage(newStageIndex);
        }

        if (currentHealth <= 0)
        {
            DestroyCurrentStage();
            if (OnCakeDestroyed != null)
                OnCakeDestroyed(this);

            Destroy(gameObject);
        }
    }

    private void SpawnStage(int index)
    {
        DestroyCurrentStage();

        GameObject stage = Instantiate(stagePrefabs[index], transform.position, Quaternion.identity);
        stage.transform.SetParent(transform);
        stage.transform.localScale = Vector3.one;
        stage.transform.localPosition = Vector3.zero;

        currentStageInstance = stage;
        currentStageIndex = index;
    }

    private void DestroyCurrentStage()
    {
        if (currentStageInstance != null)
        {
            Destroy(currentStageInstance);
            currentStageInstance = null;
        }
    }
}
