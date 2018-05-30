using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanFaceUpdate : MonoBehaviour
{
    public AnimationClip[] animations;
    public string[] animationNames;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    /*
    void OnGUI()
    {
        GUILayout.Box("Face Update", GUILayout.Width(170), GUILayout.Height(25 * (animations.Length + 2)));
        Rect screenRect = new Rect(10, 25, 150, 25 * (animations.Length + 1));
        GUILayout.BeginArea(screenRect);
        foreach (var animation in animations)
        {
            if (GUILayout.RepeatButton(animation.name))
            {
                anim.CrossFade(animation.name, 0);
            }
        }
        GUILayout.EndArea();
    }*/

    /*
    //アニメーションEvents側につける表情切り替え用イベントコール
    public void OnCallChangeFace(string str)
    {
        int ichecked = 0;
        foreach (var animation in animations)
        {
            if (str == animation.name)
            {
                ChangeFace(str);
                break;
            }
            else if (ichecked <= animations.Length)
            {
                ichecked++;
            }
            else
            {
                //str指定が間違っている時にはデフォルトで
                str = "default@unitychan";
                ChangeFace(str);
            }
        }
    }*/

    public void ChangeFace(int index)
    {
        //Debug.Log("ChangeFace:"+index);
        index %= animations.Length;

        ChangeFace(animations[index].name);
    }

    void ChangeFace(string str)
    {
        //isKeepFace = true;
        anim.CrossFade(str, 0, 1);
        anim.SetLayerWeight(1, 1f);
    }
}
