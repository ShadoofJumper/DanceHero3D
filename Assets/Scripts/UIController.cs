using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] UIButton buttonActionOrigin;
    [SerializeField] ConnectPoint connectionPointOrigin;
    [SerializeField] private Text Score;
    [SerializeField] private Text mainTime;
    [SerializeField] private Button startGame;
    [SerializeField] private Button resetGame;
    [SerializeField] private GameObject placeForButtons;

    public float currentTime = 60.0f;
    private bool canCount = true;

    private float startX = -2.0f;
    private float startY = -2.0f;
    private int rowCount = 3;
    private float offset = 2;

    public Color[] buttonColorArray = { Color.green, Color.green, Color.green, };
    public string[] buttonTextArray = { "aa", "ss", "dd", };
    public KeyCode[] keyCodeAction = { KeyCode.A, KeyCode.S, KeyCode.D };
    private UIButton[] buttonArray;
    private ConnectPoint[] connectArray;
    private SceneController sceneControll;

    // Start is called before the first frame update
    void Start()
    {
        sceneControll = this.GetComponent<SceneController>();
        // get start parametrs from scene controll
        this.startX = sceneControll.startX;
        this.startY = sceneControll.startY;
        this.rowCount = sceneControll.rowCount;
        this.offset = sceneControll.offset;

        buttonArray = new UIButton[rowCount];
        connectArray = new ConnectPoint[rowCount];

        for (int buttonId = 0; buttonId < rowCount; buttonId++)
        {
            //create trigger point
            CreateTriggerPoint(buttonId);
            //create action buttons
            CreateActionButton(buttonId);
        }
    }


    public void StartGame()
    {
        sceneControll.ChangeGameEnd(false, true);

        sceneControll.StartGame();
        startGame.enabled = false;
        canCount = true;
    }

    public void ResetGame()
    {
        startGame.enabled = true;
        sceneControll.ChangeGameEnd(true, false);
        //Coin[] allCoins = GameObject.FindObjectsOfType(typeof(Coin));
        Coin[] allCoins = UnityEngine.Object.FindObjectsOfType<Coin>();

        foreach (Coin coin in allCoins)
        {
            Debug.Log("Coin: " + coin.name);
            coin.DeletCoin();
        }
        //sceneControll.StartGame();
        Managers.Sound.StopMusic();
        Managers.Score.ClearScore();
        currentTime = 60.0f;
        canCount = false;
        OnTimePlayUpdate();
        OnScoreUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        for (int buttonId = 0; buttonId < rowCount; buttonId++)
        {
            // if press action key
            if (Input.GetKeyDown(keyCodeAction[buttonId]))
            {
                buttonArray[buttonId].PressButton();
                connectArray[buttonId].PressButton();
            }
            else if (Input.GetKeyUp(keyCodeAction[buttonId]))
            {
                buttonArray[buttonId].UnpressButtton();
                connectArray[buttonId].UnpressButtton();
            }
        }
        if (sceneControll.gameInProgress)
        {
            if (currentTime >= 0.0f && canCount)
            {
                OnTimePlayUpdate();
            }
            else if (currentTime <= 0.0f)
            {
                canCount = false;
                mainTime.text = "Time: 0.00";
                this.GetComponent<SceneController>().StopGame();
            }
        }
    }

    void CreateActionButton(int buttonId)
    {
        // create buttons for lines
        UIButton buttonAction = Instantiate(buttonActionOrigin) as UIButton;
        // add to array
        buttonArray[buttonId] = buttonAction;
        // place button
        // get place for buttons position
        Vector3 placePos = placeForButtons.transform.position;

        float x = placePos.x - 2.0f + offset * buttonId;
        float y = placePos.y; // place under line start
        float z = placePos.z;
        buttonAction.transform.position = new Vector3(x, y, z);
        // set button color
        buttonAction.GetComponent<MeshRenderer>().material.color = buttonColorArray[buttonId];
        // set button text
        // get text object of button
        GameObject text = buttonActionOrigin.transform.GetChild(0).gameObject;
        //set text
        TextMesh textMesh = text.GetComponent<TextMesh>();
        textMesh.text = buttonTextArray[buttonId];
        // set bitton trigger object
        buttonAction.SetChildConnector(connectArray[buttonId]);
    }

    void CreateTriggerPoint(int buttonId)
    {
        // create connect area for button
        ConnectPoint connectArea = Instantiate(connectionPointOrigin) as ConnectPoint;
        connectArray[buttonId] = connectArea;
        // place area
        float x = startX + offset * buttonId;
        float y = startY + 0.38f; // place upper button
        float z = 0;
        connectArea.transform.position = new Vector3(x, y, z);
        //set area color
        connectArea.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 1.0f, 0.0022f, 0.5f);
    }

    public void OnScoreUpdate()
    {
        string message = "Score: " + Managers.Score.currentScore;
        if (Managers.Score.bonusStep > 1)
        {
            message = message + " x" + Managers.Score.bonusStep + "!";
        }
        Score.text = message;
    }

    void OnTimePlayUpdate()
    {
        currentTime -= Time.deltaTime;
        string message = "Time: " + currentTime.ToString("F");
        mainTime.text = message;
    }

}
