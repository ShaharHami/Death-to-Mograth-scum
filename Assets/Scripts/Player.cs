using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 45f;
    [Tooltip("In ms")] [SerializeField] float xRange = 30f;
    [Tooltip("In ms")] [SerializeField] float yRange = 30f;
    [Header("Guns")]
    [SerializeField] GameObject[] primaryWepaon;
    [SerializeField] GameObject[] secondaryWeapon;
    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -1f;
    [SerializeField] float positionYawFactor = 1f;
    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -25f;
    [SerializeField] float controlRollFactor = -25f;
    [SerializeField] bool isInvertedVerticalAxis = true;
    float xThrow, yThrow;
    bool controlsEnabled = true;
    void Update()
    {
        if (controlsEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        if (isInvertedVerticalAxis)
        {
            yThrow *= -1;
        }
        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);
        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawNewYPos, -yRange, yRange);
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void ProcessFiring()
    {
        SetGunsActive(CrossPlatformInputManager.GetButton("Fire"), primaryWepaon);
        // SetGunsActive(CrossPlatformInputManager.GetButton("AltFire"), secondaryWeapon);
    }
    void SetGunsActive(bool isActive, GameObject[] guns)
    {
        foreach (GameObject gun in guns)
        {
            var particles = gun.GetComponent<ParticleSystem>().emission;
            particles.enabled = isActive;
        }
        AudioSource sound = guns[0].GetComponent<AudioSource>();
        if (isActive)
        {
            if (!sound.isPlaying)
            {
                sound.Play();
            }
        }
        else 
        {
            sound.Stop();
        }
    }
    void OnPlayerDeath() // String Reference
    {
        controlsEnabled = false;
        SetGunsActive(false, primaryWepaon);
        SetGunsActive(false, secondaryWeapon);
    }
    public void OnGameEnd()
    {
        controlsEnabled = false;
    }
}
