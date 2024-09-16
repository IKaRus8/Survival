using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class StartFromFirstScene : MonoBehaviour
{
   
        [RuntimeInitializeOnLoadMethod]
        private static void SetInitialSceneOnLoad()
        {
            SceneManager.LoadScene(0); // Replace with the index of your first scene
        }
   
}
