using UnityEngine;
using System;

// For Scene 02 - Autoplay
[RequireComponent(typeof(Bird))]
public class BirdAI : MonoBehaviour 
{
	[SerializeField]
	private PipeRepeater pipes;
	
	[SerializeField]
	private float pipeOffsetX;
	
	[SerializeField]
	private float clickRate = 0.25f;

	private Bird bird;
	
	private Action currentState;
	
	private float jumpStartY = 0;
	private float jumpDistance = float.MinValue;

	private float radius;
	
	private Transform nearest;
	private float dot = 0;
	private bool isOver;
	
	// Built-in Functions
	void Awake ()
	{
		bird = GetComponent<Bird>();
		radius = GetComponent<CircleCollider2D>().radius;
		
		currentState = PredictJumpDistance;
	}

	void Start () 
	{
		jumpStartY = transform.position.y;
		isOver = false;
	}
	
	void Update () 
	{
		currentState();
	}
	
	void OnDrawGizmos()
	{
		if(false == enabled)
			return;
	
		if(isOver)
		{
			Gizmos.color = Color.red;
		}
		else
		{
			Gizmos.color = Color.cyan;
		}
		
		if(null != nearest)
		{
			Gizmos.DrawLine(transform.position, nearest.position + (Vector3.left * pipeOffsetX));
		}
	}
	
	// Custom Functions
	private void PredictJumpDistance()
	{	
		float distance = transform.position.y - jumpStartY;
		if(distance >= jumpDistance)
		{
			jumpDistance = distance;
			
		}
		else
		{
			currentState = FindNearest;
			InvokeRepeating("AutoFlap", clickRate, clickRate);
		}
	}

	private void FindNearest ()
	{
		Transform nextNearest = pipes.findNearest(transform.position.x, pipeOffsetX);
		
		if(null == nearest)
		{
			nearest = nextNearest;
		}
		
		if(nextNearest != nearest)
		{
			Vector2 p1 = nearest.position;
			Vector2 p2 = nextNearest.position;
			
			p1 += Vector2.right * pipeOffsetX;
			p2 -= Vector2.right * pipeOffsetX;
		
			dot = Vector2.Dot((p2 - p1).normalized, -Vector2.up);
			nearest = nextNearest;
			
			float targetY = nearest.position.y;
			float selfY = transform.position.y;
			
			isOver = (selfY > targetY);
		}
		
	}
	
	private void ForceFlap ()
	{
		bird.FaceUp();
		bird.Flap();
	}
	
	private void AutoFlap ()
	{
		float targetY = nearest.position.y;
		float selfY = transform.position.y;
		
		if( selfY <= targetY )
		{
			ForceFlap ();
		}
		else if( isOver )
		{
			
			float dist = Math.Abs(selfY - targetY);
			if( dist < radius * 1.25f )
			{
				ForceFlap();
				return;
			}

			Vector2 p1 = transform.position;
			Vector2 p2 = nearest.position + (Vector3.left * pipeOffsetX);
			
			Vector2 dir = (p2 - p1).normalized;
			Vector2 down = -Vector2.up;
			
			float currentDot = Vector2.Dot(dir, down);
			if(currentDot <= dot)
			{
				ForceFlap();
				return;
			}
			
		}
		
		
	}
	
	public void Die()
	{
		CancelInvoke("AutoFlap");
		enabled = false;
	}
}
