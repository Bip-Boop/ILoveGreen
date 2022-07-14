using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoroutinesPack
{
    public IEnumerator WaitThenDo(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);

        action.Invoke();
    }
}
