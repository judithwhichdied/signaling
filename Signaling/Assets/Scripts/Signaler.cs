using System.Collections;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
[RequireComponent(typeof(Watcher))]
public class Signaler : MonoBehaviour
{
    private AudioSource _siren;

    private Watcher _watcher;

    private float _speed = 0.05f;

    private float _maxVolume = 0.3f;

    private float _minVolume = 0;

    private void Awake()
    {
        _siren = GetComponent<AudioSource>();
        _watcher = GetComponent<Watcher>();
        _siren.volume = 0;
    }

    private void OnEnable()
    {
        _watcher.AlarmOn += IncreaseVolume;
        _watcher.AlarmOff += DecreaseVolume;
    }

    private void OnDisable()
    {
        _watcher.AlarmOn -= IncreaseVolume;
        _watcher.AlarmOff -= DecreaseVolume;
    }

    private void IncreaseVolume()
    {
        _siren.Play();

        StartCoroutine(VolumeChanging(_maxVolume));

        if (_siren.volume == _maxVolume)
            StopCoroutine(VolumeChanging(_maxVolume));
    }

    private void DecreaseVolume()
    {
        StartCoroutine(VolumeChanging(_minVolume));

        if (_siren.volume == _minVolume)
        {
            StopCoroutine(VolumeChanging(_minVolume));
            _siren.Stop();
        }
            
    }

    private IEnumerator VolumeChanging(float value)
    {
        while (_siren.volume != value)
        {
            _siren.volume = Mathf.MoveTowards
                (_siren.volume, value, _speed * Time.deltaTime);

            yield return null;
        }
    }
}
