using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damagableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;
    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private bool _isAlive = true;
    Animator animator;
    public bool IsAlive
    {
        get 
        { 
            return _isAlive; 
        }
        set 
        { 
            _isAlive = value;
            animator.SetBool(AnimationStrings.IsAlive, value);
            Debug.Log("IsAlive set" + value);

            if(value == false)
            {
                damageableDeath.Invoke();
            }
        }
    }

    public bool LockVelocity { get { return animator.GetBool(AnimationStrings.lockVelocity); } set { animator.SetBool(AnimationStrings.lockVelocity, value); } }

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    [SerializeField]
    private int _health = 100;

    [SerializeField]
    private bool isInvincible = false;

    public bool Ishit { 
        get 
        { 
            return animator.GetBool(AnimationStrings.isHit); 
        } 
        private set 
        { 
            animator.SetBool(AnimationStrings.isHit, value); 
        } 
    }

    private float timeSinceHit = 0;
    public float invinsibilityTime = 0.25f;

    public int Health
    {
        get 
        { 
            return _health; 
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            if(_health<=0)
            {
                IsAlive = false;
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invinsibilityTime)
            { 
                isInvincible=false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible=true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damagableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }

    public bool Heal(int healthRestored)
    {
        if(IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestored);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }

        return false;
    }

}

