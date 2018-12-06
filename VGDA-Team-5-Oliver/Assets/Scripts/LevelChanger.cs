
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public Animator animator;
    private int mainMenuToLoad;
    private bool mainDisplayed = false;
	
	// Update is called once per frame
	void Update () {

     //   if (mainDisplayed == false)
      if (Input.anyKey)
         {
            FadeToMainMenu(0);
            mainDisplayed = true;
            Debug.Log("any key was used");

            OnFadeComplete();
        }

	}

    public void FadeToMainMenu(int mainMenuIndex)
    {
        mainMenuToLoad = mainMenuIndex;
        animator.SetTrigger("ToMainMenu");
    } 


    public void OnFadeComplete()
    {
        SceneManager.LoadScene(mainMenuToLoad);
    } 
}
