using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class BallisticBallzPlayer : Singleton<BallisticBallzPlayer>
{
    public UnityEvent OnDeath;
    public UnityEvent OnDamage;
    [ReadOnly] public bool canMove;

    [HorizontalLine]
    public bool keyboardControls;
    public float movementSpeed = 5f;
    public float gravity = 9.81f; // Adjust the gravity force as needed
    public int health;
    public Image healthFill;


    CharacterController characterController;
    Animator anim;
    Vector3 velocity;
    Vector3 movement;

    int maxHealth;

    float animTrans;
    [ReadOnly] public float horizontalInput;

    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        maxHealth = health;
    }

    void Update()
    {
        if(keyboardControls)
            horizontalInput = Input.GetAxis("Horizontal");
        animTrans = Mathf.Lerp(animTrans, horizontalInput, Time.deltaTime * 5);
        anim.SetFloat("Speed", animTrans);

        if (canMove)
        {
            movement = new Vector3(0, 0f, -horizontalInput);
            movement *= movementSpeed;
        }

        // Apply gravity
        if (!characterController.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        movement += velocity;

        // Move the character
        characterController.Move(movement * Time.deltaTime);

        // Make sure the character doesn't sink into the ground if grounded
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        healthFill.fillAmount = Mathf.MoveTowards(healthFill.fillAmount, Mathf.InverseLerp(0, maxHealth, health), Time.deltaTime * 2);
    }

    public void DoDamage(int damage)
    {
        health -= damage;
        OnDamage?.Invoke();
        if (health <= 0)
        {
            canMove = false;
            OnDeath?.Invoke();
        }
    }
}
