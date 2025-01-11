using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedTimerSlider : MonoBehaviour
{
    public float timerDuration;
    public Slider timerSlider;

    public void ShowTime(float fraction)
    {
        timerSlider.value = fraction;
    }

}