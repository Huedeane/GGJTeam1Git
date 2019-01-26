using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PlayerDirection { Left , Right }

public class PlayerController : MonoBehaviour {

    #region Variable
    [SerializeField] private float m_PlayerSpeed = 5f;   
    [SerializeField] private float m_MaxRunSpeed = 3f;
    [SerializeField] private float m_JumpForce = 1f;
    [SerializeField] private bool m_IsDead;
    [SerializeField] private bool m_IsGrounded;
    [SerializeField] private bool m_IsMoving;
    [SerializeField] private bool m_IsRunning;
    [SerializeField] private bool m_CanMove;
    [SerializeField] private Rigidbody2D m_PlayerRB;
    [SerializeField] private E_PlayerDirection m_PlayerDirection;
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private float m_XAxis;
    [SerializeField] private float m_YAxis;
    [SerializeField] private float m_CurrentRunSpeed;
    [SerializeField] private float m_RunSpeedIncrement;
    #endregion

    #region Getter & Setter
    public float PlayerSpeed
    {
        get
        {
            return m_PlayerSpeed;
        }

        set
        {
            m_PlayerSpeed = value;
        }
    }
    public float JumpForce
    {
        get
        {
            return m_JumpForce;
        }

        set
        {
            m_JumpForce = value;
        }
    }
    public bool IsDead
    {
        get
        {
            return m_IsDead;
        }

        set
        {
            m_IsDead = value;
        }
    }
    public bool IsGrounded
    {
        get
        {
            return m_IsGrounded;
        }

        set
        {
            m_IsGrounded = value;
        }
    }
    public bool IsMoving
    {
        get
        {
            return m_IsMoving;
        }

        set
        {
            m_IsMoving = value;
        }
    }
    public bool IsRunning
    {
        get
        {
            return m_IsRunning;
        }

        set
        {
            m_IsRunning = value;
        }
    }
    public bool CanMove
    {
        get
        {
            return m_CanMove;
        }

        set
        {
            m_CanMove = value;
        }
    }
    public Rigidbody2D PlayerRB
    {
        get
        {
            return m_PlayerRB;
        }

        set
        {
            m_PlayerRB = value;
        }
    }
    public E_PlayerDirection PlayerDirection
    {
        get
        {
            return m_PlayerDirection;
        }

        set
        {
            m_PlayerDirection = value;
        }
    }
    public LayerMask GroundLayer
    {
        get
        {
            return m_GroundLayer;
        }

        set
        {
            m_GroundLayer = value;
        }
    }
    public Transform GroundCheck
    {
        get
        {
            return m_GroundCheck;
        }

        set
        {
            m_GroundCheck = value;
        }
    }
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return m_SpriteRenderer;
        }

        set
        {
            m_SpriteRenderer = value;
        }
    }
    public float XAxis
    {
        get
        {
            return m_XAxis;
        }

        set
        {
            m_XAxis = value;
        }
    }
    public float YAxis
    {
        get
        {
            return m_YAxis;
        }

        set
        {
            m_YAxis = value;
        }
    }
    public float CurrentRunSpeed
    {
        get
        {
            return m_CurrentRunSpeed;
        }

        set
        {
            m_CurrentRunSpeed = value;
        }
    }
    public float RunSpeedIncrement
    {
        get
        {
            return m_RunSpeedIncrement;
        }

        set
        {
            m_RunSpeedIncrement = value;
        }
    }
    #endregion

    void Start()
    {
        
        m_IsDead = false;
        m_IsGrounded = false;
        m_CanMove = true;
        m_PlayerRB = GetComponent<Rigidbody2D>();
        m_PlayerDirection = E_PlayerDirection.Right;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        m_IsGrounded = Physics2D.OverlapPoint(m_GroundCheck.position, m_GroundLayer);

        if (m_CanMove == true) {

            //Set axis movement
            #region SetAxisMovement
            //Movement on X-axis
            m_XAxis = Input.GetAxisRaw("Horizontal");
            //Movement on Y-axis
            m_YAxis = Input.GetAxisRaw("Vertical");
            #endregion


            /*If Movement on Y-axis yield greater then .001
             *and player is grounded, then add force to the player
             *and make him jump
             */
            if (m_YAxis > 0 && m_IsGrounded)
            {
                Vector2 BaseJump = new Vector2(m_PlayerSpeed * m_XAxis, m_YAxis * m_JumpForce);
                Vector2 AdditionalJumpSpeed = new Vector2(m_CurrentRunSpeed * m_XAxis, m_CurrentRunSpeed * m_YAxis);

                m_PlayerRB.velocity = BaseJump + AdditionalJumpSpeed;
                m_IsGrounded = false;
     
            }

            //Handles speed between moving and running
            #region Moving and Running
            if (m_IsGrounded == true) {
                //Switch max velocity based on if they are running or walking
                if (m_IsMoving == true && m_IsRunning == false)
                {
                    //Switch from running to not running slow down the player until walking speed
                    if (m_CurrentRunSpeed > 0)
                    {
                        m_CurrentRunSpeed -= m_RunSpeedIncrement * Time.deltaTime;
                    }

                    Vector2 AdditionalSpeed = new Vector2(m_CurrentRunSpeed * m_XAxis, m_PlayerRB.velocity.y);

                    //Set the velocity of mario              
                    this.m_PlayerRB.velocity = new Vector2((m_PlayerSpeed * m_XAxis) + AdditionalSpeed.x, this.m_PlayerRB.velocity.y);
                }
                else if (m_IsMoving == true && m_IsRunning == true)
                {
                    if (m_CurrentRunSpeed < m_MaxRunSpeed)
                    {
                        m_CurrentRunSpeed += m_RunSpeedIncrement * Time.deltaTime;
                    }

                    Vector2 BaseSpeed = new Vector2(m_PlayerSpeed * m_XAxis, m_PlayerRB.velocity.y);
                    Vector2 AdditionalSpeed = new Vector2(m_CurrentRunSpeed * m_XAxis, m_PlayerRB.velocity.y);

                    this.m_PlayerRB.velocity = new Vector2(BaseSpeed.x + AdditionalSpeed.x, m_PlayerRB.velocity.y);
                }
                else {
                    m_CurrentRunSpeed = 0;
                }
            }
            #endregion

            //Determine if player is running
            #region CheckIfRunning
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                m_IsRunning = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                m_IsRunning = false;
            }
            #endregion

            //If absolute of x-axis is greater then one, then they are consider moving
            #region CheckIsMoving
            if (Mathf.Abs(m_XAxis) > 0 || Mathf.Abs(m_YAxis) > 0)
                m_IsMoving = true;
            else
                m_IsMoving = false;
            #endregion

            //Set Sprite direction depending on x axis movement
            #region SetSpriteDirection
            //Set Sprite Right
            if (m_XAxis > 0)
            {
                m_SpriteRenderer.flipX = false;
                m_PlayerDirection = E_PlayerDirection.Right;
            }
            //Set Sprite Left
            else if (m_XAxis < 0)
            {
                m_SpriteRenderer.flipX = true;
                m_PlayerDirection = E_PlayerDirection.Left;
            }
            #endregion
        }
    }

}
