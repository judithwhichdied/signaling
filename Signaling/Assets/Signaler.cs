using System.Collections;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Signaler : MonoBehaviour
{
    private AudioSource _siren;

    private float _speed = 0.05f;

    private float _maxVolume = 0.3f;

    private float _minVolume = 0;

    private void Start()
    {
        _siren = GetComponent<AudioSource>();
        _siren.volume = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _siren.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IncreaseVolume();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(Decreasing());

        if (_siren.volume == _minVolume)
        {
            StopCoroutine(Decreasing());
            _siren.Stop();
        }
    }

    private void IncreaseVolume()
    {
        _siren.volume = Mathf.MoveTowards(_siren.volume, _maxVolume, _speed * Time.deltaTime);
    }

    private IEnumerator Decreasing()
    {
        while (_siren.volume > _minVolume)
        {
            _siren.volume = Mathf.MoveTowards(_siren.volume, _minVolume, _speed * Time.deltaTime);

            yield return null;
        }
    }
}
