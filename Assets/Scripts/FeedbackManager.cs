using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class FeedbackManager : MonoBehaviour
{
    [SerializeField] public  VideoClip[] positiveClips;
    [SerializeField] public  VideoClip[] negativeClips;
    [SerializeField] public  VideoClip[] endClips;

    private VideoPlayer m_videoPlayer;
    private RawImage myImg;

    private void OnEnable()
    {
        GameManager.OnNumberPressed += PlayPositiveFeedback;
        GameManager.OnWrongNumberPressed += PlayNegativeFeedback;
    }

    private void OnDisable()
    {
        GameManager.OnNumberPressed -= PlayPositiveFeedback;
        GameManager.OnWrongNumberPressed += PlayNegativeFeedback;
    }

    // Start is called before the first frame update
    void Start()
    {
        myImg = GetComponent<RawImage>();
        myImg.color = new Color(1, 1, 1, 0);
        m_videoPlayer = GetComponent<VideoPlayer>();
        m_videoPlayer.targetCameraAlpha = 0;
    }

    /// <summary>
    /// Pick and plays a random clip from the positive collection.
    /// </summary>
    /// <param name="i"></param>
    public void PlayPositiveFeedback(int i)
    {
        StartCoroutine(PlayFeedback(positiveClips[Random.Range(0,positiveClips.Length)]));
    }

    /// <summary>
    /// Pick and plays a random clip from the negative collection.
    /// </summary>
    /// <param name="i"></param>
    public void PlayNegativeFeedback()
    {
        StartCoroutine(PlayFeedback(negativeClips[Random.Range(0, positiveClips.Length)]));
    }

    /// <summary>
    /// Menager method to display feedbacks with the perfect timing.
    /// </summary>
    /// <param name="clip"></param>
    /// <returns></returns>
    public IEnumerator PlayFeedback(VideoClip clip )
    {
        m_videoPlayer.clip = clip;
        yield return new WaitForSeconds(0.5f);
        m_videoPlayer.Play();
        myImg.color = new Color(1, 1, 1, 0.8f);
        yield return new WaitForSeconds((float)clip.length);
        myImg.color = new Color(1, 1, 1, 0);
        m_videoPlayer.Stop();
    }

}
