// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNetCore.Mvc
{
    public class ActionResult<TValue>
    {
        public ActionResult(TValue value)
        {
            Value = value;
        }

        public ActionResult(ActionResult result)
        {
            Result = result;
        }

        public ActionResult Result { get; }

        public TValue Value { get; }

        public static implicit operator ActionResult<TValue>(TValue value)
        {
            return new ActionResult<TValue>(value);
        }

        public static implicit operator ActionResult<TValue>(ActionResult result)
        {
            return new ActionResult<TValue>(result);
        }
    }
}