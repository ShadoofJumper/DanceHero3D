using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPoint : MonoBehaviour
{
    //radius of detection coin
    private float radius = 0.3f;
    private float pressScaleK = 0.9f;
    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }


    public void PressButton()
    {
        //change button size for intereactive feedback
        transform.localScale = startScale * pressScaleK;
        //check coins near
        Collider[] coinConnect = CheckConnecting();

        if (coinConnect.Length == 0)
        {
            Managers.Score.FailScore();
        }
    }
    public void UnpressButtton()
    {
        transform.localScale = startScale;

    }

    private Collider[] CheckConnecting()
    {
        //OverlapSphere return all object in range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            // if have then Collect coin
            //
            
            if (hitCollider.gameObject.GetComponent<Coin>())
            {
                hitCollider.gameObject.GetComponent<Coin>().CollectCoin();
            }
        }

        return hitColliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
