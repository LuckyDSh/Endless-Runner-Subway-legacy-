/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    #region Fields
    [SerializeField] private Transform player;
    private Text score_txt;
    #endregion

    #region Unity Methods
    void Start()
    {
        score_txt = GetComponent<Text>();
    }

    void Update()
    {
        score_txt.text = ((int)(player.position.z / 2)).ToString();
    }
    #endregion
}
