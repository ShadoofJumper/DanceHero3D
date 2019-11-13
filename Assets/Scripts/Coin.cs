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

    public void SetParametrsCoin(Vector3 start, Vector3 finish, CoinTypeConst coinType, float coinSpeed = 1.0f)
    {
        this.coinSpeed = coinSpeed;
        this.start = start;
        this.finish = finish;
        this.coinType = coinType;
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
        transform.position = start;
        // set coin color
        SpriteRenderer lineSprite = GetComponent<SpriteRenderer>();
        if (coinType == CoinTypeConst.Red)
        {
            lineSprite.color = Color.red;
        }
        else if (coinType == CoinTypeConst.Blue)
        {
            lineSprite.color = Color.blue;
        }
        else if (coinType == CoinTypeConst.Green)
        {
            lineSprite.color = Color.green;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        if (isDrop)
        {
            movement.y -= coinSpeed;
            transform.position += movement * Time.deltaTime;// * coinSpeed 
        }

        // destroy coin if it under finish
        if (transform.position.y < (this.finish.y - 1)) //for offset
        {
            Managers.Score.FailScore();
            Destroy(this.gameObject);
        }
    }

}
