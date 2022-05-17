using System.Collections;
using UnityEngine;

namespace Modules.Core.Infrastructure
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}