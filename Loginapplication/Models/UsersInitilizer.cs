using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Loginapplication.Models
{
    public class UsersInitilizer : DropCreateDatabaseIfModelChanges<LoginDbContext>
    {
        

    }
}