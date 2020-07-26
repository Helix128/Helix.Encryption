using UnityEngine;
using UnityEditor;

public class EncryptorSettings : EditorWindow
{
  
    bool constantSeed;
  

    // Add menu named "My Window" to the Window menu
    [MenuItem("Helix/Encryption/Settings")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        EncryptorSettings window = (EncryptorSettings)EditorWindow.GetWindow(typeof(EncryptorSettings));
        window.Show();
    }

    void OnGUI()
    {
        if(PlayerPrefs.GetInt("cSeed", 0) == 1)
        {
            constantSeed = true;
        }
        else
        {
            constantSeed = false;
        }
        GUILayout.Label("Helix.Encryption Global Settings", EditorStyles.boldLabel);
      
        constantSeed = EditorGUILayout.Toggle("Constant Seed",constantSeed);
        if (constantSeed)
        {
            PlayerPrefs.SetInt("cSeed", 1); 
        }
        else
        {
            PlayerPrefs.SetInt("cSeed", -1);
        }
     
    }
}