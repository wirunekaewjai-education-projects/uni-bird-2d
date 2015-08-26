using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour 
{

	[SerializeField]
	private Text currentScoreText;
	
	[SerializeField]
	private Text resultScoreText;

	[SerializeField]
	private Text bestScoreText;
	
	[SerializeField]
	private Image medalImage;
	
	[SerializeField]
	private Sprite bronzeMedalSprite;
	
	[SerializeField]
	private Sprite silverMedalSprite;
	
	[SerializeField]
	private Sprite goldMedalSprite;
	
	private int currentScore;
	
	public void IncreaseScore()
	{
		currentScore++;
		currentScoreText.text = currentScore.ToString();
	}
	
	public void SaveScore()
	{
		PlayerPrefs.SetInt("Score", currentScore);
		
		int bestScore = PlayerPrefs.GetInt("BestScore", 0);
		if(currentScore > bestScore)
		{
			PlayerPrefs.SetString("Medal", "Gold");
			PlayerPrefs.SetInt("BestScore", currentScore);
		}
		else if(currentScore == bestScore)
		{
			PlayerPrefs.SetString("Medal", "Silver");
		}
		else
		{
			PlayerPrefs.SetString("Medal", "Bronze");
		}
	}
	
	public void ShowResult()
	{
		int score = PlayerPrefs.GetInt("Score", 0);
		int bestScore = PlayerPrefs.GetInt("BestScore", 0);
		string medal = PlayerPrefs.GetString("Medal", "Gold");
		
		resultScoreText.text = score.ToString();
		bestScoreText.text = bestScore.ToString();
		
		if(medal == "Gold")
			medalImage.sprite = goldMedalSprite;
		else if (medal == "Silver")
			medalImage.sprite = silverMedalSprite;
		else
			medalImage.sprite = bronzeMedalSprite;
	}
}
