using UnityEngine;

public class RotatePointer : MonoBehaviour
{
    public float rotationTime = 1f;
    public bool speedUp = false;

    public bool rotate = true;
    public bool cahsIN;
    private float elapsedTime = 0f;
    private float rotationDegrees = 360f;

    bool playEndMusic = true;

    void Update()
    {
        if (rotate)
        {
            // Check if the pointer has completed a full rotation and set the rotate flag to false to stop the rotation
            if (elapsedTime >= rotationTime)
            {
                rotate = false;

                foreach (GameObject client in SpawnManager.instance.characterPool)
                {
                    Destroy(client);
                }

                if (!EndGameManager.instance.endLevelScreen.activeSelf & playEndMusic)
                {
                    playEndMusic = false;
                    EndGameManager.instance.EndLevel();
                }
            }
            else
            {
                float rotationSpeed = rotationDegrees / rotationTime;

                // Calculate the rotation speed based on whether the speedUp flag is true or false
                if (speedUp)
                {
                    rotationSpeed *= 50f;
                    elapsedTime += Time.deltaTime * 50f;
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }

                transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
                // Rotate the pointer based on the time elapsed and the rotation speed
                if (cahsIN)
                {
                    ScoreManager.instance.AddScore(1);
                }
            }
        }
    }
}