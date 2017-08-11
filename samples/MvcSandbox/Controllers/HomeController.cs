// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcSandbox.Controllers
{
    public class HomeController : Controller
    {
        [ModelBinder]
        public string Id { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult<int> Value(int value)
        {
            if (value == 1)
            {
                return BadRequest();
            }

            return value + 1;
        }

        public async Task<ActionResult<int>> ValueAsync(int value)
        {
            if (value == 1)
            {
                await Task.Delay(10);
                return BadRequest();
            }

            return value + 1;
        }
    }
}
