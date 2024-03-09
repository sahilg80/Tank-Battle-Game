
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool isCollided;
    [SerializeField]
    private ParticleSystem fireParticleSystem;

    private void OnEnable()
    {
        isCollided = false;
    }

    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Player") && !isCollided)
        {
            ObjectPoolManager.Instance.DeSpawnObject(this.gameObject);
            isCollided = true;
            if (fireParticleSystem != null)
            {
                GameObject bulletParticleEffectObject = ObjectPoolManager.Instance.SpawnObject(fireParticleSystem.gameObject);
                bulletParticleEffectObject.transform.position = collision.GetContact(0).point;
                bulletParticleEffectObject.GetComponent<ParticleSystem>().Play();
                ObjectPoolManager.Instance.DeSpawnObjectWithDelay(bulletParticleEffectObject,3f);
            }
        }
    }
}
