using UnityEngine;

public class PipeRepeater : SpriteRepeater 
{
	[SerializeField]
	private float minY, maxY;

    protected override void OnResetPosition (Transform sprite)
	{
		base.OnResetPosition (sprite);
		
		Vector3 pos = sprite.position;
		pos.y = Random.Range(minY, maxY);
		
		sprite.position = pos;
	}
}
