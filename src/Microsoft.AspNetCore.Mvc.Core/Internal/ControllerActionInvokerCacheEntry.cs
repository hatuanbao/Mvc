// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Internal;

namespace Microsoft.AspNetCore.Mvc.Internal
{
    public class ControllerActionInvokerCacheEntry
    {
        internal ControllerActionInvokerCacheEntry(
            FilterItem[] cachedFilters,
            Func<ControllerContext, object> controllerFactory,
            Action<ControllerContext, object> controllerReleaser,
            ControllerBinderDelegate controllerBinderDelegate,
            ObjectMethodExecutor actionMethodExecutor,
            ActionResultOfTUnwrapper unwrapper)
        {
            ControllerFactory = controllerFactory;
            ControllerReleaser = controllerReleaser;
            ControllerBinderDelegate = controllerBinderDelegate;
            CachedFilters = cachedFilters;
            ActionMethodExecutor = actionMethodExecutor;
            ActionResultOfTUnwrapper = unwrapper;
        }

        public FilterItem[] CachedFilters { get; }

        public Func<ControllerContext, object> ControllerFactory { get; }

        public Action<ControllerContext, object> ControllerReleaser { get; }

        public ControllerBinderDelegate ControllerBinderDelegate { get; }

        internal ObjectMethodExecutor ActionMethodExecutor { get; }

        internal ActionResultOfTUnwrapper ActionResultOfTUnwrapper { get; }
    }
}
