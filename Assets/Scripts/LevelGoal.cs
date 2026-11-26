using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    bool reached;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) reached = true;
    }

    void OnGUI()
    {
        if (!reached) return;

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 40;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "¡Zona completada!", style);
    }
}
