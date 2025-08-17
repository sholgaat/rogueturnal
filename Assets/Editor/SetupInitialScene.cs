using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class SetupInitialScene : EditorWindow
{
    [MenuItem("Rogueturnal/Setup Initial Scene")]
    public static void SetupScene()
    {
        // Create new scene
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        scene.name = "Main";

        // Create Player GameObject
        GameObject player = new GameObject("Player");
        player.AddComponent<SpriteRenderer>(); // Placeholder sprite
        player.AddComponent<PlayerController>();

        // Rigidbody2D for physics
        Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        // Create Camera2D
        GameObject cameraObj = new GameObject("Main Camera");
        Camera camera = cameraObj.AddComponent<Camera>();
        camera.orthographic = true;
        cameraObj.tag = "MainCamera";
        cameraObj.transform.position = new Vector3(0, 0, -10);

        // Save scene
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/Main.unity");

        Debug.Log("Initial scene setup complete! Player and Camera2D created.");
    }
}
