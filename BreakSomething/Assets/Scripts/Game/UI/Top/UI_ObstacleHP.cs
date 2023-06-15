using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_ObstacleHP : UIElement
{
    public TMP_Text current;
    public TMP_Text max;
    public GameObject bar;
    public Transform obstacles;

    private void Update()
    {
        if(obstacles.childCount < 1)
        {
            bar.SetActive(false);
            return;
        }
        else
        {
            bar.SetActive(true);

            var currentObstacle = obstacles.GetChild(0).GetComponent<Obstacle>();
            current.SetText(string.Format("{0:#,0}", currentObstacle.currentHP)) ;
            max.SetText(string.Format("{0:#,0}", currentObstacle.maxHP));
        }
    }
}