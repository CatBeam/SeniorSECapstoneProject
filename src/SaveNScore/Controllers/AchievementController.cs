using SaveNScore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SaveNScore.Controllers
{
    public class AchievementController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Achievement
        public async Task<ActionResult> Index()
        {
            var achievements = db.Achievements;
            
            return View(await achievements.ToListAsync());
        }
    }
}