using UnityEngine;

public class GameDisk
{
    private int diskLevel;

    public GameObject gameObject;

    private SpriteRenderer spriteRenderer;

    public float diskScale = 0.3f;


    public GameDisk(int diskLevel, Sprite disk, 
        //Vector2 leftStump, Vector2 middleStump, Vector2 rightStump, 
        SpriteDrawMode sprDrawMode)
    {
        //Assign diskLevel
        this.diskLevel = diskLevel;

        gameObject = new GameObject("Disk");

        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.drawMode = sprDrawMode;
        spriteRenderer.sprite = disk;

        //Change Disk Size
        spriteRenderer.size += Vector2.right * (diskLevel-1) * diskScale;
    }
    public int GetLevel()
    {
        return diskLevel;
    }

}
