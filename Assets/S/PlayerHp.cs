using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Slider slider;
    public int hp = 3;
    private Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        slider.value = hp;
    }
    public void demege(int demege)
    {
        hp -= demege;
        if (hp < 0) hp = 0;
        if (hp == 0)
        {
            StartCoroutine(day());
        }
        else
        {
            Animator.SetTrigger("demege");
        }
    }

    private IEnumerator day()
    {
        Animator.SetTrigger("diy");
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        

    }
}
