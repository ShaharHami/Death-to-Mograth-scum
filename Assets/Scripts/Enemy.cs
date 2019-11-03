using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] bool hasOwnCollider = false;
    [SerializeField] bool destructable = true;
    [SerializeField] GameObject deathFX;
    [SerializeField] float destroyDelay = 1f;
    [SerializeField] int enemyScore = 12;
    [SerializeField] int hits = 10;
    [SerializeField] float gravityMultiplier = 10f;
    GameObject playerCollider;
    GameObject deathFXInstance;
    ScoreBoard scoreBoard;
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player");
        if (!hasOwnCollider)
        {
            AddNonTriggerBoxCollider();
            scoreBoard = FindObjectOfType<ScoreBoard>();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Untagged") && !other.gameObject.CompareTag("Terrain"))
        {
            KillEnemy();
            isDead = true;
        }
    }
    void OnParticleCollision(GameObject other)
    {
        if (gameObject != null)
        {
            int damage;
            // TODO: implement secondary weapon
            // if (other.name == "UltraLasers")
            // {
            //     damage = 2;
            // }
            // else
            // {
            damage = 1;
            // }
            TakeDamage(damage);
            if (hits <= 1)
            {
                KillEnemy();
                isDead = true;
            }
        }
    }
    void TakeDamage(int damage)
    {
        hits -= damage;
    }
    void KillEnemy()
    {
        if (isDead) { return; }
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
        }
        if (scoreBoard != null)
        {
            scoreBoard.ScoreHit(enemyScore);
        }
        AddRigidBody();
        InstantiateFX();
        Invoke("TurnOffMesh", 2f);
        Destroy(gameObject, destroyDelay);
        Destroy(deathFXInstance, destroyDelay);
        // TODO: Make explodable enemies
        // Collider[] colls = Physics.OverlapSphere(deathFXInstance.transform.position, 5f);
        // foreach (var coll in colls)
        // {
        //     Rigidbody rb = coll.GetComponent<Rigidbody>();
        //     print(rb.gameObject.name);
        //     if (rb != null)
        //     {
        //         rb.AddExplosionForce(700f, deathFXInstance.transform.position, 5f);
        //     }
        // }
    }

    private void TurnOffMesh()
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    private void InstantiateFX()
    {
        deathFXInstance = Instantiate(deathFX, transform.position, Quaternion.identity);
    }

    private void AddRigidBody()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.mass = 5000;
        rb.velocity += Vector3.up * Physics.gravity.y * gravityMultiplier;
    }

    void AddNonTriggerBoxCollider()
    {
        Collider enemyCollider = gameObject.AddComponent<BoxCollider>();
        enemyCollider.isTrigger = false;
    }
}
