
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem fireParticleSystem;

    private void OnEnable()
    {

    }

    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectPoolManager.Instance.DeSpawnObject(this.gameObject);

        if (fireParticleSystem != null)
        {
            GameObject bulletParticleEffectObject = ObjectPoolManager.Instance.SpawnObject(fireParticleSystem.gameObject);
            bulletParticleEffectObject.transform.position = collision.GetContact(0).point;
            bulletParticleEffectObject.GetComponent<ParticleSystem>().Play();
            ObjectPoolManager.Instance.DeSpawnObjectWithDelay(bulletParticleEffectObject, 3f);
        }
    }
}
