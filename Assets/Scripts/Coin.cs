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
    private float TIME_DESTROY = 3.0f;
    private Rigidbody selfRigidbody;

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
        //disable rigindbody gravity
        
        selfRigidbody = gameObject.GetComponent<Rigidbody>();
        selfRigidbody.useGravity = false;

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
        }

        // destroy coin if it under finish
        if (transform.localPosition.y < finish.y) //for offset
        {
            Managers.Score.FailScore();
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            // add timer for destroy element after it fall
            StartCoroutine(DestroyCoinAfterFall());
        }
    }

    private IEnumerator DestroyCoinAfterFall()
    {
        Vector3 torque;
        torque.x = Random.Range(-200, 200);
        torque.y = Random.Range(-200, 200);
        torque.z = Random.Range(-200, 200);
        gameObject.GetComponent<ConstantForce>().torque = torque;

        // add force for rigidbody
        //selfRigidbody.AddForce(impulseVector, ForceMode.Impulse);

        //wait time
        yield return new WaitForSeconds(TIME_DESTROY);

        Destroy(this.gameObject);
        yield return null;
    }

    

}
