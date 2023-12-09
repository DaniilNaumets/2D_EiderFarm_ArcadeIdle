using Characters;
using UnityEngine;

namespace Audio
{
    public class BallAudio : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.GetComponent<Walker>())
                FMODUnity.RuntimeManager.PlayOneShot("event:/���� �� ���� 2.0");
            else if (other.transform.CompareTag("Obstacle"))
                FMODUnity.RuntimeManager.PlayOneShot("event:/���� � ������ ���� (�� ������, ������) 2.0");
            else
                FMODUnity.RuntimeManager.PlayOneShot("event:/���� � ������ ���� (�� �� ������) 2.0");
        }
    }
}