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
        #region Achievements
        public static async Task GenerateUserAchievementsAsync(string uid)
        {
            if (!String.IsNullOrEmpty(uid))
            {
                ApplicationDbContext db = new ApplicationDbContext();

                // Create User's Default Achievements
                //IF ACHIEVEMENTS ARE ADDED THEN THE CONDITIONAL ON LINE 60 MUST BE UPDATED
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

            //Check to make sure Achievements exist
            List<Achievement> userAchievements = await achievementsTable.Where(u => u.UserID == uid).ToListAsync();

            // If user doesn't have achievements, generate them.
            // CURRENTLY HAVE 8 ACHIEVEMENTS
            if(userAchievements.Count != 8)
            {
                await GenerateUserAchievementsAsync(uid);
                db = new ApplicationDbContext();
                achievementsTable = db.Achievements;
            }

            //QUERIES
            /*Queries: All User Savings Accounts and CheckingAccounts*/
            var savingsQuery = accountsTable.Where(s => s.UserID == uid).Where(s => s.AccountType == CustomerAccountTypeEnum.Savings);
            var checkingQuery = accountsTable.Where(c => c.UserID == uid).Where(c => c.AccountType == CustomerAccountTypeEnum.Checking);


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
                    Decimal accountBalance = await UserUtility.CalculateBalances(ca.UserID, ca.AccountNum);
                    if (accountBalance >= accBalance5k)
                    {
                        await ActivateAchievementAsync(db, uid, AchievementType.ACCOUNT_5K);
                        if (accountBalance >= accBalance10k)
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
                    Decimal accountBalance = await UserUtility.CalculateBalances(ca.UserID, ca.AccountNum);
                    //If Savings Account Balance >= $1,000
                    if (accountBalance >= savingBalance1k)
                    {
                        await ActivateAchievementAsync(db, uid, AchievementType.SAVE_1K_TOTAL);
                        if (accountBalance >= accBalance5k)
                        {
                            await ActivateAchievementAsync(db, uid, AchievementType.ACCOUNT_5K);
                            if (accountBalance >= accBalance10k)
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
            }

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

        #endregion Achievements
        public static async Task<List<SelectListItem>> GetUserAccountsList(ApplicationDbContext db, string uid)
        {
            //Get Customer Accounts tied to UserID
            var customerAccounts = db.CustomersAccounts.Where(u => u.UserID == uid);
            List<CustomerAccount> customerAccs = await customerAccounts.ToListAsync();
            List<SelectListItem> caList = new List<SelectListItem>();

            //For each account number tied to the User's UserID, save the account number
            foreach (var acc in customerAccs)
            {
                caList.Add(new SelectListItem { Text = acc.AccountNum, Value = acc.AccountNum });
            }

            return caList;
        }

        public static async Task UpdateGoal(string uid, CustomerTransaction trans)
        {

            ApplicationDbContext db = new ApplicationDbContext();

            List<Goal> goals = await db.Goals.Where(g => g.AccountNum == trans.AccountNum).ToListAsync();

            foreach(Goal goal in goals)
            {
                bool resetvalue = false;

                //Remove Goal from DB
                db.Goals.Remove(goal);
                await db.SaveChangesAsync();

                //Update Goal Amount
                if(trans.TransactionType == TransactionTypeEnum.Credit)
                {
                    goal.StartValue += trans.Amount;
                }else if(trans.TransactionType == TransactionTypeEnum.Debit){
                    goal.StartValue -= trans.Amount;
                }

                //Check Completion criteria
                if (goal.StartValue >= goal.LimitValue)
                {
                    goal.Completed = true;
                    goal.StartValue = goal.LimitValue;
                    await ActivateAchievementAsync(db, uid, AchievementType.COMPLETE_GOAL);
                }

                Goal resetGoal = goal;

                if (goal.StartDate >= goal.EndDate || (goal.Completed))
                {
                    if(goal.GoalType == GoalTypeEnum.Recurring)
                    {
                        resetvalue = true;
                        resetGoal.StartDate = DateTime.Now;
                        resetGoal.StartValue = 0;
                        resetGoal.Completed = false;

                        switch (resetGoal.GoalPeriod)
                        {
                            case GoalPeriodEnum.Weekly:
                                resetGoal.EndDate = resetGoal.StartDate.AddDays(7);
                                break;

                            case GoalPeriodEnum.Monthly:
                                resetGoal.EndDate = resetGoal.StartDate.AddMonths(1);
                                break;

                            case GoalPeriodEnum.Yearly:
                                resetGoal.EndDate = resetGoal.StartDate.AddYears(1);
                                break;
                        }
                    }

                }

                await ActivateAchievementAsync(db, uid, AchievementType.UPDATE_GOAL);

                if (resetvalue)
                    db.Goals.Add(resetGoal);
                else
                    db.Goals.Add(goal);

                await db.SaveChangesAsync();
            }

            await ActivateAchievementAsync(db, uid, AchievementType.ADD_TRANSACTION);
        }


        public static async Task<Decimal> CalculateBalances(string uid, string accountNumber)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            List<CustomerAccount> userAccs = await db.CustomersAccounts.Where(u => u.AccountNum == accountNumber).Where(u => u.UserID == uid).ToListAsync();

            CustomerAccount userAcc = userAccs.ElementAt(0);

            Decimal currBalance = userAcc.Balance;


            List<CustomerTransaction> transList = await db.CustomerTransactions
                .Where(a => a.AccountNum == accountNumber)
                .OrderBy(data => data.TransactionDate)
                .ToListAsync();

            foreach (CustomerTransaction ct in transList)
            {
                if (ct.TransactionType == TransactionTypeEnum.Credit)
                    currBalance += ct.Amount;
                else
                    currBalance -= ct.Amount;
            }

            return currBalance;
        }
    }


}