using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterAnimation : MonoBehaviour
{
    private Animator         m_animator;

    void Start()
    {
        m_animator       = GetComponent<Animator>();
    }

    public void ChangeAnimatorState  (string variable,int i) 
	{
		m_animator.SetInteger (variable, i);
	}

    public void ChangeAnimatorState  (string variable,bool i) 
	{
		m_animator.SetBool (variable, i);
	}
}
