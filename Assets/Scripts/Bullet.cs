
using Assets.Scripts.Views;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject PlayerFrom { get; set; }
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
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        if(health != null)
        {
            health.TakeDamage(PlayerFrom, 6f);
        }
        Destroy(this.gameObject);
        //BaseView coll = collision.transform.GetComponent<BaseView>();
        //coll?.RecieveDamage(34);
        
        //ObjectPoolManager.Instance.DeSpawnObject(this.gameObject);

        //if (fireParticleSystem != null)
        //{
        //    GameObject bulletParticleEffectObject = ObjectPoolManager.Instance.SpawnObject(fireParticleSystem.gameObject);
        //    bulletParticleEffectObject.transform.position = collision.GetContact(0).point;
        //    bulletParticleEffectObject.GetComponent<ParticleSystem>().Play();
        //    ObjectPoolManager.Instance.DeSpawnObjectWithDelay(bulletParticleEffectObject, 3f);
        //}
    }
}
