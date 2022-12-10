using System.Collections;
using UnityEngine;

namespace SPS_Slime_RPG.Code.Infrastructure.Services.CoroutineRunner
{
    public interface ICoroutineRunner : IService
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}