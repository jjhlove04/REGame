using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretShooting : Turret
{
    //[SerializeField]
    //private Transform firePosition;
    //[SerializeField]
    //private float bulletLineEffectTime = 0.03f;

    //private float curshootTimer;

    //[SerializeField]
    //private bool onPlayer = false;

    /*private void Shot()
    {
        if (curshootTimer >= 0)
        {
            curshootTimer -= Time.deltaTime;
        }

        if (targetEnemychil != null && curshootTimer <= 0)
        {
            RaycastHit hit;
            Vector3 hitPosition = Vector3.zero;
            if (Physics.Raycast(
                    firePosition.position,
                    firePosition.forward, out hit, maxDistance))
            {
                hitPosition = hit.point;
            }
            else
            {
                hitPosition = firePosition.position
                                + firePosition.forward * maxDistance;
            }
            curshootTimer = shootTimerMax;
            curBulletAmount--;
            //StartCoroutine(ShotEffect(hitPosition));

        }
    }*/

    //private IEnumerator ShotEffect(Vector3 hitPosition)
    //{
    //    muzzleflash.Play();
    //    bulletLineRenderer.SetPosition(1,bulletLineRenderer.transform.InverseTransformPoint(hitPosition));
    //    bulletLineRenderer.gameObject.SetActive(true);
    //    yield return new WaitForSeconds(bulletLineEffectTime);
    //    bulletLineRenderer.gameObject.SetActive(false);
    //    muzzleflash.Stop();
    //}
}
