using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerInquiryAPI.Models
{

    public class SharedObjectContext
    {
        private readonly Test_DBEntities context;

        #region Singleton Pattern

        // Static members are lazily initialized.
        // .NET guarantees thread safety for static initialization.
        private static readonly SharedObjectContext instance = new SharedObjectContext();

        // Make the constructor private to hide it.
        // This class adheres to the singleton pattern.
        private SharedObjectContext()
        {
            // Create the ObjectContext.
            context = new Test_DBEntities();
        }

        // Return the single instance of the ClientSessionManager type.
        public static SharedObjectContext Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        public Test_DBEntities Context
        {
            get
            {
                return context;
            }
        }
    }
}



