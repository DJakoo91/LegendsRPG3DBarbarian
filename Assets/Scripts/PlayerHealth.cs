using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private float timeBetweenHits = 1f;
    [SerializeField] private Collider[] weapons;

    private int _currentHealth;
    private int _currentMaxHealth;
    private float lastHitTime = 0;
    private Animator animator;

    public static bool isAlive = true;

    public int CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = Mathf.Max(0, value);
    }

    public void EnableWapons()
    {
        foreach (Collider weapon in weapons)
            weapon.enabled = true;
    }

    public void DisableWapons()
    {
        foreach (Collider weapon in weapons)
            weapon.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyWeapon") && isAlive && Time.time - lastHitTime > timeBetweenHits)
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage)
    {
        lastHitTime = Time.time;
        _currentHealth -= damage;

        if (_currentHealth > 0)
            animator.SetTrigger("HitBack");
        else
        {
            animator.SetTrigger("Death");
            isAlive = false;
            GetComponent<PlayerMouseControl>().enabled = false;
        }

    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        _currentHealth = startingHealth;
        _currentMaxHealth = startingHealth;
        isAlive = true;
        DisableWapons();
    }

    public float GetHealthRatio()
    {
        return (float)_currentHealth / _currentMaxHealth;
    }
}
