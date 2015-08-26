using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour 
{
	[SerializeField]
	private UnityEvent onStart;

	[SerializeField]
	private UnityEvent onPause;
	
	void Start()
	{
		onStart.Invoke();
	}

	public void Pause()
	{
		Invoke("OnPause", 1f);
	}
	
	private void OnPause()
	{
		onPause.Invoke();
	}
	
	public void Restart()
	{
		Invoke("OnRestart", 0.5f);
	}
	
	private void OnRestart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}
