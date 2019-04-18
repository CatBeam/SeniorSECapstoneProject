﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SaveNScore.Models;

namespace SaveNScore.Controllers
{
    public class AchievementController : Controller
    {
        //Create DB Instance
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Achievement
        public async Task<ActionResult> Index()
        {
            var uid = User.Identity.GetUserId();

            //Get All User Achievements
            var userAchievements = db.Achievements.Where(u => u.UserID == uid);

            return View(await userAchievements.ToListAsync());
        }
    }
}