using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyAnimation : MonoBehaviour {

    [SerializeField] private GameObject m_FocusCamera;
    [SerializeField] private Animator m_FadeAnimation;
    [SerializeField] private Image m_FadeImage;
    [SerializeField] private GameObject m_KeyAnimationObject;
    [SerializeField] private Animator m_KeyAnimations;

    public Animator FadeAnimation
    {
        get
        {
            return m_FadeAnimation;
        }

        set
        {
            m_FadeAnimation = value;
        }
    }
    public Image FadeImage
    {
        get
        {
            return m_FadeImage;
        }

        set
        {
            m_FadeImage = value;
        }
    }
    public GameObject FocusCamera
    {
        get
        {
            return m_FocusCamera;
        }

        set
        {
            m_FocusCamera = value;
        }
    }
    public GameObject KeyAnimationObject
    {
        get
        {
            return m_KeyAnimationObject;
        }

        set
        {
            m_KeyAnimationObject = value;
        }
    }
    public Animator KeyAnimations
    {
        get
        {
            return m_KeyAnimations;
        }

        set
        {
            m_KeyAnimations = value;
        }
    }

    public void StartAnimation() {
        StartCoroutine("PlayKeyAnimation");
    }

    public IEnumerator PlayKeyAnimation() {
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        FadeAnimation.SetBool("Fade", true);
        yield return new WaitUntil(() => FadeImage.color.a == 1);
        FadeAnimation.SetBool("Fade", false);
        player.PlayerCamera.SetActive(false);
        m_FocusCamera.SetActive(true);
        m_KeyAnimationObject.SetActive(true);


        yield return new WaitForEndOfFrame();

    }
}
