using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource shootSource;

    private void Start()
    {
        shootSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
