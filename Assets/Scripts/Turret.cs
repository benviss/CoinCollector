using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
  private Player player;
  Vector3 adjustedPlayerPosition;

  private GameObject target;
  public GameObject muzzle;
  public GameObject projectileSpawn;
  public GameObject projectile;

  public float radius;
  public float power;
  public float aimLead;
  public float difficultyRating;

  public ParticleSystem particles;

  public float chargeTime;
  float currentCharge;

  // Use this for initialization
  void Start()
  {
    player = FindObjectOfType<Player>();
    SetTarget(player.gameObject);
  }

  // Update is called once per frame
  void Update()
  {
    if(!GameManager.Instance.playerIsAlive) return;
    float distanceToPlayer = (transform.position - target.transform.position).magnitude;
    float aimLead = GameManager.Instance.scrollSpeed * (distanceToPlayer / power);

    //within range, lets kill a bitchs
    if (player && distanceToPlayer < radius) {
      adjustedPlayerPosition = new Vector3(player.transform.position.x + aimLead, player.transform.position.y, player.transform.position.x);

      Vector3 dir = adjustedPlayerPosition - muzzle.transform.position;
      float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;

      muzzle.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

      if (currentCharge > chargeTime) {
        Fire();
        currentCharge = 0;
      }

      currentCharge += Time.deltaTime;
    }

    if ((currentCharge / chargeTime) > .5) {
      particles.Play();
      particles.startSpeed = -currentCharge * 4f;
    } else {
      particles.Stop();
      particles.startSpeed = 0;
    }
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(transform.position, radius);
  }

  public void SetTarget(GameObject _target)
  {
    target = _target;
  }

  void Fire()
  {

    var newProjectile = (GameObject)Instantiate(projectile, projectileSpawn.transform.position, muzzle.transform.rotation);
    newProjectile.transform.parent = gameObject.transform;
    newProjectile.transform.Rotate(new Vector3(0, 0, 180));
    newProjectile.GetComponent<Projectile>().SetTarget(adjustedPlayerPosition);
    newProjectile.GetComponent<Projectile>().SetSpeed(power);
  }
}
