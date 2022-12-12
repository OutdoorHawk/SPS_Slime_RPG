using System.Collections;
using UnityEngine;

namespace Project.Code.Infrastructure.Services.CoroutineRunner
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}