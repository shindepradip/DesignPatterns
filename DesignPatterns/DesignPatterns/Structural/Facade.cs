using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural
{
    interface IBank
    {
        bool HasSufficientSavings(ICustomer c, int amount);
    }

    interface ICredit
    {
        bool HasGoodCredit(ICustomer c);
    }

    interface ILoan
    {
        bool HasNoBadLoans(ICustomer c);
    }

    /// <summary>
    /// The 'Subsystem ClassA' class
    /// </summary>
    class Bank : IBank
    {
        public bool HasSufficientSavings(ICustomer c, int amount)
        {
            Console.WriteLine("Check bank for " + c.Name);
            return true;
        }
    }

    /// <summary>
    /// The 'Subsystem ClassB' class
    /// </summary>
    class Credit : ICredit
    {
        public bool HasGoodCredit(ICustomer c)
        {
            Console.WriteLine("Check credit for " + c.Name);
            return true;
        }
    }

    /// <summary>
    /// The 'Subsystem ClassC' class
    /// </summary>
    class Loan : ILoan
    {
        public bool HasNoBadLoans(ICustomer c)
        {
            Console.WriteLine("Check loans for " + c.Name);
            return true;
        }
    }

    interface ICustomer
    {
        string Name { get; }
    }

    /// <summary>
    /// Customer class
    /// </summary>
    class Customer : ICustomer
    {
        private string _name;

        // Constructor
        public Customer(string name)
        {
            this._name = name;
        }

        // Gets the name
        public string Name
        {
            get { return _name; }
        }
    }

    interface IMortgage
    {
        bool IsEligible(ICustomer cust, int amount);
    }

    /// <summary>
    /// The 'Facade' class
    /// </summary>
    class Mortgage : IMortgage
    {
        private readonly IBank _bank;
        private readonly ILoan _loan;
        private readonly ICredit _credit;

        public Mortgage(IBank bank,ILoan loan, ICredit credit)
        {
            _bank = bank;
            _credit = credit;
            _loan = loan;
        }

        public bool IsEligible(ICustomer cust, int amount)
        {
            Console.WriteLine("{0} applies for {1:C} loan\n",
              cust.Name, amount);

            bool eligible = true;

            // Check creditworthyness of applicant
            if (!_bank.HasSufficientSavings(cust, amount))
            {
                eligible = false;
            }
            else if (!_loan.HasNoBadLoans(cust))
            {
                eligible = false;
            }
            else if (!_credit.HasGoodCredit(cust))
            {
                eligible = false;
            }

            return eligible;
        }
    }
}
