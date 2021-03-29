using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyConsoleApp
{
    interface IPeopleResponse
    {
        Task<PeopleResponse> GetPeople();
    }
}
