using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dek : MonoBehaviour
{
    public GameObject BOT;
    public GameObject BOT1;
    public GameObject BOT3;
    public int damageAmount = 1;
    private Rigidbody body;
    public float speed = 0.5f;
    public float jump = 0.5f;
    public int touchGround = 0;
    public float jumpHor = 300f;
    private bool attackLock = false;

    private Animator animator;
    // Start is called before the first frame update

   
    void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Padat", touchGround);
        if (touchGround <= 0 || attackLock)
        {
            return;   
        }

        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.forward;       
        }
        if (Input.GetKey(KeyCode.A))
        {
            move += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right;        
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10;
        }
        else { speed = 5; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
            body.AddRelativeForce(Vector3.up * jump + move*jumpHor);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Atiti");
            StartCoroutine(lockAttack());
            GetComponent<Collider>().isTrigger = true;
        }
        GetComponent<Collider>().isTrigger = false;

        animator.SetFloat("Speed", body.velocity.magnitude);
        move.Normalize();
        body.AddRelativeForce(move * speed);
        body.AddRelativeForce(-move * body.velocity.magnitude);
    }
    private IEnumerator day()
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("kubik"))
           
        {
            StartCoroutine(day());
        }

        if (collision.gameObject.CompareTag("Enemy") &&
            Input.GetKey (KeyCode.E)) {
            Chomber damage = BOT.GetComponent<Chomber>();
            damage.TakeDamage(damageAmount);
        }
        touchGround++;

        if (collision.gameObject.CompareTag("Enemy1") &&
            Input.GetKey(KeyCode.E))
        {
            Chomber damage = BOT1.GetComponent<Chomber>();
            damage.TakeDamage(damageAmount);
        }

        if (collision.gameObject.CompareTag("Enemy3") &&
            Input.GetKey(KeyCode.E))
        {
            Chomber damage = BOT3.GetComponent<Chomber>();
            damage.TakeDamage(damageAmount);
        }
    }

    private void OnCollisionExit(Collision collision) {  touchGround--;
  }
    private IEnumerator lockAttack()
    {
        attackLock = true;
        yield return new WaitForSeconds(3);
        attackLock = false;
    }
}
