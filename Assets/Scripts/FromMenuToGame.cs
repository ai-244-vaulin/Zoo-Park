using UnityEngine;
using UnityEngine.SceneManagement;

public class FromMenuToGame : MonoBehaviour
{
	public void GoFromMenuToGame()
	{
		SceneManager.LoadScene("SampleScene");
	}  
}
