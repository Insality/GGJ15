using Assets.Scripts.ProjectileScripts;
using UnityEngine;

namespace Assets.Scripts {
    public class WallCollider: MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Projectile"){
                other.gameObject.GetComponent<MainProjectileLogic>().DestroyProjectile();
            }
        }
    }
}