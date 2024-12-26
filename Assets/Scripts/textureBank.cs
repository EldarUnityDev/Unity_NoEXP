using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textureBank : MonoBehaviour
{
    public List<RenderTexture> targetTextures = new List<RenderTexture>(); //рамки

   // public RenderTexture targetTexture1;
    //public RenderTexture targetTexture2;
    //public RenderTexture targetTexture3;
   // public RenderTexture targetTexture4;
    // Start is called before the first frame update
    void Start()
    {
        References.textureCamBank = this;
        //1- присвоить текстуру нужной камере
        //2- активировать и переместить нужное изображение с текстурой
    }
}
