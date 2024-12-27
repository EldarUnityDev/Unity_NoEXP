using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


     //static - можно обращаться к скрипту через другие скрипты
public static class References //: MonoBehaviour - означает, что скрипт - компонент игрового объекта
{
    public static PlayerBehaviour thePlayer;
    public static CanvasBehaviour canvas;
    public static CanvasInWorldBehaviour canvasInWorld;
    public static List<SpawnerBehaviour> spawners = new List<SpawnerBehaviour>();
    public static List<EnemyBehaviour> allEnemies = new List<EnemyBehaviour>();
    public static List<Useable> useables = new List<Useable>();
    public static List<PlinthBehaviour> plinths = new List<PlinthBehaviour>();


    public static AlarmManager alarmManager;

    public static LevelManager levelManager;
    public static LevelGenerator levelGenerator;
    
    public static List<NavPoint> navPoints = new List<NavPoint>();

    public static CameraTools cameraTools;

    public static float maxDistanceInALevel = 1000;

    public static LayerMask wallsLayer = LayerMask.GetMask("Wall");
    public static LayerMask enemyLayer = LayerMask.GetMask("Enemy");
    public static LayerMask playerLayer = LayerMask.GetMask("Player");

    public static textureBank textureCamBank;
    public static EnemyCamSettings enemyCams;

    public static List<int> sniperCamOrderTextures = new List<int>();

    public static TriggerAnimation triggerAnimation;
    public static FingerAnimation fingerAnimation;

    public static Persistent essentials;
}
