using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program;

public static class Registration
{

    public static string Login { get { return Login; } set { Login = HashData.HashValue(Login); } }
    public static string Password { get { return Password; } set { Password = HashData.HashValue(Password); } }

}
