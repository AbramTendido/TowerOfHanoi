using UnityEngine;



public enum Scene
{
    MainMenu = 0,
    Game = 1,
    Win = 2
}

[CreateAssetMenu(fileName = "CurrentScene", menuName = "ScriptableObjectMenu/CurrentScene", order = 3)]
public class CurrentScene : ScriptableObject
{
    public Scene scene;
}
