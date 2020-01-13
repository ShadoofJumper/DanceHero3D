using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinTypeConst coinType = CoinTypeConst.Red;
    private bool isDrop = false;
    private float coinSpeed;
    private Vector3 start;
    private Vector3 finish;
    private GameObject parentObject;

    public void SetParametrsCoin(Vector3 start, Vector3 finish, CoinTypeConst coinType, GameObject parentObject, float coinSpeed = 1.0f)
    {
        this.coinSpeed = coinSpeed;
        this.start = start;
        this.finish = finish;
        this.coinType = coinType;
        this.parentObject = parentObject;
    }

    public CoinTypeConst GetRandomCoinType()
    {
        // get all coins types
        var allCoinTypes = System.Enum.GetValues(typeof(CoinTypeConst));
        // get random coin type each iteration
        int randCoinTypeId = System.Convert.ToInt32(Random.RandomRange(0.0f, 2.0f));
        CoinTypeConst coinType = (CoinTypeConst)allCoinTypes.GetValue(randCoinTypeId);

        return coinType;
    }

    public void DropCoin()
    {
        isDrop = true;
    }
    public void DeletCoin()
    {
        Destroy(this.gameObject);
    }
    public void CollectCoin()
    {
        Debug.Log("Collect");
        Managers.Score.IncreasScore();
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // set start position
        transform.position = new Vector3(start.x, start.y, start.z);//start;
        transform.eulerAngles = new Vector3(-30, 0, 0);
        // set parent for cube
        transform.SetParent(parentObject.transform);

        // set coin color
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (coinType == CoinTypeConst.Red)
        {
            meshRenderer.material.color = Color.red;
        }
        else if (coinType == CoinTypeConst.Blue)
        {
            meshRenderer.material.color = Color.blue;
        }
        else if (coinType == CoinTypeConst.Green)
        {
            meshRenderer.material.color = Color.green;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        if (isDrop)
        {
            movement.y -= coinSpeed;
            transform.localPosition += movement * Time.deltaTime;// * coinSpeed
            Debug.Log("y: " + transform.localPosition.y);
            Debug.Log("x: " + transform.localPosition.x);
            Debug.Log("z: " + transform.localPosition.z);
        }

        // destroy coin if it under finish
        //if (transform.position.y < (this.finish.y - 1)) //for offset
        //{
        //    Managers.Score.FailScore();
       //     Destroy(this.gameObject);
        //}
    }

}
