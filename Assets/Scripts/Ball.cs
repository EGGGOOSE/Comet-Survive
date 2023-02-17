using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{


    public AudioClip sound;
    public float energyChange;
    public float moneyChange;
    public float pushForce;
    public Color particlesColor;
    public bool isRandomBall = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.isDefeat)
        {
            return;
        }

        if (collider.tag == "Comet")
        {
            Comet comet = gameManager.comet;
            int moneyInscrease = (int)(moneyChange * (1 + Score.score / 100f));

            comet.Combo++;

            gameManager.Energy += energyChange;

            if (!isRandomBall)
            {
                if (pushForce > 0 && comet.rb.velocity.y < 0 || pushForce < 0 && comet.rb.velocity.y > 0)
                {
                    comet.rb.velocity = new Vector2(comet.rb.velocity.x, 0);
                }

                comet.Push(new Vector2(0, pushForce));
            }
            else
            {
                float Rotation = (transform.GetChild(0).eulerAngles.z + 90) * Mathf.Deg2Rad;

                Vector2 pushForceVector = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)) * pushForce;
                if (pushForceVector.y > 0 && comet.rb.velocity.y < 0 || pushForceVector.y < 0 && comet.rb.velocity.y > 0)
                {
                    comet.rb.velocity = new Vector2(comet.rb.velocity.x, 0);
                }
                if (pushForceVector.x > 0 && comet.rb.velocity.x < 0 || pushForceVector.x < 0 && comet.rb.velocity.x > 0)
                {
                    comet.rb.velocity = new Vector2(0, comet.rb.velocity.y);
                }

                comet.Push(pushForceVector);
            }

            GameObject curParticles = Instantiate(comet.particles, transform.position, Quaternion.identity);
            curParticles.GetComponent<ParticleSystem>().startColor = particlesColor;
            Destroy(curParticles, 1f);


            if (comet.Combo > 1)
            {
                moneyInscrease *= comet.Combo;
            }

            Money.money += moneyInscrease;

            GameObject floatingPoints = Instantiate(gameManager.floatingPoints, transform.position, Quaternion.identity);
            floatingPoints.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + moneyInscrease;
            Destroy(floatingPoints, 1f);
            gameManager.soundSource.PlayOneShot(sound);

            Destroy(gameObject);
        }
    }
}
