using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cat", menuName = "Cat")]
public class Cats : ScriptableObject
{
    public string catName;
    public bool obtained;
    public int catID;
    public Sprite spr;
    public Sprite closedEyesSpr;
    public Sprite boxSprite;
    public Sprite houseSprite;
    
}
