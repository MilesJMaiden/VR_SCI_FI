using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EnemyFollow : MonoBehaviour
{
    public GameObject player;
    public float targetDistance;
    public float aggroRange = 10f;
    public GameObject enemy;
    public float enemySpeed;
    public int state;
    public RaycastHit shot;
    public GameObject explosion;

    public AudioClip enemyDeath;
    AudioSource audioSource;
    public float vol = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Here it is set so that the enemies will be raycasting towards the player position so that enemeies face the their posisition.
        transform.LookAt(player.transform);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
        {
            targetDistance = shot.distance;

            if (targetDistance < aggroRange) //If the player is within 10f of the enemy, it will move towards them and will reamin stationary otherwise.
            {
                enemySpeed = 0.1f;

                if (state == 0)
                {
                    //enemy.GetComponent<Animation> ().Play("walk");
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed);
                }
            } else
            {
                enemySpeed = 0;
                //enemy.GetComponent<Animation> ().Play("idle");
            }
        }

        if (state == 1)
        {
            //On colliding with the bullet prefab the enemy speed will be set to 0 and the enemy destroy in addition to creating an explosion effect that destroys itself after 5 seconds.
            enemySpeed = 0;
            //enemy.GetComponent<Animation> ().play("anim_open_GoToRoll");
            Destroy(gameObject);
            Destroy(Instantiate(explosion, transform.position, transform.rotation), 0.5f);

        }

        if (state == 2)
        {
            //If the enemy collides with player it will reload the scene.
            enemySpeed = 0;
            //enemy.GetComponent<Animation> ().play("anim_open_GoToRoll");
            Destroy(gameObject);
            Destroy(Instantiate(explosion, transform.position, transform.rotation), 0.5f);
            SceneManager.LoadScene(1);

        }
    }

    void OnTriggerEnter() {

        state = 2;
    }

    void OnCollisionEnter(Collision bullet)
    {
        state = 1;
    }

    void OnDrawGizmosSelected()
    {
        //this Gizmo is for viewing the set aggro range within the editor.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
