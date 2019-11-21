using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    private ConnectPoint childConnector;
    private Vector3 startButtonScale;
    public float kScale = 0.05f;

    void Start()
    {
        startButtonScale = transform.localScale;
    }

    public void SetChildConnector(ConnectPoint connector)
    {
        childConnector = connector;
    }

    public void OnMouseDown()
    {
        PressButton();
        childConnector.PressButton();
    }

    public void OnMouseUp()
    {
        UnpressButtton();
        childConnector.UnpressButtton();

    }
    public void PressButton()
    {
        float scaleKPositiv = 1 - kScale;
        //change button size for intereactive feedback
        transform.localScale = new Vector3(startButtonScale.x * scaleKPositiv, startButtonScale.y * scaleKPositiv, startButtonScale.z * scaleKPositiv);
    }

    public void UnpressButtton()
    {
        float scaleKNegative = 1 + kScale;
        transform.localScale = new Vector3(startButtonScale.x * scaleKNegative, startButtonScale.y * scaleKNegative, startButtonScale.z * scaleKNegative); ;
    }


}
