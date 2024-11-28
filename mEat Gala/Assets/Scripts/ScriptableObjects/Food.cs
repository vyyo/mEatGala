using UnityEngine.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Food", order = 0)]
public class Food : ScriptableObject
{
    public Courses course;
    public InputType inputType;
    public Sprite foodSprite;
    public float resistance = 1;
    public int saturation = 1;
    public RuntimeAnimatorController animations;

    public enum Courses
    {
        appetizer = 1,
        entrée = 2,
        mainCourse = 3,
        dessert = 4
    }

    public enum InputType
    {
        Tap = 1,
        Twist = 2,
        L2R2 = 3
    }

}
