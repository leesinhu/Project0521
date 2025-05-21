using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public InputManager inputManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 제거
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 간 유지

        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }
}
