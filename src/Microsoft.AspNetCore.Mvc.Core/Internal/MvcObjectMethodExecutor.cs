using System;
using System.Diagnostics;
using Microsoft.Extensions.Internal;

namespace Microsoft.AspNetCore.Mvc.Internal
{
    public struct ActionResultOfTUnwrapper
    {
        public ActionResultOfTUnwrapper(Type returnType)
        {
            ActionResultGetter = null;
            ValueGetter = null;
            DeclaredValueType = null;

            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition().IsAssignableFrom(typeof(ActionResult<>)))
            {
                var genericTypeArguments = returnType.GetGenericArguments();
                Debug.Assert(genericTypeArguments.Length == 1);
                DeclaredValueType = genericTypeArguments[0];

                var properties = PropertyHelper.GetVisibleProperties(returnType);
                for (var i = 0; i < properties.Length; i++)
                {
                    var property = properties[i];
                    if (string.Equals(property.Name, nameof(ActionResult<object>.Value), StringComparison.Ordinal))
                    {
                        ValueGetter = property.ValueGetter;
                    }
                    else if (string.Equals(property.Name, nameof(ActionResult<object>.Result), StringComparison.Ordinal))
                    {
                       ActionResultGetter = property.ValueGetter;
                    }
                }
            }
        }

        /// <summary>
        /// Determines if the return type is sync or async version of <see cref="ActionResult{TValue}"/>.
        /// </summary>
        public bool IsActionResultOfT => DeclaredValueType != null;

        /// <summary>
        /// Delegate to get <see cref="ActionResult{TValue}.Result"/>
        /// </summary>
        public Func<object, object> ActionResultGetter { get; }

        /// <summary>
        /// Delegate to get <see cref="ActionResult{TValue}.Value"/>
        /// </summary>
        public Func<object, object> ValueGetter { get; }

        /// <summary>
        /// The generic paramter type in the <see cref="ActionResult{TValue}"/> return value.
        /// </summary>
        public Type DeclaredValueType { get; }

        public IActionResult GetActionResult(object resultAsObject)
        {
            var result = (IActionResult)ActionResultGetter(resultAsObject);
            if (result == null)
            {
                resultAsObject = ValueGetter(resultAsObject);
                result = new ObjectResult(resultAsObject)
                {
                    DeclaredType = DeclaredValueType,
                };
            }

            return result;
        }
    }
}
