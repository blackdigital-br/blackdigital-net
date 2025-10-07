﻿namespace BlackDigital.Rest.Transforms
{
    [Flags]
    public enum TransformDirection
    {
        Input = 1,
        Output = 2,
        Both = Input | Output
    }
}
