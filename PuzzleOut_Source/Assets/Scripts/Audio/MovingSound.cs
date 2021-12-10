using System.Collections;
using UnityEngine;

public class MovingSound : AudioActivation
{
    [SerializeField]
    private enum MoveDir
    {
        x,
        y,
        z,
        nx,
        ny,
        nz
    }

    [Header("Moving Direction")]
    [SerializeField] 
    private MoveDir moveDirection = MoveDir.x;
    [SerializeField] 
    private float moveSpeed = 1;
    [SerializeField] 
    private float existingTime = 8;

    //Object with sound moves to set direction when activated
    private void Update()
    {
        if (isActivated)
        {
            StartCoroutine(RemoveSource());

            if (moveDirection == MoveDir.x)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }

            if (moveDirection == MoveDir.y)
            {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            }

            if (moveDirection == MoveDir.z)
            {
                transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
            }

            if (moveDirection == MoveDir.nx)
            {
                transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
            }

            if (moveDirection == MoveDir.ny)
            {
                transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
            }

            if (moveDirection == MoveDir.nz)
            {
                transform.position -= Vector3.forward * moveSpeed * Time.deltaTime;
            }
        }
    }

    //Delete Object after set time
    private IEnumerator RemoveSource()
    {
        yield return new WaitForSeconds(existingTime);
        Destroy(gameObject);
    }
}
