/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Coin : MonoBehaviour
{

    private void Start()
    {
        transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 40 * Time.fixedDeltaTime, 0);
    }

}
