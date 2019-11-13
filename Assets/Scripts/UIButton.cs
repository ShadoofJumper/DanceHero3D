using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    private ConnectPoint childConnector;

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
        //change button size for intereactive feedback
        transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
    }

    public void UnpressButtton()
    {
        transform.localScale = new Vector3(5.0f, 5.0f, 5.0f); ;
    }


}
