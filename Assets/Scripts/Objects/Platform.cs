using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlitheFramework;
using Photon.Pun;

public class Platform : BaseClass
{
    #region Initialize
    #region EVENT
    public event EventHandler EVENT_REMOVE;
    #endregion EVENT

    #region Public_field
    public float Speed { get => speed; set => speed = value; }
    #endregion Public_field

    #region Pivate_field
    Rigidbody2D rigidbody;
    Collider2D collider2D;

    private const float PLAYER_JUMP_SPEED = 5f;

    float speed;
    #endregion Pivate_field
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.velocity;
                velocity.y = PLAYER_JUMP_SPEED;
                rb.velocity = velocity;
            }
        }
    }
    #endregion Initialize
    public override void Init()
    {
        
    }

    public void Init(float _speed)
    {
        speed = _speed;
        //Debug.Log("init platform");
        //Debug.Log("speed : "+speed+"; X Pos : "+this.transform.position.x);
        
    }

    void Start()
    {

    }
    #region factory
    #region EVENT_LISTENER_ADD
    #endregion EVENT_LISTENER_ADD
    #region EVENT_LISTENER_METHOD
    #endregion EVENT_LISTENER_METHOD
    #endregion factory
    #region private method
    private void Move()
    {
        this.transform.position += Vector3.down * speed;
    }
    private void CheckOutOfScreen()
    {
        if(this.transform.position.y < -5.4f)
        {
            Remove();
        }
    }
    #endregion
    #region public method
    public void Remove()
    {
       dispatchEvent(EVENT_REMOVE, this.gameObject, EventArgs.Empty);
    }
    #endregion
    #region update
    public void UpdateMethod()
    {
        Move();
        CheckOutOfScreen();
    }
    #endregion
}