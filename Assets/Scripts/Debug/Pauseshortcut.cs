using UnityEngine;

public class Pauseshortcut : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!UnityEditor.EditorApplication.isPaused)
            {
                Debug.Break();
            }
            else
            {
                UnityEditor.EditorApplication.isPaused = false;
            }
        }
#endif
    }
}
