using UnityEngine;

public class AudioPlayer : MonoBehaviour 
{
	[SerializeField]
	private AudioClip flapAudioClip;
	
	[SerializeField]
	private AudioClip pointAudioClip;
	
	[SerializeField]
	private AudioClip hitAudioClip;
	
	[SerializeField]
	private AudioClip dieAudioClip;
	
	[SerializeField]
	private AudioClip swooshAudioClip;
	
	[SerializeField]
	private AudioSource audioSource;

	public void Flap()
	{
		audioSource.PlayOneShot(flapAudioClip);
	}
	
	public void Point()
	{
		audioSource.PlayOneShot(pointAudioClip);
	}
	
	public void Hit()
	{
		audioSource.PlayOneShot(hitAudioClip);
		Invoke("Die", 0.4f);
	}
	
	public void Die()
	{
		audioSource.PlayOneShot(dieAudioClip);
	}
	
	public void Swoosh()
	{
		audioSource.PlayOneShot(swooshAudioClip);
	}
}
