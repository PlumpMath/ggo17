using UnityEngine;
using UnityEngine.SceneManagement;

// Put this on the animation of the bunker destruction, so we transition after the animation finishes.
public class OnDestroyedGameOver : MonoBehaviour, IPooledOnDestroy {

	public void OnDestroyPooled()
	{
		SceneManager.LoadScene("GameOver");
	}
}
