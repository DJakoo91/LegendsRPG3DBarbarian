using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public float moveSpeed = 20;
    public LayerMask layerMask;
    public GameObject laserDot;
    public Collider[] swordColliders;
    public static bool isAlive = true;

    [SerializeField] private int startingHealth = 100;
    [SerializeField] private float timeBetweenHits = 1f;
    [SerializeField] private Collider[] weapons;

    private float lastAttackTime = -999f;
    private int _currentHealth;
    private int _currentMaxHealth;
    private float lastHitTime = 0;

    void Awake()
    {
        animator = GetComponent<Animator>();
        _currentHealth = startingHealth;
        _currentMaxHealth = startingHealth;
        isAlive = true;
        DisableWeapons();
    }

    void Start()
    {
        EndAttack();
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleAttackInput();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyWeapon") && isAlive && Time.time - lastHitTime > timeBetweenHits)
        {
            TakeDamage(5);
        }
    }

    // ------------------- MOVEMENT -------------------

    private void HandleMovement()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed);
        animator.SetFloat("Speed", (moveDirection * moveSpeed).magnitude);
    }

    private void HandleMouseLook()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 500, Color.red);

        if (Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore))
        {
            laserDot.transform.position = hit.point;
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }
    }

    // ------------------- COMBAT -------------------

    private void HandleAttackInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.Play("Stab");
            lastAttackTime = Time.time;
        }
    }

    public void BeginAttack()
    {
        foreach (Collider swordCollider in swordColliders)
            swordCollider.enabled = true;
    }

    public void EndAttack()
    {
        foreach (Collider swordCollider in swordColliders)
            swordCollider.enabled = false;
    }

    public void EnableWeapons()
    {
        foreach (Collider weapon in weapons)
            weapon.enabled = true;
    }

    public void DisableWeapons()
    {
        foreach (Collider weapon in weapons)
            weapon.enabled = false;
    }

    // ------------------- HEALTH -------------------

    public void TakeDamage(int damage)
    {
        lastHitTime = Time.time;
        _currentHealth -= damage;
        Debug.Log("Player Health: " + _currentHealth);

        if (_currentHealth > 0)
            animator.SetTrigger("HitBack");
        else
        {
            animator.SetTrigger("Death");
            isAlive = false;
        }
    }

    public float GetHealthRatio()
    {
        return (float)_currentHealth / _currentMaxHealth;
    }

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Max(0, value); }
    }
}
