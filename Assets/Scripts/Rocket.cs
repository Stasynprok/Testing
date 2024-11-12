using PlasticGui.WorkspaceWindow.PendingChanges;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    public TMP_Text energyText;

    [SerializeField] public Rigidbody _rigidbody;
    [SerializeField] public AudioSource _audioSource;
    [SerializeField]
    public float rotationRate = 1f;
    [SerializeField]
    public float engineRate = 1f;
    [SerializeField]
    public int energyTotal = 1000;
    public int _initialEnergy;
    [SerializeField]
    public int energyApply = 5;

    [SerializeField]
    public AudioClip flySound;
    [SerializeField]
    public AudioClip finishSound;
    [SerializeField]
    public AudioClip boomSound;

    [SerializeField]
    public ParticleSystem launchParticles;
    [SerializeField]
    public ParticleSystem boomParticles;
    [SerializeField]
    public ParticleSystem finishParticles;

    public enum GameState
    {
        Playing,
        Dead,
        NextLevel,
    }

    private GameState state;

    // Start is called before the first frame update
    void Start()
    {
        _initialEnergy = energyTotal;
        UpdateEnergyText();
        state = GameState.Playing;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == GameState.Playing)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            {
                Launch();
                PlayAudio();
            }
            else
            {
                StopAudio();
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                Rotation(true);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Rotation(false);
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Playing)
        {
            ProcessDebugKeys();
        }
    }

    public void Launch()
    {
        float fuelRate = 1f;
        if (energyTotal >= energyApply)
        {
            energyTotal -= energyApply;
        }
        else if (energyTotal > 0)
        {
            fuelRate = (float)energyTotal / energyApply;
            energyTotal = 0;
        }
        else
        {
            _audioSource.Pause();
            launchParticles.Stop();
            return;
        }
        UpdateEnergyText();
        _rigidbody.AddRelativeForce(Vector3.up * engineRate * fuelRate);
    }

    public void PlayAudio()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(flySound);
        }

        if (!launchParticles.isPlaying)
        {
            launchParticles.Play();
        }
    }

    public void StopAudio()
    {
        _audioSource.Pause();
        launchParticles.Stop();
    }

    public void UpdateEnergyText()
    {
        if (energyText)
        {
            energyText.text = energyTotal.ToString();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (state != GameState.Playing)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                energyTotal = _initialEnergy;
                UpdateEnergyText();
                break;
            case "Battery":
                AddEnergy(1500);
                Destroy(other.gameObject);
                break;
            case "Finish":
                Finish();
                break;
            default:
                Lose();
                break;
        }
    }

    public void ProcessDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            var nextIndex = SceneManager.GetActiveScene().buildIndex - 1;
            if (nextIndex < 1)
            {
                nextIndex = 1;
            }
            SceneManager.LoadScene(nextIndex);
        }
    }

    public void Finish()
    {
        state = GameState.NextLevel;
        _audioSource.Stop();
        launchParticles.Stop();
        _audioSource.PlayOneShot(finishSound);
        finishParticles.Play();
        //Invoke(nameof(LoadNextLevel), 2f);
    }

    public void AddEnergy(int value)
    {
        energyTotal += value;
        UpdateEnergyText();
    }

    public void Lose()
    {
        state = GameState.Dead;
        _audioSource.Stop();
        launchParticles.Stop();
        _audioSource.PlayOneShot(boomSound);
        boomParticles.Play();
        //Invoke(nameof(LoadFirstLevel), 2f);
    }

    public GameState GetStatus()
    {
        Debug.Log(state);
        return state;
    }

    public void LoadNextLevel()
    {
        var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextIndex -= 1;
        }
        SceneManager.LoadScene(nextIndex);
    }

    public void LoadFirstLevel()
    {
        // SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Rotation(bool right)
    {
        _rigidbody.freezeRotation = true;
        if (right)
        {
            transform.Rotate(Vector3.forward * rotationRate);
        }
        else if (!right)
        {
            transform.Rotate(-Vector3.forward * rotationRate);
        }
        _rigidbody.freezeRotation = false;
    }
}
