using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{

    public Image filledPart;
    public Image background;

    public void ShowHealthFraction(float fraction)
    {
        //filledPart.rectTransform.localScale.y, filledPart.rectTransform.localScale.z можно написать вместо 1,1
        filledPart.rectTransform.localScale = new Vector3(fraction, 1, 1);
        if(fraction < 1)
        {
            filledPart.enabled = true;
            background.enabled = true;

        }
    }

    void Start()
    {
        filledPart.enabled = false;
        background.enabled = false;
    }
}
