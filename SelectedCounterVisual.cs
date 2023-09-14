using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    // Start is called before the first frame update[SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    [SerializeField] private BaseCounter baseCounter;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)//
        {
            show();
        }
        else
        {
            hide();
        }
    }

    private void show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }

    }

    private void hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
