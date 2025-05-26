using System.Collections;
using UnityEngine;

namespace Scripts.Services
{
  public interface ICoroutineRunner : IService
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
    void StopCoroutine(Coroutine coroutine);
  }
}