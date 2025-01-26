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
    [SerializeField] private float _maxOffset = 5f;
    [SerializeField] private float _minOffset = -5f;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _from100Scale = 0.2f;
    [SerializeField] private float _from150Scale = 0.2f;
    [SerializeField] private float _from200Scale = 0.2f;
    [SerializeField] private float _from300Scale = 0.2f;
    [SerializeField] private float _from400Scale = 0.2f;
    [SerializeField] private float _from100Mass = 0.2f;
    [SerializeField] private float _from150Mass = 0.2f;
    [SerializeField] private float _from200Mass = 0.2f;
    [SerializeField] private float _from300Mass = 0.2f;
    [SerializeField] private float _from400Mass = 0.2f;
    private float _currentHP;

    private void Awake()
    {
        _currentHP = _startHP;
    }

    public void TakeDamage(float damage)
    {
        _currentHP -= damage;
        CreateSnitch(damage);
        if (_currentHP <= 100f)
        {
            _rb.mass -= _from100Mass; // Дефолт пузырь
            gameObject.transform.localScale -= new Vector3(_from100Scale, _from100Scale, _from100Scale);
        }
        else if (_currentHP <= 150f)
        {
            _rb.mass -= _from150Mass; // Средний пузырь
            gameObject.transform.localScale -= new Vector3(_from150Scale, _from150Scale, _from150Scale);
        }
        else if (_currentHP <= 200f)
        {
            _rb.mass -= _from200Mass; // Большой пузырь
            gameObject.transform.localScale -= new Vector3(_from200Scale, _from200Scale, _from200Scale);
        }
        else if (_currentHP <= 300f)
        {
            _rb.mass -= _from300Mass; // Огромный пузырь
            gameObject.transform.localScale -= new Vector3(_from300Scale, _from300Scale, _from300Scale);
        }
        else 
        {
            _rb.mass -= _from400Mass; // Ебаный пузырь
            gameObject.transform.localScale -= new Vector3(_from400Scale, _from400Scale, _from400Scale);
        }
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
                Random.Range(_minOffset - gameObject.transform.localScale.x, _maxOffset + gameObject.transform.localScale.x), 
                0f, 
                Random.Range(_minOffset - gameObject.transform.localScale.x, _maxOffset + gameObject.transform.localScale.x)
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
            _rb.mass += _from100Mass; // Дефолт пузырь
            gameObject.transform.localScale += new Vector3(_from100Scale, _from100Scale, _from100Scale);
        }
        else if (_currentHP <= 150f)
        {
            _rb.mass += _from150Mass; // Средний пузырь
            gameObject.transform.localScale += new Vector3(_from150Scale, _from150Scale, _from150Scale);
        }
        else if (_currentHP <= 200f)
        {
            _rb.mass += _from200Mass; // Большой пузырь
            gameObject.transform.localScale += new Vector3(_from200Scale, _from200Scale, _from200Scale);
        }
        else if (_currentHP <= 300f)
        {
            _rb.mass += _from300Mass; // Огромный пузырь
            gameObject.transform.localScale += new Vector3(_from300Scale, _from300Scale, _from300Scale);
        }
        else 
        {
            _rb.mass += _from400Mass; // Ебаный пузырь
            gameObject.transform.localScale += new Vector3(_from400Scale, _from400Scale, _from400Scale);
        }
    }


    private void Death()
    {
        _playerController.KillPlayer();
    }
}
