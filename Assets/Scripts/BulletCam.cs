using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCam : MonoBehaviour
{
    public RenderTexture targetTexture;
    public Camera myCam;
    public GameObject myCamGameObject;
    // Start is called before the first frame update
    void Start()
    {
        TurnMyCamOn();
    }
    public void TurnMyCamOn()
    {
        if (myCam != null)
        {
            // 1 - активируем ImgForCamEnemyN + CamBorder
            //References.canvas.GetComponent<EnemyCamSettings>().turnOnTheCamera();
            // 2 - достаём текстуру из банка (лучше бы загружали из библиотеки)
            // 3 - присвоение текстуры нашей камере
            //! myCam.targetTexture = References.textureCamBank.targetTextures[References.sniperCamOrderTextures[1]];
            myCam.targetTexture = gameObject.transform.parent.gameObject.transform.parent.GetComponentInParent<SniperBehaviour>().targetTexture;
            transform.parent = null;
            //Debug.Log("i took bank: " + References.sniperCamTexture.name);
            //Debug.Log("now is: " + myCam.targetTexture.name);
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }

    }
    private void OnDestroy()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
    }
}
