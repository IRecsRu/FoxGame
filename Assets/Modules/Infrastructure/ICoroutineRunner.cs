using System.Collections;
using UnityEngine;

namespace Modules.Infrastructure
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}