
using Assets.Scripts;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem fireParticleSystem;
    private float damageValue;

    private void OnEnable()
    {

    }

    private void Update()
    {

    }

    public void SetDamage(float value)
    {
        damageValue = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        BaseView atack = collision.transform.GetComponentInParent<BaseView>();

        if (atack != null)
        {
            atack.OnAttacked(damageValue);
        }

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
