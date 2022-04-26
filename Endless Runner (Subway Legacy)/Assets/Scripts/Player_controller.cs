/*
*	TickLuck
*	All rights reserved
*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player_controller : MonoBehaviour
{
    #region Fields
    [SerializeField] private float speed;
    [SerializeField] private Vector3 slide_collider_center;
    [SerializeField] private float slide_collider_height;

    private CapsuleCollider player_collider;
    private CharacterController controller;
    private Vector3 direction;

    private int lineToMove = 1;
    public float lineDistance = 4;

    [SerializeField] private float jump_force;
    [SerializeField] private float gravity;

    [SerializeField] private GameObject GO_UI;
    [SerializeField] private Text coins_txt;
    [SerializeField] private int coins;

    private Vector3 targetPositionBuffer;
    private Vector3 diff;
    private Vector3 moveDir;
    private const float max_speed = 110f;
    #endregion

    #region Unity Methods
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        player_collider = GetComponent<CapsuleCollider>();
        GO_UI.SetActive(false);
        Time.timeScale = 1f;

        StartCoroutine(SpeedIncrease());
    }

    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
        }

        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        if (SwipeController.swipeUp)
        {
            Jump();
        }

        if (SwipeController.swipeDown)
        {
            StartCoroutine(Slide());
        }

        targetPositionBuffer = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lineToMove == 0)
            targetPositionBuffer += Vector3.left * lineDistance;
        if (lineToMove == 2)
            targetPositionBuffer += Vector3.right * lineDistance;

        if (transform.position == targetPositionBuffer)
            return;

        diff = targetPositionBuffer - transform.position;
        moveDir = diff.normalized * 25 * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);

    }

    private void FixedUpdate()
    {
        direction.z = speed;
        direction.y += gravity * Time.fixedDeltaTime;
        controller.Move(direction * Time.fixedDeltaTime);
    }
    #endregion

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle")
        {
            GO_UI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            other.gameObject.SetActive(false);
            coins++;

            coins_txt.text = coins.ToString();
        }
    }

    private void Jump()
    {
        if (controller.isGrounded)
            direction.y = jump_force;
    }

    private IEnumerator Slide()
    {
        player_collider.center = slide_collider_center;
        player_collider.height = slide_collider_height;

        yield return new WaitForSeconds(1);

        // Default Numbers
        player_collider.center = Vector3.zero;
        player_collider.height = 2;
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(4);

        if (speed < max_speed)
        {
            speed += 3;
            StartCoroutine(SpeedIncrease());
        }
    }
}
