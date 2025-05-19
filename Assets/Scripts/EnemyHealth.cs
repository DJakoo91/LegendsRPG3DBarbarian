using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 30;
    [SerializeField] private Collider weapon;
    private int _currentHealth;
    private Animator _animator;
    private bool _isDead = false;

    public Spawner spawner; // reference to spawner
    public PlayerExperience playerEXP;

    void Awake()
    {
        _currentHealth = startingHealth;
        _animator = GetComponent<Animator>();
        DisableWeapons();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            playerEXP = player.GetComponent<PlayerExperience>();
    }

    public bool isDead()
    {
        return _isDead;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isDead && other.CompareTag("PlayerWeapon"))
        {
            TakeDamage(8);
        }
    }

    public void EnableWeapons()
    {
        weapon.enabled = true;
    }

    public void DisableWeapons()
    {
        weapon.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth > 0)
        {
            _animator.SetTrigger("Hit");
        }
        else
        {
            _isDead = true;
            _animator.SetTrigger("Dead");

            if (spawner != null)
                spawner.EnemyDied();

            if (playerEXP != null)
                playerEXP.AddExperience(25); // pieliek

            Destroy(gameObject, 2.5f);
        }
    }
}