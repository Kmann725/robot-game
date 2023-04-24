using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    public Enemy[] enemiesToDisable;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableEnemies());
    }

    IEnumerator DisableEnemies()
    {
        for(int i=0; i<enemiesToDisable.Length;i++)
        {
            yield return new WaitForSeconds(1);
            enemiesToDisable[i].TakeDamage(1000);
        }
        yield return new WaitForSeconds(2);
        GameController.Instance.TextBox.gameObject.SetActive(false);
        GameController.Instance.GameWon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
