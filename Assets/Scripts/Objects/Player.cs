using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlitheFramework;
public class Player : BaseClass
{
    #region Initialize
    #region EVENT
    public event EventHandler EVENT_REMOVE;
    #endregion EVENT

    #region Public_field
    public bool IsMine { get => isMine; set => isMine = value; }
    public int Id { get => id; set => id = value; }
    #endregion Public_field

    #region Pivate_field
    private bool isMine;
    private int id;

    [SerializeField] List<Sprite> listOfSpritePlayer = new List<Sprite>();

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    private Collider2D collider2D;
    private float movement = 0f;
    private float movementSpeed = 2.5f;
    #endregion Pivate_field
    #endregion Initialize
    public override void Init()
    {
    }
    public void Init(int _id, bool _isMine)
    {
        isMine = _isMine;
        id = _id;
        rigidbody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        //collider2D.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

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
    private void SetSprite()
    {
        spriteRenderer.sprite = listOfSpritePlayer[id - 1];
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
        if (isMine)
        {
            movement = GetAccelerometerInput().x * movementSpeed;
        }
        Movement();
        ResetPosition();
    }

    private void Movement()
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.x = movement;
        rigidbody.velocity = velocity;
        if (rigidbody.velocity.y < 0)
        {
            collider2D.enabled = true;
            //Debug.Log(rigidbody.velocity);
        }
        else
        {
            collider2D.enabled = false;
        }
            
    }
    private void ResetPosition()
    {
        if(this.transform.position.x < -3.2f)
        {
            this.transform.position = new Vector3(3.2f, this.transform.position.y);
        }else if (this.transform.position.x > 3.2f)
        {
            this.transform.position = new Vector3(-3.2f, this.transform.position.y);
        }
    }
    private Vector3 GetAccelerometerInput()
    {
        Debug.Log("ACC : " + Input.acceleration);
        return Input.acceleration;
    }
    #endregion
}