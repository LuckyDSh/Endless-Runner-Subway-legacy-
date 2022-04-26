/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAME_CONTROLLER : MonoBehaviour
{
    public void RESTART()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QUIT()
    {
        Application.Quit();
    }
}
