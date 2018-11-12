using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Her er der implementeret DI - Dependency Injection (Pattern) med method injection.
// Som tilfældet var med Dependency Injection (Pattern) med constructor injection, kan dette eksempel
// med Dependency Injection også udbygges, så der kan mikses klasser fra Business logik laget med 
// klasser fra Data Access laget på kryds og på tværs.

namespace Dependency_Injection4d_DI_Method_Injection
{
    public interface ICustomerDataAccess
    {
        string GetCustomerData(int id);
    }

    public interface IDataAccessDependency
    {
        void SetDependency(ICustomerDataAccess customerDataAccess);
    }

    public class CustomerBusinessLogic : IDataAccessDependency
    {
        private ICustomerDataAccess _dataAccess;

        public CustomerBusinessLogic()
        {

        }

        public string ProcessCustomerData(int id)
        {
            return _dataAccess.GetCustomerData(id);
        }
        
        public void SetDependency(ICustomerDataAccess customerDataAccess)
        {
            _dataAccess = customerDataAccess;
        }
    }

    public class CustomerService
    {
        CustomerBusinessLogic _customerBL;

        public CustomerService()
        {
            _customerBL = new CustomerBusinessLogic();
            _customerBL.SetDependency(new CustomerDataAccess());
        }

        public string GetCustomerName(int id)
        {
            return _customerBL.ProcessCustomerData(id);
        }
    }

    public class CustomerDataAccess : ICustomerDataAccess
    {
        public CustomerDataAccess()
        {
        }

        public string GetCustomerData(int id)
        {
            //get the customer name from the db in real application        
            return "Dummy Customer Name DI - Method Injection " + id.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CustomerService CustomerService_Object = new CustomerService();
            Console.WriteLine(CustomerService_Object.GetCustomerName(16));
            Console.ReadLine();
        }
    }
}
