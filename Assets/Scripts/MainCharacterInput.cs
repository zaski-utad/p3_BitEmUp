using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterInput : MonoBehaviour
{
    Player m_mainCharacter;

    // Start is called before the first frame update
    void Start()
    {
        m_mainCharacter = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
		{
			m_mainCharacter.Move(-1, m_mainCharacter.m_rigidBody.velocity.y / m_mainCharacter.m_speed);
		}
		if (Input.GetKey(KeyCode.D))
		{
			m_mainCharacter.Move(1, m_mainCharacter.m_rigidBody.velocity.y / m_mainCharacter.m_speed);
		}
		if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
		{
			m_mainCharacter.Move(0, m_mainCharacter.m_rigidBody.velocity.y / m_mainCharacter.m_speed);
		}
		if (Input.GetKey (KeyCode.W))
		{
			m_mainCharacter.Move(m_mainCharacter.m_rigidBody.velocity.x / m_mainCharacter.m_speed ,1);
		}
		if (Input.GetKey (KeyCode.S))
		{
			m_mainCharacter.Move(m_mainCharacter.m_rigidBody.velocity.x / m_mainCharacter.m_speed ,-1);
		}
		if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
		{
			m_mainCharacter.Move(m_mainCharacter.m_rigidBody.velocity.x / m_mainCharacter.m_speed , 0);
		}
		if (Input.GetKeyDown (KeyCode.Space))
		{
			m_mainCharacter.Jump ();
		}
		if (Input.GetKeyDown (KeyCode.E))
		{
			m_mainCharacter.Attack ();
		}
		if (Input.GetKeyDown (KeyCode.F))
		{
			m_mainCharacter.Block();
		}
    }
}
