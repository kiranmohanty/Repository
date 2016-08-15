using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyWeb
{
    #region OOPs
    public class OppsFile
    {
    }

    public class Encapsulation
    {
        public int fisrtParam;
        public int secondParam;
        public int result;
        public Encapsulation(int a, int b)
        {
            fisrtParam = a;
            secondParam = b;
            Sum();
        }
        /// <summary>
        /// Sum method is private, no instance can access it. Hence encapsulation of data/logic.
        /// </summary>
        private void Sum()
        {
            result = fisrtParam + secondParam;
        }
    }

    public class Abstraction
    {
        public int Count ;
        public void SumTwoNumbers()
        {
            Encapsulation encapsulation = new Encapsulation(2, 3);
            int result = encapsulation.result; // can only see property that is necessary, not Sum method.
        }
        public virtual int MethodTobeOverriden(int a, int b)
        {
            return a + b;
        }
    }

    public class Inheritance : Abstraction
    {
        public void DoNothing()
        {
            Count = 5;// property from parent class
        }

        /// <summary>
        /// polymerphism (overloading)
        /// </summary>
        /// <param name="a"></param>
        public void DoNothing(int a)
        {
            Count = a;
        }

        //polymerphism (overwritting)
        public override int MethodTobeOverriden(int a, int b)
        {
            return a - b;
        }

    }

    public interface IExample
    {
        //int a; biult error :- interface can't contain fields
        int a { get; set; } // it can contain properties.
    }

    public class Example : IExample
    {
        public int a
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }
    }
    #endregion

    #region Employee

    public class BasicUserInfo
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public BasicUserInfo()
        {
            ID = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
        }
        public BasicUserInfo(int  id, string firstName,string lastName) // overloading
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
        }
        public virtual string FullName()
        {
            return FirstName + " " + LastName;
        }

        public bool IsValidUser()
        {
            return ValidUser();
        }

        private bool ValidUser() // Encapsulation
        {
            return ID >= 0 ? true : false;
        }
    }

    public class EmployeeInfo:BasicUserInfo // inheritance
    {
        public string EmailID { get; set;}
        public string MobileNumber { get; set; }

        public override string FullName() // overridding
        {
            return LastName + " " + FirstName;
        }

        public void ValidateUser()
        {
            IsValidUser(); // abstraction
        }

        public void Add(int a, int b)
        {
        }
        public void Add(ref int a, ref int b)
        {
        }
    }

    #endregion

    #region SingleTon

    public class SingleTon
    {
        private static SingleTon singleTon = null;

        private  SingleTon()
        {            
        }

        public static SingleTon SingleTonInstance
        {
            get {
                
                    if (singleTon == null)
                    {
                        singleTon = new SingleTon();
                    }                
                return singleTon;
            }
        }
    }

    public class TestSingleTone
    {
        SingleTon singleTone = SingleTon.SingleTonInstance;
        SingleTon singleTone1 = SingleTon.SingleTonInstance;// both the instancces point  to same heap memory location
    }
    // singleton design pattern will instantiate only one instance of the class through out the application. To make it thread safe, tread safe singleton pattern we should follow.
    //it's different from static class. Static class can't be instantiated, but Singleton class can be (only once).
    //satic class can't inherit any class, but singleton class can.
    // stataic class can't have constructor, but singleton class can have.

    #endregion


    #region Constructor
   //Inheritance is not applicable to static class.
    public  class A
    {
        private int x;
        private static int y;
        // class having only private constructor can't be instantiated and can't be inherited.
        //private constructors are used to avoid instantiation of a class.
        private A()
        {
        }

        //if any public constructor is avalable, then that class can be instantiated.
        public A(int a) 
        {
            this.x = a;
        }

        //static constructor can't have access modifier, it's default public.
        //static constructor are meant for intializing static field .
        //static constructor will be called by CLR, not by any instance.
        //defining parameterized static constructor is not possible.
         static A()
        {
            // x = 1;  no non-static field can be initialized here.
            y = 1;
        }

    }

    #endregion

    #region Finalize & Disppose
    //For cleaning unmanaged code we should define dispose method for that class. For managed code dispose or finalize is not required as garbage collector will take care of cleaning managed code.
    //While defining finilize method for any class , it's better to define dispose method as well
    //In dispose method it's always better to have code " GC.SuppressFinalize(this);"
    //finalize is nothing but destructor of a class.

    public class DisposeAndFinalize : IDisposable
    {
        public void Dispose()
        {
            //logic for disposing unmanaged code
            GC.SuppressFinalize(this);
        }
        ~DisposeAndFinalize()
        {
            //logic for disposing unmanaged code
        }

    }
    #endregion
}