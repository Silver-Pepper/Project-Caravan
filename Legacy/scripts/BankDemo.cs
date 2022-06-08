using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
///just to test the script
/// <summary>
public class BankDemo : MonoBehaviour
{
    Bank testBank;
    BankAccount testAccount;
    GameDateTime nextTimeToUpdate;
    //User input

    public void Start()
    {
        //load saved data here
        testBank = new Bank() { BankName = "testBank" };
        nextTimeToUpdate = GameDateTime.ReturnZeroHour(GameDateTime.TimeLapseDay(1, GameTime.Now));
        testAccount = new BankAccount()
        {
            Bank = testBank,
            DemandDeposit = 0,
            TermDepositList = new List<DepositAndLoan>(),
            LoanList = new List<DepositAndLoan>(),
            OverdueLoanList = new List<DepositAndLoan>()
        };
        testAccount.Deposit(1000, 1, GameTime.GetCurrentGameDateTime(), testBank);
        testAccount.Loan(1000, 2, GameTime.GetCurrentGameDateTime(), testBank);
        testAccount.Loan(1000, 1, GameTime.GetCurrentGameDateTime(), testBank);
        InvokeRepeating("AccountDailyUpdate", 0.1f, 1);
    }



    public void AccountDailyUpdate()
    {
        if (GameDateTime.Compare(nextTimeToUpdate, GameTime.Now) >= 0)
        {
            BankAccount.AccountDailyUpdateAsync(testAccount);
            nextTimeToUpdate = GameDateTime.ReturnZeroHour(GameDateTime.TimeLapseDay(1, nextTimeToUpdate));
        }

        //debug lines

        if (testAccount.TermDepositList != null)
        {
            foreach (var deposit in testAccount.TermDepositList)
            {
                Debug.Log("amount of deposit = " + deposit.Amount);
            }
        }


        if (testAccount.LoanList != null)
        {
            foreach (var loan in testAccount.LoanList)
            {
                Debug.Log("amount of loan = " + loan.Amount);
            }
        }


        if (testAccount.OverdueLoanList != null)
        {
            foreach (var deposit in testAccount.OverdueLoanList)
            {
                Debug.Log("amount of overdue loan = " + deposit.Amount);
            }
        }

        Debug.Log("demand deposit = " + testAccount.DemandDeposit);
    }

}






