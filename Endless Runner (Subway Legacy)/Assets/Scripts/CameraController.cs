/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Fields
    [SerializeField] private Transform player;
    private Vector3 offset;

    private Vector3 new_position_buffer;
    #endregion

    #region Unity Methods
    void Start()
    {
        offset = transform.position - player.position;
    }

    void FixedUpdate()
    {
        new_position_buffer = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z);
        transform.position = new_position_buffer;
    }
    #endregion
}
