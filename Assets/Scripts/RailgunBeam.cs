using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class RailgunBeam : BulletBehaviour
{
    public LineRenderer myBeam;
    // Start is called before the first frame update
    void Start()
    {
       // float secondsUntilDestroyedOri = secondsUntilDestroyed;
        float beamThickness = 0.5f;

        //Step 1 - fire a laser to nearest wall
        //Raycast(starting point, direction, ПЕРЕМЕННАЯ В К. СОДЕРЖ. ВСЯ ИНФА, distance to check, what we want to hit)

        Physics.SphereCast(transform.position, beamThickness, transform.forward, out RaycastHit hitInfo, References.maxDistanceInALevel, References.wallsLayer);
        float distanceToWall = hitInfo.distance;

        //Step 2 - fire a new laser nut now checking for enemies
        //для каждого элемента, который мы назовём enemyHitInfo, в списке выполнить "получить урон"
        //float beamThickness = 0.3f;
        //Для рейлгана, высчитываем всех на пути
        /*
        foreach (RaycastHit enemyHitInfo in Physics.SphereCastAll(transform.position, beamThickness, transform.forward, distanceToWall, References.enemyLayer))
        {
            enemyHitInfo.collider.GetComponentInParent<HealthSystem>().TakeDamage(bulletDamage);
        }
        */
        //для тейзера нужен только один:
        Physics.Raycast(transform.position, transform.forward, out RaycastHit firstEnemyHitInfo, distanceToWall, References.enemyLayer);
        if (firstEnemyHitInfo.collider != null && firstEnemyHitInfo.collider.GetComponentInParent<HealthSystem>() != null)
        {
            firstEnemyHitInfo.collider.GetComponentInParent<NavMeshAgent>().speed += 7;
            firstEnemyHitInfo.collider.GetComponentInParent<NavMeshAgent>().acceleration = firstEnemyHitInfo.collider.GetComponentInParent<NavMeshAgent>().speed;
            firstEnemyHitInfo.collider.GetComponentInParent<EnemyBehaviour>().overchargedStep += 1;
            firstEnemyHitInfo.collider.GetComponentInParent<EnemyBehaviour>().explodeOnTouch = true;
            firstEnemyHitInfo.collider.GetComponentInParent<HealthSystem>().currentHealth = 1;

        }

        //Step 3 - show the beam
        myBeam.SetPosition(0, transform.position);
        myBeam.SetPosition(1, hitInfo.point); //hitInfo.Point = where we hit the wall
    }

    // Update is called once per frame
     protected override void Update()
     {

         base.Update();//same ticking before death
        //Make our beam fade out over time
        
     }
    private void FixedUpdate()
    {
        myBeam.endColor = Color.Lerp(myBeam.endColor, Color.clear, 0.02f);
    }

}
