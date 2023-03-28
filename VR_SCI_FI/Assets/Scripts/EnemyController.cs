using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //For NavmeshAgent

public class EnemyController : MonoBehaviour
{

    //range of attack
    public float shootRange = 10f;
    public float chaseRange = 15f;

    Transform target; //player
    NavMeshAgent agent;

    public GameObject explosion;

    AudioSource audioSource;
    public float vol = 0.5f;
    public AudioClip explosionSFX;


    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player");
        GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= shootRange)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            audioSource.PlayOneShot(explosionSFX, vol);
            Destroy(gameObject);

        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
