using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPoint : MonoBehaviour
{
    //radius of detection coin
    private float radius = 0.3f;

    public void PressButton()
    {
        //change button size for intereactive feedback
        transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        //check coins near
        Collider[] coinConnect = CheckConnecting();
        Debug.Log("" + coinConnect.Length);
        if (coinConnect.Length == 0)
        {
            Managers.Score.FailScore();
        }
    }
    public void UnpressButtton()
    {
        transform.localScale = new Vector3(5.0f, 5.0f, 5.0f); ;

    }

    private Collider[] CheckConnecting()
    {
        //OverlapSphere return all object in range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            // if have then Collect coin
            hitCollider.gameObject.GetComponent<Coin>().CollectCoin();
        }

        return hitColliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
