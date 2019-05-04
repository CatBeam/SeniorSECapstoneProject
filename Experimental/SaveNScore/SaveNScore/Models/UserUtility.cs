using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SaveNScore.Models
{
    public static class UserUtility
    {

        public static async Task GenerateUserAchievementsAsync(string uid)
        {
            if (!String.IsNullOrEmpty(uid))
            {
                ApplicationDbContext db = new ApplicationDbContext();

                // Create User's Default Achievements
                List<Achievement> userAchievements = new List<Achievement>{
                    new Achievement { UserID = uid, AchType = AchievementType.CREATE_SAVINGS_ACCOUNT, Description = "Create a Savings Account!", Completed = false, CountToUnlock = 1 },
                    new Achievement { UserID = uid, AchType = AchievementType.CREATE_GOAL, Description = "Create a Goal!", Completed = false, CountToUnlock = 1},
                    new Achievement { UserID = uid, AchType = AchievementType.COMPLETE_GOAL, Description = "Complete a Goal!", Completed = false, CountToUnlock = 1},
                    new Achievement { UserID = uid, AchType = AchievementType.ACCOUNT_5K, Description = "Have an Account with a balance of $5,000!", Completed = false, CountToUnlock = 5000},
                    new Achievement { UserID = uid, AchType = AchievementType.ACCOUNT_10K, Description = "Have an Account with a balance of $10,000!", Completed = false, CountToUnlock = 10000},
                    new Achievement { UserID = uid, AchType = AchievementType.SAVE_1K_TOTAL, Description = "Have a Savings Account with a balance of $1,000!", Completed = false, CountToUnlock = 1000},
                    new Achievement { UserID = uid, AchType = AchievementType.ADD_TRANSACTION, Description = "Add a Transaction to your Account!", Completed = false, CountToUnlock = 1},
                    new Achievement { UserID = uid, AchType = AchievementType.UPDATE_GOAL, Description = "Update an existing Goal!", Completed = false, CountToUnlock = 1}
                };

                // Add each Achievement to the Database.
                foreach (Achievement achievement in userAchievements)
                {
                    db.Achievements.Add(achievement);
                }

                // Commit Changes to DB
                await db.SaveChangesAsync();
            }
        }


        public static async Task UpdateAchievements(string uid)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            //Tables
            var goalsTable = db.Goals;
            var accountsTable = db.CustomersAccounts;
            var achievementsTable = db.Achievements;
            var transactionsTable = db.CustomerTransactions;

            //QUERIES
            /*Queries: All User Savings Accounts and CheckingAccounts*/
            var savingsQuery = accountsTable.Where(s => s.UserID == uid && s.AccountType == CustomerAccountTypeEnum.Savings);
            var checkingQuery = accountsTable.Where(c => c.UserID == uid && c.AccountType == CustomerAccountTypeEnum.Checking);

            /*Queries: All User Transactions and Goals*/
            //TODO: Transactions Query
            var goalsQuery = goalsTable.Where(u => u.UserID == uid);

            //Convert Queries to Lists
            List<CustomerAccount> savings = await savingsQuery.ToListAsync();
            List<CustomerAccount> checkings = await checkingQuery.ToListAsync();
            List<Goal> goals = await goalsQuery.ToListAsync();

            decimal savingBalance1k = 1000;
            decimal accBalance5k = 5000;
            decimal accBalance10k = 10000;

            //Check for: AccountValue$5000, AccountValue$10000
            if (checkings.Count > 0)
            {
                foreach (CustomerAccount ca in checkings)
                {
                    if (ca.Balance >= accBalance5k)
                    {
                        await ActivateAchievementAsync(db, uid, AchievementType.ACCOUNT_5K);
                        if (ca.Balance >= accBalance10k)
                        {
                            await ActivateAchievementAsync(db, uid, AchievementType.ACCOUNT_10K);
                        }
                    }

                }
            }

            //Check for: CreateSavingsAccount, Save$1000
            if (savings.Count > 0)
            {
                await ActivateAchievementAsync(db, uid, AchievementType.CREATE_SAVINGS_ACCOUNT);

                foreach(CustomerAccount ca in savings)
                {
                    //If Savings Account Balance >= $1,000
                    if (ca.Balance >= savingBalance1k)
                    {
                        await ActivateAchievementAsync(db, uid, AchievementType.SAVE_1K_TOTAL);
                        if (ca.Balance >= accBalance5k)
                        {
                            await ActivateAchievementAsync(db, uid, AchievementType.ACCOUNT_5K);
                            if (ca.Balance >= accBalance10k)
                            {
                                await ActivateAchievementAsync(db, uid, AchievementType.ACCOUNT_10K);
                            }
                        }
                        break; //Exit loop if Savings Acc Balance >= $1,000 exists
                    }
                }
                        
            }

            //Check For: CreateGoal, CompleteGoal, UpdateGoal
            if (goals.Count > 0)
            {
                await ActivateAchievementAsync(db, uid, AchievementType.CREATE_GOAL);

                foreach(Goal goal in goals)
                {
                    //TODO: RETHINK UPDATED GOAL?
                    if (goal.Completed)
                    {
                        await ActivateAchievementAsync(db, uid, AchievementType.COMPLETE_GOAL);
                    }
                }
            }

            //TODO: IF(TRANSACTIONS.COUNT > 0)

        }

        private static async Task ActivateAchievementAsync(ApplicationDbContext context, string uid, AchievementType at)
        {
            Achievement achievement = await context.Achievements.FindAsync(uid, at);
            if(achievement.Completed == false)
            {
                // Remove Achievement temporarily
                context.Achievements.Remove(achievement);
                await context.SaveChangesAsync();

                //Change data, add to db, save changes
                achievement.Completed = true;
                context.Achievements.Add(achievement);
                await context.SaveChangesAsync();
            }
        }

    }
}