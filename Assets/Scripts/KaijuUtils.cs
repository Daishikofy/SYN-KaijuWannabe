using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KaijuUtils
{
    public static int GetLevel(Bounds bounds)
    {
        float level = bounds.size.x + bounds.size.y + bounds.size.z;
        return (int)level;
    }

    public static int GetLevel(float radius)
    {
        float level = (int)(radius * 3);
        return (int)level;
    }
}
