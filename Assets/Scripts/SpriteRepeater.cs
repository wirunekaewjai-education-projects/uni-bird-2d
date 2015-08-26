using UnityEngine;
using System.Collections;

public class SpriteRepeater : MonoBehaviour 
{
	[SerializeField]
	private GameObject original;
	
	[SerializeField]
	private int quantity;
	
	[SerializeField]
	private float width;

	[SerializeField]
	private float startPoint;
	
	[SerializeField]
	private float endPoint;
	
	[SerializeField]
	private float moveSpeed = 1f;
	
	private Transform[] sprites;
	
	void Start ()
	{
		sprites = new Transform[quantity];
		
		Vector3 oPos = original.transform.position;
		Quaternion rot = Quaternion.identity;
		
		for (int i = 0; i < quantity; i++) 
		{
			Vector3 pos = new Vector3(startPoint + (i * width), oPos.y, oPos.z);
			GameObject g = Instantiate(original, pos, rot) as GameObject;
			
			sprites[i] = g.transform;
			OnResetPosition(g.transform);
		}
	}


	// Update is called once per frame
	void Update () 
	{
		
		foreach(Transform sprite in sprites)
		{
			sprite.Translate(-moveSpeed * Time.deltaTime, 0, 0);
			
			if(sprite.position.x <= endPoint)
			{
				sprite.Translate(quantity * width, 0, 0);
				OnResetPosition(sprite);
			}
		}
	}
	
	protected virtual void OnResetPosition(Transform sprite)
	{
		
	}
	
	// Used in Scene 02 - Autoplay
	public Transform findNearest(float x, float offsetX)
	{
		float minDistance = float.MaxValue;
		Transform nearest = sprites[0];
		
		foreach(Transform sprite in sprites)
		{
			if(x > sprite.position.x + offsetX)
				continue;
		
			float dist = Mathf.Abs(sprite.position.x - x);
			if(dist <= minDistance)
			{
				minDistance = dist;
				nearest = sprite;
			}
		}
		
		return nearest;
	}
}
