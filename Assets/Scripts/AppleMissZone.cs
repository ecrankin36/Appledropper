using UnityEngine;

public class AppleMissZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple"))
        {
            //collision.tag = "Untagged";
			//Destroy(collision.gameObject); // remove the apple
            FindObjectOfType<GameManager>().LoseBasket();
        }
    }
}
