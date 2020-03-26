using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float xRange = 7f;
    [SerializeField] float upRange = 4f;
    [SerializeField] float downRange = 5f;
    [Header("Position based")]
    [SerializeField] float posPitchFactor = -5f;
    [SerializeField] float posYawFactor = 5f;
    [Header("Throw based")]
    [SerializeField] float ctrlPitchFactor = -10f;
    [SerializeField] float ctrlRollFactor = -20f;

    float horizontalThrow, verticalThrow;
    bool isControlEnabled = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }
    }

    private void ProcessRotation()
    {
        // todo Fix wrong rotation order along axes 
        float pitch = posPitchFactor * transform.localPosition.y + ctrlPitchFactor * verticalThrow;
        float yaw = posYawFactor * transform.localPosition.x;
        float roll = ctrlRollFactor * horizontalThrow;
        transform.localRotation = Quaternion.AngleAxis(yaw, Vector3.up) *
            Quaternion.AngleAxis(pitch, Vector3.right) *
            Quaternion.AngleAxis(roll, Vector3.forward);



        //transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
       // Debug.Log(pitch + "," + yaw + "," + roll);
    }

    private void ProcessTranslation()
    {
        //  Calculation of X offset based on control throw and screen restrictions
        horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = horizontalThrow * moveSpeed * Time.deltaTime;
        float clampedXOffset = Mathf.Clamp(xOffset + transform.localPosition.x, -xRange, xRange);
        //  Calculation of Y offset based on control throw and screen restrictions
        verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = verticalThrow * moveSpeed * Time.deltaTime;
        float clampedYOffset = Mathf.Clamp(yOffset + transform.localPosition.y, -downRange, upRange);

        //  Move the ship
        transform.localPosition = new Vector3(clampedXOffset, clampedYOffset, transform.localPosition.z);
    }

    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }

}
