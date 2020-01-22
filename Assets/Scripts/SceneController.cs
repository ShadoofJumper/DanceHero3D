using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public int rowCount = 3;
    public float offset = 2;
    public float coinSpeed = 1.0f;
    private Line[] linesArray;
    // position of line
    public float startX = -1.5f;
    public float startY = -2.0f;
    public float yCoordinatForDestroyCoin = -4.75f;
    public bool gameInProgress = false;

    [SerializeField] private Coin coinPrefab;
    [SerializeField] private Line linePrefab;
    [SerializeField] private GameObject Field;
    [SerializeField] private GameObject PlaceToDropCoins;

    private bool gameEnd = false;
    private Coin currentCoin;

    // Start is called before the first frame update
    void Start()
    {
        linesArray = new Line[rowCount];
        // set field angle
        Field.transform.eulerAngles = new Vector3(60, 0, 0);
        Field.transform.position = new Vector3(0, 0, 0);

        //create lines
        for (int i = 0; i < rowCount; i++)
        {
            linesArray[i] = Instantiate(linePrefab) as Line;
            // set parent for line
            linesArray[i].transform.SetParent(Field.transform);

            float lineSizeZ = linesArray[i].transform.localScale.z;

            // set star position of line
            linesArray[i].transform.position = new Vector3(0, 0, 0);//lineSizeZ * -1
            // set start angle of line
            linesArray[i].transform.eulerAngles = new Vector3(60, 0, 0);

        }

        for (int i = 0; i < rowCount; i++)
        {
            float lineSizeZ = linesArray[i].transform.localScale.z;
            linesArray[i].transform.localPosition = new Vector3(startX + offset * i, startY, lineSizeZ * -1.5f);
        }
     }

    public Line[] GetLinesArray()
    {
        return linesArray;
    }


    public void StartGame()
    {
        gameInProgress = true;

        // play music
        Managers.Sound.PlayLevelMusic();

        //create lines
        for (int i = 0; i < rowCount; i++)
        {
            StartCoroutine(StartLineDrop(i));
        }
    }

    public void ChangeGameEnd(bool _gameEnd, bool _gameInProgress)
    {
        gameEnd = _gameEnd;
        gameInProgress = _gameInProgress;
    }
    public void StopGame()
    {
        gameEnd = true;
        StartCoroutine(StopMusicLast());
    }

    private IEnumerator StopMusicLast()
    {
        yield return new WaitForSeconds(5.0f);

        Managers.Sound.StopMusic();
    }

    private IEnumerator StartLineDrop(int lineId)
    {
        // Get lite top point
        //float lineTopPoint = linesArray[lineId].GetLineTopPoint(startY);
        // calculte start and finish
        float x = startX + offset * lineId;
        float y = PlaceToDropCoins.transform.position.y;
        float z = PlaceToDropCoins.transform.position.z;

        Vector3 start = new Vector3(x, y, z);
        Vector3 finish = new Vector3(x, yCoordinatForDestroyCoin, 0);
        while (!gameEnd)
        {
            //wait random time
            yield return new WaitForSeconds(Random.RandomRange(1.0f, 3.0f));
            if (gameEnd == true)
            {
                break;
            }
            // create coin
            currentCoin = Instantiate(coinPrefab) as Coin;

            CoinTypeConst coinType = currentCoin.GetRandomCoinType();

            // set coin parametrs
            currentCoin.SetParametrsCoin(start, finish, coinType, Field, coinSpeed);
            //drop coin, set start and finish for droped coin
            currentCoin.DropCoin();


        }
    }
}
