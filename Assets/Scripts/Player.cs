using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public  float            m_speed;
    public  float            m_jumpPower;
    public  float            m_blockTime;

    public  Rigidbody2D            m_rigidBody;
	private SpriteRenderer         m_spriteRenderer; 
    private GamePlayManager        m_gameManager;
    private MainCharacterAnimation m_mainCharacterAnimation;

    private float            m_floorLevel;
    private bool             m_isGrounded; 
    private bool             m_isBlocking;

    private void Awake                () 
	{
		Init ();
	}

    private void Init                 () 
	{
        // Get References
        m_rigidBody                   = GetComponent<Rigidbody2D> ();
		m_mainCharacterAnimation      = GetComponent<MainCharacterAnimation>();
        m_spriteRenderer              = GetComponent<SpriteRenderer> ();

        // Set private values
		m_rigidBody.gravityScale      = 0;
		m_floorLevel                  = float.MinValue;
		m_isGrounded                  = true;
	}

    public void  Move                 (float x, float y) 
	{
		if (!m_isBlocking)
        {
            // Flip Sprite
            if (x < 0)
            { 
				transform.rotation = Quaternion.Euler (transform.rotation.x, -180, transform.rotation.z); 
			}
            else if (x > 0)
            { 
				transform.rotation = Quaternion.Euler (transform.rotation.x, 0, transform.rotation.z); 
			}

            // Move the sprite
            m_rigidBody.velocity = new Vector2 (x, m_rigidBody.velocity.y / m_speed) * m_speed; 

            if (m_isGrounded)
            { 
				m_rigidBody.velocity = new Vector2 (m_rigidBody.velocity.x / m_speed, y) * m_speed;

                // Set Animation
                if (m_rigidBody.velocity == Vector2.zero)
                { 
					m_mainCharacterAnimation.ChangeAnimatorState ("movingTransition", 0); 
				}
                else
                { 
					m_mainCharacterAnimation.ChangeAnimatorState ("movingTransition", 2);
				}
			} 
		}
	}
    
    public void  Idle                 () 
	{
        // Set AnimationController
        m_mainCharacterAnimation.ChangeAnimatorState ("movingTransition",      0); 
		m_mainCharacterAnimation.ChangeAnimatorState ("blockTransition",       0); 

		m_isGrounded = true;
	}

    public void  Jump                 ()
    {
        if (m_isGrounded && !m_isBlocking)
        { 
			m_isGrounded             = false;
			m_rigidBody.gravityScale = 10; 
			m_rigidBody.velocity     = new Vector2 (m_rigidBody.velocity.x, m_jumpPower); 
			m_floorLevel             = transform.position.y - 1;

			GetComponent<BoxCollider2D> ().enabled = false; 
			m_mainCharacterAnimation.ChangeAnimatorState ("movingTransition", 3);
		}
    }

    public void  IsInGround                 () 
	{
		m_rigidBody.gravityScale               = 0; 
		m_isGrounded                           = true;
		m_rigidBody.velocity                   = new Vector2 (m_rigidBody.velocity.x, 0);
		m_floorLevel                           = - 10000;
		GetComponent<BoxCollider2D> ().enabled = true;

		Idle (); 
	}

    private void FixedUpdate          ()
    {
        // Check if player is in ground
        if (transform.position.y <= m_floorLevel) 
		{
			IsInGround (); 
		}
	}

    public void Block()
    {
        StartCoroutine ("block");
    }

    public IEnumerator block() 
	{
		if (m_isGrounded && !m_isBlocking)
        { 
			m_rigidBody.velocity = Vector2.zero;
			m_mainCharacterAnimation.ChangeAnimatorState("blockTransition", 1); 
			m_isBlocking = true;
			yield return new WaitForSeconds (m_blockTime); 
			m_isBlocking = false;
			Idle();
		}
	}

    public void  Attack               () 
	{
		StartCoroutine ("attack");
	}

    public IEnumerator attack() 
	{
		if (m_isGrounded && !m_isBlocking) 
        { 
			m_rigidBody.velocity = Vector2.zero; 
			
			m_mainCharacterAnimation.ChangeAnimatorState ("movingTransition", 4); 
            yield return new WaitForSeconds (0.5f); 
            m_mainCharacterAnimation.ChangeAnimatorState ("movingTransition", 0);
		}
	}
}