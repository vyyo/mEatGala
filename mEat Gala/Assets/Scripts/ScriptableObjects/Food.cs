using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Food", order = 0)]
public class Food : ScriptableObject
{
    public Courses course;
    public Sprite foodSprite;
    public int resistance = 1;
    public int saturation = 1;
    public AnimatorController animations;

    public enum Courses
    {
        appetizer = 1,
        entrée = 2,
        mainCourse = 3,
        dessert = 4
    }

    /*public enum InputType
    {
        tap = 1,
        L2R2 = 2,
        Twist = 3
    }
    */

}
