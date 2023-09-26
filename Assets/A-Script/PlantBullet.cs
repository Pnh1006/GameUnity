using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlantBullet : Bullet
    {
        protected override void SpawnPiece()
        {
            base.SpawnPiece();
            ParticleSystem plbullet = ObjectPool.instance.GetPoolObject("PlantBreakBl").GetComponent<ParticleSystem>();
            plbullet.gameObject.SetActive(true);
            plbullet.Play();
            plbullet.transform.position = transform.position;
            ObjectPool.instance.SetActive(2, plbullet.gameObject);
        }

      
    }
}