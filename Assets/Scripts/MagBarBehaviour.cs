using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagBarBehaviour : MonoBehaviour
{
    public Image filledPart;

    public void ShowMagFraction(float fraction)
    {
        //filledPart.rectTransform.localScale.y, filledPart.rectTransform.localScale.z можно написать вместо 1,1
        filledPart.rectTransform.localScale = new Vector3(1, fraction, 1);
    }
}
