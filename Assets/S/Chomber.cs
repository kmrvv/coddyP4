using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Chomber : MonoBehaviour
{
    public int hp = 2;
    public Slider slider;
    public GameObject player;
    private Animator animator;
    public bool seeplayer = false;
    public int demage = 1;
    public int activeDistance = 10;
    public Transform[] wayPoints;
    public float stoppingDistance = 5;
    public float rotationSpeed = 5;
    public Transform defLook;
    private NavMeshAgent agent;
    private Vector3 target;
    private float curTimeout;
    private int wayCount;
    public float timeWait = 3;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetInteger("TouchGround", 1);
    }

    void SetRotation(Vector3 lookAT)
    {
        Vector3 lookPos = lookAT - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = hp;
        if (seeplayer)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            if (wayPoints.Length >= 2)
            {
                agent.stoppingDistance = 0;
                agent.SetDestination(wayPoints[wayCount].position);
                if (!agent.hasPath)
                {
                    SetRotation(defLook.position);
                    curTimeout += Time.deltaTime;
                    if (curTimeout > timeWait)
                    { curTimeout = 0; if (wayCount == wayPoints.Length - 1) wayCount++; else wayCount = 0; }
                }
            }
            else if (wayPoints.Length == 0)
            {
                agent.stoppingDistance = stoppingDistance;
                agent.SetDestination(target);
                if (agent.velocity == Vector3.zero) SetRotation(target);
            }
        }    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("pl"))
        {
            seeplayer = true;
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("pl"))
        {
            seeplayer = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("pl"))
        {
            animator.SetTrigger("Attack1");
            PlayerHp playerHp = player.GetComponent<PlayerHp>();
            playerHp.demege(demage);
        }   
    }
    public void TakeDamage(int damageAmount)
    {
        hp -= damageAmount;

        if (hp <= 0)
        {
            Destroy(gameObject);
            animator.SetTrigger("Die");
            GetComponent<Collider>().enabled = false;
            slider.gameObject.SetActive(false);
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }

}
