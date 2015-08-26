using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour 
{
	[SerializeField]
	private Vector2 force = Vector2.up;
	
	[SerializeField]
	private UnityEvent onFlap;
	
	[SerializeField]
	private UnityEvent onTrigger;
	
	[SerializeField]
	private UnityEvent onCollision;

	private Rigidbody2D rigidBody2D;
	
	private bool died = false;
	
	// Monobehaviour functions
	void Awake ()
	{
		rigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	void Start () 
	{
		Flap();
	}
	
	void Update () 
	{
		if(true == isFlap() && false == died)
		{
			FaceUp();
			Flap();
		}
		else
		{
			FaceDown();
		}
	}
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(true == died)
			return;
	
		onTrigger.Invoke();
	}
	
	void OnCollisionEnter2D(Collision2D c)
	{
		if(true == died)
			return;
		
		onCollision.Invoke();
	}
	
	// Custom functions
	public void Die ()
	{
		died = true;
	}
	
	private bool isFlap()
	{
		bool mouseDown = Input.GetMouseButtonDown(0);
		bool touchDown = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
		
		return mouseDown || touchDown;
	}
	
	
	public void Flap()
	{
		rigidBody2D.velocity = Vector2.zero;
		rigidBody2D.AddForce(force);
		
		onFlap.Invoke();
	}
	
	public void FaceUp()
	{
		float currentAngle = CurrentAngle();
		float faceUpAngle  = (currentAngle < 45) ? 45 - currentAngle : 0;
		
		transform.Rotate(0, 0, faceUpAngle);
	}
	
	private void FaceDown()
	{
		float currentAngle = CurrentAngle();
		float faceDownAngle = (currentAngle > -90) ? -2 : 0;
		
		transform.Rotate(0, 0, faceDownAngle);
	}
	
	private float CurrentAngle()
	{
		float currentAngle = transform.eulerAngles.z;
		
		if(currentAngle > 180)
			currentAngle -= 360;
			
		return currentAngle;
	}
	
}
