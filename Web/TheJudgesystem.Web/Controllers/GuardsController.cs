using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheJudgesystem.Web.Controllers
{
    [Authorize(Roles = "Guard")]
    public class GuardsController : Controller
    {
    }
}
