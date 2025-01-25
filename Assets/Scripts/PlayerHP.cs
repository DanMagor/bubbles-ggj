using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerHP : MonoBehaviour
{ 
    [SerializeField] private float _startHP = 100;
    [SerializeField] private float _hpInOneSnitch = 5;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _massBufferSnitchPrefab;
    [SerializeField] private float _maxOffset = 0.3f;
    [SerializeField] private float _minOffset = -0.3f;
    [SerializeField] private Rigidbody _rb;
    private float _currentHP;

    private void Awake()
    {
        _currentHP = _startHP;
    }

    public void TakeDamage(float damage)
    {
        _currentHP -= damage;
        CreateSnitch(damage);
        if (_currentHP <= 0)
        {
            _currentHP = 0;
            Death();
        }
    }

    private void CreateSnitch(float hp)
    {
        int snitchCount = Mathf.CeilToInt(hp / _hpInOneSnitch); 
        for (int i = 0; i < snitchCount; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(_minOffset, _maxOffset), 
                0f, 
                Random.Range(_minOffset, _maxOffset)
            );
            var massBuffer = PhotonNetwork.Instantiate(_massBufferSnitchPrefab.name, gameObject.transform.position + randomOffset, Quaternion.identity);
            var snitch = massBuffer.GetComponent<SnitchLogic>();
            var bonus = massBuffer.GetComponent<BonusMassAndSize>();
        
            snitch._boxArea = BoxAreaReference.box;
            bonus._additionalHP = _hpInOneSnitch;
            hp -= _hpInOneSnitch;
        }

    }

    public void GetHP(float hp)
    {
        _currentHP += hp;

        if (_currentHP <= 100f)
        {
            _rb.mass = 1f; // Дефолт пузырь
        }
        else if (_currentHP <= 150f)
        {
            _rb.mass = 1.5f; // Средний пузырь
        }
        else if (_currentHP <= 200f)
        {
            _rb.mass = 2f; // Большой пузырь
        }
        else if (_currentHP <= 300f)
        {
            _rb.mass = 3f; // Огромный пузырь
        }
        else 
        {
            _rb.mass = 4f; // Ебаный пузырь
        }
    }


    private void Death()
    {
        _playerController.KillPlayer();
    }
}
