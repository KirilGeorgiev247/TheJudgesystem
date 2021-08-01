using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheJudgesystem.Web.Controllers
{
    [Authorize(Policy = "IsJudge")]
    public class JudgesController : Controller
    {
    }
}
