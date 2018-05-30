using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMe : MonoBehaviour {

    public AnimationClip idleAnimation;
    public AnimationClip walkAnimation;
    private Transform targetCamera;
    bool isFollowMe = false;
    bool isMoving = false;
    float speed = 0f;
    const float lerpFactor = 100f;

    //インスペクタ代入時チェック
    private void OnValidate()
    {
        if (!CheckAnimationValidate(idleAnimation))
        {
            idleAnimation = null;
        }
        if (!CheckAnimationValidate(walkAnimation))
        {
            walkAnimation = null;
        }
    }

    bool CheckAnimationValidate(AnimationClip clip)
    {
        var animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.Log("This gameobject have to attach Animator component");
            return false;
        }

        if (clip != null)
        {
            if (animator.isHuman && !clip.isHumanMotion)
            {
                Debug.Log("This model is humanoid. Animation must be humanoid animation");
                return false;
            }
            else if (!animator.isHuman && clip.isHumanMotion)
            {
                Debug.Log("This model is generic. Animation must be generic animation");
                return false;
            }
            else if (!clip.isLooping)
            {
                Debug.Log("Animation must be loop animation");
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        if (isFollowMe)
        {
            Transform parentT = transform.parent;
            Vector2 horizModelPos = new Vector2(parentT.position.x, parentT.position.z);
            Vector2 horizCameraPos = new Vector2(targetCamera.position.x, targetCamera.position.z);
            float distance = Vector2.Distance(horizModelPos, horizCameraPos);
            if (isMoving)//歩いてる
            {
                //水平距離が1m未満になると止まる
                if (distance < 1f)
                {
                    isMoving = false;
                }
            }
            else//Idle中
            {
                //水平距離1.5m以上離れると接近する
                if (distance > 1.5f)
                {
                    isMoving = true;
                }
            }

            //スピード
            if (isMoving)
            {
                speed = Mathf.Lerp(speed, 0.05f, Time.deltaTime * lerpFactor);
            }
            else
            {
                speed = Mathf.Lerp(speed, 0f, Time.deltaTime * lerpFactor);
            }

            //向き
            parentT.LookAt(new Vector3(targetCamera.position.x, parentT.position.y, targetCamera.position.z));
            //前進
            parentT.position = parentT.position + transform.forward * transform.lossyScale.x * speed;
            //アニメーターパラメータ
            GetComponent<Animator>().SetFloat("Speed", speed);
        }
    }

    public void StartFollowMe(Transform cameraT)
    {
        targetCamera = cameraT;
        isFollowMe = true;
        isMoving = false;
        speed = 0f;
    }

    public void EndFollowMe()
    {
        targetCamera = null;
        isFollowMe = false;
    }
}
