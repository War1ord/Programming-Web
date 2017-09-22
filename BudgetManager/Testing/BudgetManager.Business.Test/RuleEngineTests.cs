using BudgetManager.Data;
using BudgetManager.Data.Extensions;
using BudgetManager.Enums;
using BudgetManager.Models.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Linq;

namespace BudgetManager.Business.Test
{
    [TestClass]
    public class RuleEngineTests
    {
        [TestMethod]
        public void RuleEngineCreateTestData()
        {
            using (var db = new DataContext())
            {
                var bankAccount = db.Users
                    .Include(i => i.BankAccounts)
                    .Where(i => i.Email.Contains("kuperus.charles@gmail.com"))
                    .Select(i => i.BankAccounts.FirstOrDefault(b => b.Name.Contains("Cheque")))
                    .FirstOrDefault();
                if (bankAccount == null) Assert.Fail("Invalid bank account.");
                var transactionGroup = new BankTransactionGroup { Name = "Bank Fees" };
                db.Entry(db.BankTransactionGroups.Attach(transactionGroup)).State = EntityState.Added;
                db.SaveChanges();
                var rule = new BankTransactionRule
                {
                    Description = "Bank Fees",
                    Text = "Fee",
                    BankAccountId = bankAccount.Id,
                    BankTransactionGroupId = transactionGroup.Id,
                    RuleType = RuleType.Including,
                };
                db.Entry(db.BankTransactionRules.Attach(rule)).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void RuleEngineLoadingTest()
        {
            User user = null;
            using (var db = new DataContext())
            {
                user = db.Users.FirstOrDefault(i => i.Email.Contains("kuperus.charles@gmail.com"));
            }
            if (user != null)
            {
                var ruleEngine = new RuleEngine();
                ruleEngine.Load(user.Id);
                Assert.IsTrue(ruleEngine.CurrentRules.Count > 0, "There is no rules.");
            }
            Assert.Fail("User not found.");
        }
    }
}
