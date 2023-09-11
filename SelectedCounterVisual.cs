using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    // Start is called before the first frame update[SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    [SerializeField] private ClearCounter clearCounter;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }
    
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)//
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
        visualGameObject.SetActive(true);
    }
    
    private void hide()
    {
        visualGameObject.SetActive(false);
    }
}
