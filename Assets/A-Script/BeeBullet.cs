using UnityEngine;

namespace DefaultNamespace
{
    public class BeeBullet : Bullet
    {
        protected override void SpawnPiece()
        {
            base.SpawnPiece();
            ParticleSystem plbullet = ObjectPool.instance.GetPoolObject("BeeBreakBl").GetComponent<ParticleSystem>();
            plbullet.gameObject.SetActive(true);
            plbullet.Play();
            plbullet.transform.position = transform.position;
            ObjectPool.instance.SetActive(2, plbullet.gameObject);
        }
    }
}