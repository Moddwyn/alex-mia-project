using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoulderBladePlayer : Singleton<BoulderBladePlayer>
{
    public UnityEvent OnDeath;
    public UnityEvent OnDamage;
    public UnityEvent OnSwing;
    [ReadOnly] public bool canSlice;

    [HorizontalLine]
    public int health;
    public Image healthFill;

    [HorizontalLine]
    public BoulderBladeSword leftSword;
    public BoulderBladeSword rightSword;

    int maxHealth;


    void Start()
    {
        maxHealth = health;
    }

    void Update()
    {
        if (canSlice)
        {
            if(Input.GetMouseButtonDown(0))
            {
                leftSword.anim.SetTrigger("Slice L");
            }
            if(Input.GetMouseButtonDown(1))
            {
                rightSword.anim.SetTrigger("Slice R");
            }
        }

        healthFill.fillAmount = Mathf.MoveTowards(healthFill.fillAmount, Mathf.InverseLerp(0, maxHealth, health), Time.deltaTime * 2);
    }

    public void SliceAction(int side)
    {
        if (canSlice)
        {
            if(side == 1)
            {
                leftSword.anim.SetTrigger("Slice L");
            }
            if(side == 2)
            {
                rightSword.anim.SetTrigger("Slice R");
            }
        }
    }

    public void DoDamage(int damage)
    {
        health -= damage;
        OnDamage?.Invoke();
        if (health <= 0)
        {
            canSlice = false;
            OnDeath?.Invoke();
        }
    }
}
