using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    private string[] sceneNames = {"Scenario1_1", "Scenario1_1Quest", "Scenario1_2", "Scenario1_2Quest", "Scenario2_1", "Scenario2_1Quest", "Scenario2_2", "Scenario2_2Quest", "Scenario3_1", "Scenario3_1Quest", "Scenario3_2", "Scenario3_2Quest"};
    private static int counterScenes = 0;

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1);

        if (currentSceneIndex == sceneNames.Length)  //when the index is the last scene
        {
            if(counterScenes == 5)
            {
                Application.Quit();  //when six runs done quit
            }
            else
            {
                counterScenes++;
                SceneManager.LoadScene(1);  //increment and load scene 1
            }
        }
        else
        {
            if (counterScenes == 5)
            {
                Application.Quit();  //when six runs done quit
            }
            else
            {
                counterScenes++;
                Debug.Log(counterScenes);
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
    }
}

